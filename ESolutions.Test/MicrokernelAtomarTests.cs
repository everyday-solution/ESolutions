using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ESolutions.Test
{
	/// <summary>
	/// These test must all be executed one after another. Since the microkernel makes use of the current AppDomain
	/// the necessary reinitialization of the microkernel can not be performed in one test run that only has one AppDomain.
	/// </summary>
	[TestClass]
	public class MicrokernelAtomarTests
	{
		#region InitializeOk
		/// <summary>
		/// Tests that initialize can be called in a valid case and all events are fired.
		/// </summary>
		[TestMethod]
		public void InitializeOk()
		{
			Boolean eventFired = false;
			Microkernel.ConfigurationChanged += (s, e) => { eventFired = true; };
			Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Atomar01.xml"));
			Assert.IsTrue(eventFired);
		}
		#endregion

		#region NotInitialized
		/// <summary>
		/// Tests that an exception is thrown if the microkernel was not initialized before calling CreateInstance
		/// </summary>
		[TestMethod]
		public void NotInitialized()
		{
			try
			{
				Microkernel.Reset();
				Object mocker = Microkernel.CreateInstance<Object>();
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				MicrokernelException expected = MicrokernelException.NotInitialized();
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region InitializeTwice
		/// <summary>
		/// Tests that an exception is thrown if the Initialize method is called more than once.
		/// </summary>
		[TestMethod]
		public void InitializeTwice()
		{
			Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Atomar01.xml"));
			Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Atomar01.xml"));
		}
		#endregion

		#region InitializeNonSchemaConformFile
		/// <summary>
		/// Tests that an exception is thrown if the mappings-file is not conform with the Microkernel.Mappings.xsd.
		/// </summary>
		[TestMethod]
		public void InitializeNonSchemaConformFile()
		{
			try
			{
				Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Invalid.xml"));
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				MicrokernelException expected = MicrokernelException.NotMatchingSchema(actual);
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region InitializeWithNotExistingFile
		/// <summary>
		/// Tests that an exception is thrown if the specified mappings file does not exist.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void InitializeWithNotExistingFile()
		{
			Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Unexist.xml"));
		}
		#endregion

		#region InitializeWithDuplicateTypeMappings
		[TestMethod]
		public void InitializeWithDuplicateTypeMappings()
		{
			try
			{
				Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Atomar02.xml"));
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				MicrokernelException expected = MicrokernelException.DuplicateTypeMapping("ESolutions.Test.Interfaces.ISimpleMapping", "");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region InitializeWithDuplicateAssemblyMappings
		[TestMethod]
		public void InitializeWithDuplicateAssemblyMappings()
		{
			try
			{
				Microkernel.Initialize(new FileInfo("Microkernel.Mappings.Atomar03.xml"));
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				MicrokernelException expected = MicrokernelException.DuplicateAssemblyMapping("ESolutions.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion
	}
}