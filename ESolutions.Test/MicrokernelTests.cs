using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using ESolutions.Test.Helper.Contracts;
using ESolutions.Test.Helper;
using ESolutions.Test.Interfaces;
using ESolutions.Test.Mocker;

namespace ESolutions.Test
{
	/// <summary>
	/// Run tests only in DEBUG settings
	/// </summary>
	[TestClass]
	public class MicrokernelTests
	{
		#region MicrokernelTests
		public MicrokernelTests()
		{
			Microkernel.Initialize(new FileInfo("Microkernel.Mappings.xml"));
		}
		#endregion

		//Methods
		#region CreateInstanceWithoutDependencies
		/// <summary>
		/// Tests the correct creation of a simple stand-alone class without any dependencies
		/// </summary>
		[TestMethod]
		public void CreateInstanceWithoutDependencies()
		{
			var actual = Microkernel.CreateInstance<ISimpleMapping>();

			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(Mocker.SimpleMapping));
		}
		#endregion

		#region CreateInstanceWithMappedDependency
		[TestMethod]
		public void CreateInstanceWithMappedDependency()
		{
			var actual = Microkernel.CreateInstance<IDependencyMapping01>();

			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(Mocker.DependencyMapping01));
		}
		#endregion

		#region CreateInstanceWithIndirectDependencyMapped
		[TestMethod]
		public void CreateInstanceWithIndirectDependencyMapped()
		{
			Microkernel.CreateInstance<IDependencyMapping02>();
		}
		#endregion

		#region CreateInstanceWithoutTypeMapping
		[TestMethod]
		public void CreateInstanceWithoutTypeMapping()
		{
			try
			{
				var instance = Microkernel.CreateInstance<IDependencyMapping03>();
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				var expected = MicrokernelException.TypeOrMikNotMapped("ESolutions.Test.Interfaces.IDependencyMapping03", "");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region CreateInstanceWithoutAssemblyMapping
		[TestMethod]
		public void CreateInstanceWithoutAssemblyMapping()
		{
			try
			{
				var instance = Microkernel.CreateInstance<IDependencyMapping04>();
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				var expected = MicrokernelException.AssemblyNotMapped("ESolutions.Test.Unmapped, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region CreateInstanceWithoutAssemblyContainingType
		[TestMethod]
		public void CreateInstanceWithoutAssemblyContainingType()
		{
			try
			{
				var instance = Microkernel.CreateInstance<IDependencyMapping05>();
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				var expected = MicrokernelException.TypeNotContained("ESolutions.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ESolutions.Test.Mocker.DependencyMapping05");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region CreateInstanceNotImplementingInterface
		[TestMethod]
		public void CreateInstanceNotImplementingInterface()
		{
			try
			{
				var instance = Microkernel.CreateInstance<IDependencyMapping06>();
				Assert.Fail("Expected exception was not thrown");
			}
			catch (MicrokernelException actual)
			{
				var expected = MicrokernelException.TypeDoesNotImplementInterface("ESolutions.Test.Mocker.DependencyMapping06", "ESolutions.Test.Interfaces.IDependencyMapping06");
				Assert.AreEqual(expected.Message, actual.Message);
			}
			catch (Exception)
			{
				Assert.Fail("Expected exception was not thrown");
			}
		}
		#endregion

		#region CreateInstanceWithIndirectDependency
		[TestMethod]
		public void CreateInstanceWithIndirectDependency()
		{
			Microkernel.CreateInstance<IDependencyMapping07>();
		}
		#endregion
	}
}
