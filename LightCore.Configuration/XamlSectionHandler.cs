using System.Configuration;
using System.IO;
using System.Xml;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a generic XAML config section handler.
    /// </summary>
    public class XamlConfigSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Contains the silkveil configuration section.
        /// </summary>
        private object _lightCoreConfigSection;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>The configuration.</returns>
        public T GetInstance<T>()
        {
            return (T)this._lightCoreConfigSection;
        }

        /// <summary>Creates a configuration section handler.</summary>
        /// <returns>The created section handler object.</returns>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        public object Create(object parent, object configContext, XmlNode section)
        {
            StringReader stringReader = new StringReader(section.OuterXml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            this._lightCoreConfigSection = System.Windows.Markup.XamlReader.Load(xmlReader);
            return this;
        }
    }
}