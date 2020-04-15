# Settings
Read user configuration settings from file

To store user and application settings separate from source code (and source code repositories) move API keys, connection strings,
and other static key value pairs to configuration files.

The appsettings.json and Configuration class are scaffolded into most .NET Core web applications. This example is intended to 
demonstrate and document the same capability in a minimal console application.


## 1. Start with a simple console application.
The example is built on .NET 3.1, other targets should also work. Your actual mileage may vary.

## 2. Add dependency (Nuget package manager) for 
- Microsoft.Extensions.Configuration  (v3.1.3 is current as of this document)
- Microsoft.Extensions.Configuration.Json  (v3.1.3)
- Microsoft.Extensions.Configuration.Binder (v3.1.3) to bind getsection to class model

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
     - create new GUID (UUID) 
     - create directory in %APPDATA%\Microsoft\UserSecrets\[GUID]\
     - add secrets.json file
