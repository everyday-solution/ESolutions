using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace ESolutions
{
	/// <summary>
	/// Enables component based late binding for assemblies and objects.
	/// </summary>
	/// <remarks>
	/// Do not refactor this class to multiple methods. It has really bad influence on any applications performance.
	/// </remarks>
	public static class Microkernel
	{
		//Fields
		#region typeMappings
		private static SortedList<String, TypeMapping> typeMappings = new SortedList<String, TypeMapping>();
		#endregion

		#region assemblyMapping
		private static SortedList<String, AssemblyMapping> assemblyMapping = new SortedList<String, AssemblyMapping>();
		#endregion

		#region fallbackPath
		private static String fallbackPath = String.Empty;
		#endregion

		#region isInitialized
		private static Boolean isInitialized = false;
		#endregion

		//Events
		#region ConfigurationChanged
		/// <summary>
		/// Occurs after each call to the initialize methods.
		/// </summary>
		public static event EventHandler ConfigurationChanged;
		#endregion

		//Methods
		#region Initialize
		/// <summary>
		/// Initializes the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <remarks>
		/// Initialization is done in a single methods and not in seperate methods to
		/// remarkebly improve speed. MENTION: The location where assemblies are searched is foremost die value 
		/// from the 'pathToFile' attribute if assemblies. If the attribute is not set the path of the Microkernel.Mappings.xml
		/// is taken. If the assembly is used and not found in those places the fallback path is used. 
		/// </remarks>
		public static void Initialize(FileInfo file)
		{
			try
			{
				#region CleanConfiguration
				Microkernel.assemblyMapping = new SortedList<String, AssemblyMapping>();
				Microkernel.typeMappings = new SortedList<String, TypeMapping>();
				#endregion

				#region ReadFileToXmlDocument
				var schemaFile = Encoding.UTF8.GetBytes(Properties.Resources.Microkernel_Mappings_Schema);
				var schemaStream = new MemoryStream(schemaFile);
				var schema = XmlSchema.Read(schemaStream, null);

				var settings = new XmlReaderSettings();
				settings.Schemas.Add(schema);
				settings.ValidationType = ValidationType.Schema;

				using (var reader = XmlReader.Create(file.FullName, settings))
				{
					var doc = new XmlDocument();
					doc.Load(reader);
					#endregion

					#region PrepareParsing
					var namespaceManager = new XmlNamespaceManager(doc.NameTable);
					namespaceManager.AddNamespace("es", "http://everydaysolutions.de/schemas/");
					#endregion

					#region FallbackPath
					var assembliesNode = doc.SelectSingleNode("//es:assemblies", namespaceManager);
					if (assembliesNode != null && assembliesNode.Attributes.Count > 0)
					{
						Microkernel.fallbackPath = assembliesNode.Attributes[0].Value;
					}

					#endregion

					#region LoadTypeMappings
					foreach (XmlNode current in doc.SelectNodes("//es:type", namespaceManager))
					{
						var newMapping = new TypeMapping();
						newMapping.InterfaceName = current.Attributes[0].Value;
						newMapping.MultiImplementationKey = current.Attributes.Count > 1 ? current.Attributes[1].Value : String.Empty;
						newMapping.DeclaringAssembly = current.ChildNodes[0].Attributes[0].Value;
						newMapping.ImplementingType = current.ChildNodes[1].Attributes[0].Value;

						try
						{
							Microkernel.typeMappings.Add(newMapping.InterfaceNameWithMIK, newMapping);
						}
						catch (ArgumentException)
						{
							throw MicrokernelException.DuplicateTypeMapping(newMapping.InterfaceName, newMapping.MultiImplementationKey);
						}
					}
					#endregion

					#region LoadAssemblyMappings
					foreach (XmlNode current in doc.SelectNodes("//es:assembly", namespaceManager))
					{
						var newMapping = new AssemblyMapping();
						newMapping.Fullname = current.Attributes["name"].Value;
						newMapping.PathToFile = current.Attributes["pathToFile"]?.Value ?? file.Directory.FullName;
						newMapping.Filename = current.Attributes["fileName"].Value;

						try
						{
							Microkernel.assemblyMapping.Add(newMapping.Fullname, newMapping);
						}
						catch (ArgumentException)
						{
							throw MicrokernelException.DuplicateAssemblyMapping(newMapping.Fullname);
						}
					}
				}
				#endregion

				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Microkernel.AssemblyResolve);

				if (Microkernel.ConfigurationChanged != null)
				{
					ConfigurationChanged(null, new EventArgs());
				}

				Microkernel.isInitialized = true;
			}
			catch (XmlSchemaValidationException ex)
			{
				throw MicrokernelException.NotMatchingSchema(ex);
			}
		}
		#endregion

		#region Reset
		/// <summary>
		/// Resets the whole configuration of the microkernel and puts it in a not initialized state
		/// </summary>
		public static void Reset()
		{
			Microkernel.assemblyMapping = new SortedList<String, AssemblyMapping>();
			Microkernel.typeMappings = new SortedList<String, TypeMapping>();
			Microkernel.isInitialized = false;
		}
		#endregion

		#region CreateInstance
		public static InterfaceType CreateInstance<InterfaceType>()
			where InterfaceType : class
		{
			return Microkernel.CreateInstance<InterfaceType>(String.Empty);
		}
		#endregion

		#region CreateInstance
		/// <summary>
		/// Creates the instance.
		/// </summary>
		/// <typeparam name="InterfaceType">The type of the nterface type.</typeparam>
		/// <param name="multiImplementationKey">The multi implementation key.</param>
		/// <returns></returns>
		/// <remarks>
		/// Initialization is done in a single methods and not in seperate methods to
		/// remarkebly improve speed.
		/// </remarks>
		public static InterfaceType CreateInstance<InterfaceType>(String multiImplementationKey)
			where InterfaceType : class
		{
			if (!Microkernel.isInitialized)
			{
				throw MicrokernelException.NotInitialized();
			}

			#region GetTypeMapping
			TypeMapping typeMapping = null;
			try
			{
				typeMapping = Microkernel.typeMappings[typeof(InterfaceType).FullName + multiImplementationKey];
			}
			catch
			{
				throw MicrokernelException.TypeOrMikNotMapped(typeof(InterfaceType).FullName, multiImplementationKey);
			}
			if (typeMapping == null)
			{
				throw MicrokernelException.TypeOrMikNotMapped(typeof(InterfaceType).FullName, multiImplementationKey);
			}
			#endregion

			#region GetAssemblyMapping
			AssemblyMapping assemblyMapping = null;
			try
			{
				assemblyMapping = Microkernel.assemblyMapping[typeMapping.DeclaringAssembly];
			}
			catch
			{
				throw MicrokernelException.AssemblyNotMapped(typeMapping.DeclaringAssembly);
			}
			if (assemblyMapping == null)
			{
				throw MicrokernelException.AssemblyNotMapped(typeMapping.DeclaringAssembly);
			}

			var assembly = Assembly.LoadFile(assemblyMapping.File.FullName);
			#endregion

			#region GetType
			var targetType = assembly.GetType(typeMapping.ImplementingType);
			if (targetType == null)
			{
				throw MicrokernelException.TypeNotContained(assembly.FullName, typeMapping.ImplementingType);
			}
			#endregion

			#region GetInstance
			var instance = Activator.CreateInstance(targetType);
			if (targetType.GetInterface(typeof(InterfaceType).FullName) == null)
			{
				throw MicrokernelException.TypeDoesNotImplementInterface(targetType.FullName, typeof(InterfaceType).FullName);
			}
			if (!(instance is InterfaceType result))
			{
				var interfaceAssemblyName = typeof(InterfaceType).Assembly.FullName;
				var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
					.Where(runner => runner.FullName == interfaceAssemblyName)
					.Select(runner => runner.Location)
					.ToList();
				throw MicrokernelException.InterfaceTypeIsLoadedFromDifferentLocations(
					typeof(InterfaceType).FullName,
					loadedAssemblies);
			}
			#endregion

			return result;
		}
		#endregion

		#region AssemblyResolve
		/// <summary>
		/// Assemblies the resolve.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		/// <remarks>
		/// Initialization is done in a single methods and not in seperate methods to
		/// remarkebly improve speed.
		/// </remarks>
		private static Assembly AssemblyResolve(Object sender, ResolveEventArgs args)
		{
			if(!Microkernel.assemblyMapping.ContainsKey(args.Name))
			{
				throw MicrokernelException.AssemblyNotMapped(args.Name);
			}

			var mapping = Microkernel.assemblyMapping[args.Name];
			var assemblyFile = mapping.File;

			if (assemblyFile.Exists == false)
			{
				var fullPath = Path.Combine(Microkernel.fallbackPath, mapping.Filename);
				assemblyFile = new FileInfo(fullPath);

				if (assemblyFile.Exists == false)
				{
					var message = String.Format(
						"The file {0} you specified in the es:assembly node with the name {1} does not exist.",
						assemblyFile == null ? String.Empty : assemblyFile.FullName,
						args.Name);

					throw new FileNotFoundException(message);
				}
			}

			return Assembly.LoadFile(assemblyFile.FullName);
		}
		#endregion
	}
}