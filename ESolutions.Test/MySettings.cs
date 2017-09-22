using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ESolutions.Test
{
	/// <summary>
	/// Test class simulating a custom section in config.
	/// </summary>
	/// <seealso cref="ESolutions.Configuration.SettingsBase{ESolutions.Test.MySettings}" />
	/// <seealso cref="ESolutions.Configuration.ISettings" />
	public class MySettings : Configuration.SettingsBase<MySettings>, Configuration.ISettings
	{
		//Properties
		#region A
		public String A
		{
			get
			{
				return "17a8eba9-2c66-4cee-84bd-0aa41daca120";
			}
		}
		#endregion

		#region B
		public String B
		{
			get
			{
				return "274a3e46-c497-42de-afb9-19fb75da2d68";
			}
		}
		#endregion

		#region SectionName
		public String SectionName
		{
			get
			{
				return "mytestsettings";
			}
		}
		#endregion

		#region TestValue
		public String TestValue
		{
			get;
			private set;
		}
		#endregion

		//Methods
		#region Initialize
		public void Initialize(XmlNode section)
		{
			this.TestValue = this.Helper.Decrypt(section["testvalue"].InnerText);
		}
		#endregion
	}
}
