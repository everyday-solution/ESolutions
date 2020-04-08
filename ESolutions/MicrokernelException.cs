using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ESolutions
{
	[global::System.Serializable]
	/// <summary>
	/// For guidelines regarding the creation of new exception types, see
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
	/// and
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
	/// </summary>
	public class MicrokernelException : ConverterException
	{
		//Constants
		#region NotInitializedPattern
		private const String NotInitializedPattern = "The Microkernel has not been initialized yet. Call the Initialize-Method with a valid Mappings-File.";
		#endregion

		#region AlreadyInitializedPattern
		private const String AlreadyInitializedPattern = "The Microkernel has already been initialized. A call to this method is only valid once in a processes lifetime";
		#endregion

		#region NotMatchingSchemaPattern
		private const String NotMatchingSchemaPattern = "The mappings file does not match the desired schema file.";
		#endregion

		#region AssemblyNotMappedPattern
		private const String AssemblyNotMappedPattern = "The assembly {0} is not defined in the provides Mappings-File";
		#endregion

		#region TypeNotContainedPattern
		private const String TypeNotContainedPattern = "The assembly {0} did not contain the type {1}";
		#endregion

		#region TypeDoesNotImplementInterfacePattern
		private const String TypeDoesNotImplementInterfacePattern = "The type {0} does not implement the interface {1}";
		#endregion

		#region InterfaceTypeIsLoadedFromDifferentLocationsPattern
		private const String InterfaceTypeIsLoadedFromDifferentLocationsPattern = "The interface type {0} is defined in an assembly that is loaded twice from different locations: {1}";
		#endregion

		#region DuplicateTypeMappingPattern
		private const String DuplicateTypeMappingPattern= "The interface {0} with the mik {1} has been defined earlier in the Mappings-File. Please remove one mapping.";
		#endregion

		#region DuplicateAssemblyMappingPattern
		private const String DuplicateAssemblyMappingPattern = "The assembly {0} has been defined earlier in the Mappings-File. Please remove one mapping.";
		#endregion

		//Constructors
		#region MicrokernelException
		public MicrokernelException()
		{
		}
		#endregion

		#region MicrokernelException
		public MicrokernelException(string message)
			: base(message)
		{
		}
		#endregion

		#region MicrokernelException
		public MicrokernelException(string message, ConverterException inner)
			: base(message, inner)
		{
		}
		#endregion

		#region MicrokernelException
		public MicrokernelException(string message, Exception inner)
			: base(message, inner)
		{
		}
		#endregion

		#region MicrokernelException
		protected MicrokernelException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
		#endregion

		//Methods
		#region TypeNotContained
		internal static MicrokernelException TypeNotContained(String declaringAssembly, String typeName)
		{
			String message = String.Format(
				MicrokernelException.TypeNotContainedPattern,
				declaringAssembly,
				typeName);

			return new MicrokernelException(message);
		}
		#endregion

		#region AssemblyNotMapped
		internal static MicrokernelException AssemblyNotMapped(String assemblyName)
		{
			String message = String.Format(
						MicrokernelException.AssemblyNotMappedPattern,
						assemblyName);

			return new MicrokernelException(message);
		}
		#endregion

		#region TypeOrMikNotMapped
		internal static MicrokernelException TypeOrMikNotMapped(String interfaceName, String mik)
		{
			String message = String.Format(
				"The interface {0} with the mik {1} is not mapped",
				interfaceName,
				mik);

			return new MicrokernelException(message);
		}
		#endregion

		#region TypeDoesNotImplementInterface
		internal static MicrokernelException TypeDoesNotImplementInterface(String typeName, String interfaceName)
		{
			String message = String.Format(
				MicrokernelException.TypeDoesNotImplementInterfacePattern,
				typeName,
				interfaceName);

			return new MicrokernelException(message);
		}
		#endregion

		#region InterfaceTypeIsLoadedFromDifferentLocations
		internal static MicrokernelException InterfaceTypeIsLoadedFromDifferentLocations(String interfaceName, List<String> locationList)
		{
			String locations = String.Join(Environment.NewLine, locationList);
			String message = String.Format(
				MicrokernelException.InterfaceTypeIsLoadedFromDifferentLocationsPattern,
				interfaceName,
				locations);

			return new MicrokernelException(message);
		}
		#endregion

		#region AlreadyInitialized
		internal static MicrokernelException AlreadyInitialized()
		{
			return new MicrokernelException(MicrokernelException.AlreadyInitializedPattern);
		}
		#endregion

		#region NotInitialized
		internal static MicrokernelException NotInitialized()
		{
			return new MicrokernelException(MicrokernelException.NotInitializedPattern);
		}
		#endregion

		#region NotMatchingSchema
		internal static MicrokernelException NotMatchingSchema(Exception innerEx)
		{
			return new MicrokernelException(MicrokernelException.NotMatchingSchemaPattern, innerEx);
		}
		#endregion

		#region DuplicateTypeMapping
		internal static MicrokernelException DuplicateTypeMapping(String interfaceName, String mik)
		{
			String message = String.Format(MicrokernelException.DuplicateTypeMappingPattern, interfaceName, mik);
			return new MicrokernelException(message);
		}
		#endregion

		#region DuplicateAssemblyMapping
		internal static MicrokernelException DuplicateAssemblyMapping(String assemblyname)
		{
			String message = String.Format(MicrokernelException.DuplicateAssemblyMappingPattern, assemblyname);
			return new MicrokernelException(message);
		}
		#endregion
	}
}
