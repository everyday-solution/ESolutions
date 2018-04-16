using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Test.Helper.Contracts;

namespace ESolutions.Test.Mocker
{
	public class DependencyMapping01 : Interfaces.IDependencyMapping01
	{
		#region DependencyMapping01
		public DependencyMapping01 ( )
		{
			IDependency01 dependency = Microkernel.CreateInstance<IDependency01>();
		}
		#endregion
	}
}
