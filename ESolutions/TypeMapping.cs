using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions
{
	public class TypeMapping
	{
		#region InterfaceName
		public String InterfaceName;
		#endregion

		#region MultiImplementationKey
		public String MultiImplementationKey;
		#endregion

		#region InterfaceNameWithMIK
		public String InterfaceNameWithMIK
		{
			get
			{
				return this.InterfaceName + this.MultiImplementationKey;
			}
		}
		#endregion

		#region DeclaringAssembly
		public String DeclaringAssembly;
		#endregion

		#region ImplementingType
		public String ImplementingType;
		#endregion
	}
}
