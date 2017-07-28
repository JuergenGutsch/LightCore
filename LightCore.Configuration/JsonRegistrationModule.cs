using System.IO;
using LightCore.Registration;
using Newtonsoft.Json;

namespace LightCore.Configuration
{
    public class JsonRegistrationModule : RegistrationModule
    {
        public JsonRegistrationModule(string configFilePath = "LightCore.json")
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Config File not Found", configFilePath);
            }
            using (var stream = new FileStream(configFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    Configuration = JsonConvert.DeserializeObject<LightCoreConfiguration>(sr.ReadToEnd());
                }
            }
        }

        public LightCoreConfiguration Configuration { get; }

        public override void Register(IContainerBuilder containerBuilder)
        {
            RegistrationLoader
                .Instance
                .Register(containerBuilder, Configuration);
        }
    }
}
