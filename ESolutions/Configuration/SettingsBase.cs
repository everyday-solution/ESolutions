using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Security.Cryptography;
using System.Configuration;
using System.Xml;

namespace ESolutions.Configuration
{
	/// <summary>
	/// Baseclass to enable encrypted values in web.config/app.config sections
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="System.Configuration.IConfigurationSectionHandler" />
	public abstract class SettingsBase<T> : IConfigurationSectionHandler
		where T : ISettings, new()
	{
		//Properties
		#region Helper
		/// <summary>
		/// Gets the helper.
		/// </summary>
		/// <value>
		/// The helper.
		/// </value>
		protected Rijndael Helper
		{
			get
			{
				Rijndael result = new Rijndael();
				result.EncryptionSecret = new T().A;
				result.EncryptionIV = new T().B;
				return result;
			}
		}
		#endregion

		#region Default
		/// <summary>
		/// Returns the applications default settings object.
		/// </summary>
		/// <value>The default.</value>
		public static T Default
		{
			get
			{
				return (T)ConfigurationManager.GetSection(new T().SectionName);
			}
		}
		#endregion

		//Methods
		#region Create
		/// <summary>
		/// Creates a configuration section handler.
		/// </summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>The created section handler object.</returns>
		public object Create(object parent, object configContext, XmlNode section)
		{
			T result = new T();
			result.Initialize(section);
			return result;
		}
		#endregion
	}
}
