using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ESolutions.Configuration
{
	/// <summary>
	/// Interface for custom web.config/app.config sections
	/// </summary>
	public interface ISettings
	{
		/// <summary>
		/// Gets the A-Part of the encryption secret.
		/// </summary>
		String A
		{
			get;
		}

		/// <summary>
		/// Gets the B-Part of the encryption secret.
		/// </summary>
		String B
		{
			get;
		}

		/// <summary>
		/// Gets the name of the section in the config-file.
		/// </summary>
		/// <value>
		/// The name of the section.
		/// </value>
		String SectionName
		{
			get;
		}

		/// <summary>
		/// Initializes the properties of the settings class from the xml-config.
		/// </summary>
		/// <param name="section">The xml-section of the config file.</param>
		void Initialize(XmlNode section);
	}
}
