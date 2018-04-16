using System;
using System.Collections.Generic;
using System.Text;
using ESolutions.Test.Interfaces;

namespace ESolutions.Test.Mocker
{
	public class SimpleMapping : ISimpleMapping
	{
		public Int32 APublicAttribute;

		public void FakeMethod ()
		{
		}
	}
}
