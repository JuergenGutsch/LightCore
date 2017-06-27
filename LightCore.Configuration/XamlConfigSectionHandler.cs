#if false
using System.Configuration;
using System.IO;
using System.Windows.Markup;
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
        /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
        /// <returns>The configuration.</returns>
        public TConfiguration GetInstance<TConfiguration>()
        {
            return (TConfiguration)_lightCoreConfigSection;
        }

        /// <summary>Creates a configuration section handler.</summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var stringReader = new StringReader(section.OuterXml);
            var xmlReader = XmlReader.Create(stringReader);

            _lightCoreConfigSection = XamlReader.Load(xmlReader);

            return this;
        }
    }
}
#endif