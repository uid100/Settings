![icon](https://raw.githubusercontent.com/uid100/Settings/master/images/settings.png)
---
# Settings
Read user configuration settings from file

To store user and application settings separate from source code (and source code repositories) move API keys, connection strings,
and other static key value pairs to configuration files.

The appsettings.json and Configuration class are scaffolded into most .NET Core web applications. This example is intended to 
demonstrate and document the same capability in a minimal console application.

Read configuration elements or bind to model classes, including across multiple sources. More detailed information here:

_Recommended reading!_  https://ballardsoftware.com/adding-appsettings-json-configuration-to-a-net-core-console-application/


## 1. Start with a simple console application.
The example is built on .NET 3.1, other targets should also work. Your actual mileage may vary.

## 2. Add dependency (Nuget package manager) for 
- Microsoft.Extensions.Configuration  (v3.1.3 is current as of this document)
- Microsoft.Extensions.Configuration.Json  (v3.1.3)
- Microsoft.Extensions.Configuration.Binder (v3.1.3) needed only to bind getsection to class models
- Microsoft.Extensions.Configuration.UserSecrets (v3.1.3) needed only if using User Secrets

## 3. Add appsettings.json
- Right-click in solution explorer to access properties
- set __Advanced>Copy to Output Directory:  Copy if newer__

<pre>
{
  "ice cream": {
    "flavor": {
      "best_seller": "chocolate"
    },
    "topping": "none"
  },
  "pizza": {
    "sizes": [
      "large",
      "larger",
      "you are kidding me!"
    ]
  },
  "MenuSpecials": [
    {
      "Number": 1,
      "Feature": "double-double",
      "Side": "fries",
      "Beverage": "coke"
    },
    {
      "Number": 2,
      "Feature": "ch.burger",
      "Side": "fries",
      "Beverage": "coke"
    },
    {
      "Number": 3,
      "Feature": "burger",
      "Side": "fries",
      "Beverage": "coke"
    }
  ],
  "Location":
  {
    "address": "123 Main St.",
    "city": "Oakdale",
    "state": "TX"
  }
}
</pre>

## 4. User Secrets
- "hide" secrets
     - create new GUID (UUID)  ( ex. https://www.guidgenerator.com/ )
     - create directory in %APPDATA%\Microsoft\UserSecrets\[GUID]\
     - add secrets.json file
- add the GUID to .csproj file <PropertyGroup><UserSecretsId> and reload project!

<pre>
  <PropertyGroup>
      <UserSecretsId>af2e3160-fe75-4ca9-97f8-89d99eb43a4d</UserSecretsId>
  </PropertyGroup>
</pre>

code
<pre>
namespace Settings
{
    public class Menu
    {
        public int Number { get; set; }
        public string Feature { get; set; }
        public string Side { get; set; }
        public string Beverage { get; set; }
    }
}
</pre>

<pre>
using System;
using Microsoft.Extensions.Configuration;

namespace Settings
{
    class Program
    {
        static void ReadSingleValueFromAppsettings()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = configBuilder.Build();

            string x = configuration.GetValue<string>("ice cream:flavor:best_seller");
            Console.WriteLine("\n\t{0}\n", x);
        }

        static void ReadConfigurationFromAppsettings()
        {
            var appConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = appConfig.Build();

            // read the section and bind to class array elements
            Menu[] menu = configuration.GetSection("MenuSpecials").Get<Menu[]>();

            Console.WriteLine('\n');
            foreach (var m in menu)
                Console.WriteLine($"\t#{m.Number}. {m.Feature}");
            Console.WriteLine($"\n\tAll specials include {menu[0].Side} and {menu[1].Beverage}\n\n");
        }

        static void ReadUserSecret()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();
            IConfiguration configuration = configBuilder.Build();

            string x = configuration.GetValue<string>("Vendor:ApiKey");
            Console.WriteLine($"\n\t{x}\n");
        }


        static void Main(string[] args)
        {
            ReadSingleValueFromAppsettings();
            ReadConfigurationFromAppsettings();
            ReadUserSecret();
        }
    }
}
</pre>
