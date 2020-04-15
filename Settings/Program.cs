using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Settings
{
    class Program
    {
        static void ReadConfiguration()
        {
            var appConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = appConfig.Build();

            string[] items = configuration.GetSection("Pizza:size").Get<List<string>>();
            foreach (var s in items) Console.WriteLine(s);

        }

        static void Main(string[] args)
        {
            ReadConfiguration();
        }
    }
}
