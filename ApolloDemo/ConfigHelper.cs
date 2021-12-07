using Com.Ctrip.Framework.Apollo;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace ApolloDemo
{
    public class ConfigHelper
    {
        private static IConfigurationRoot _instance;

        public static IConfigurationRoot Instance
        {
            get => _instance;
        }

        public void Build()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();

            InitFileConfig(configBuilder);
            InitApolloConfig(configBuilder);

            _instance = configBuilder.Build();
        }

        private void InitFileConfig(IConfigurationBuilder builder)
        {
            var rootPath = Assembly.GetExecutingAssembly().Location;
            var files = new DirectoryInfo(Path.GetDirectoryName(rootPath)).GetFiles("*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (file.Name.StartsWith("appsettings"))
                    builder.AddJsonFile(file.FullName);
            }
        }

        private void InitApolloConfig(IConfigurationBuilder builder)
        {
            var config = builder.Build();

            var apolloConfig = builder.AddApollo(config.GetSection("Apollo")).AddDefault();

            var json = config.GetSection("Apollo:JsonNamespace").Get<string[]>();
            var xml = config.GetSection("Apollo:XMLNamespace").Get<string[]>();
            var property = config.GetSection("Apollo:PropertyNamespace").Get<string[]>();

            if (json != null)
                foreach (var item in json)
                    apolloConfig.AddNamespace(item, Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Json);
            if (xml != null)
                foreach (var item in xml)
                    apolloConfig.AddNamespace(item, Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Xml);
            if (property != null)
                foreach (var item in property)
                    apolloConfig.AddNamespace(item, Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Properties);
        }
    }
}
