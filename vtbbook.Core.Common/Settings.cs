using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using vtbbook.Core.Common.Environment;

namespace vtbbook.Core.Common
{
    public class Settings : ISettings
    {
        public IConfiguration Configuration;
        public string ContentRootPath { get; set; }

        public Settings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{EnvironmentGetter.Get()}.json", false, true);
            Configuration = configurationBuilder.Build();
        }

        public string GetValue(string name)
        {
            return Configuration[name];
        }

        public T GetValue<T>(string name)
        {
            string value = Configuration[name];
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetSection<T>(string name) where T : class, new()
        {
            return Configuration.GetSection(name).Get<T>();
        }
    }
}
