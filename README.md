# Toybot
The repository for the custom bot created for IbxToychat

## How to run

#### Prerequisites

- .NET 5.0 (Most people already have this installed, otherwise VS or Rider will install the SDK for you)
- [Visual Studio](https://visualstudio.com) or [Rider](https://jetbrains.com/rider) or any other .NET IDE
- [PostgresSQL Server](https://www.postgresql.org/download/)

#### Steps

1. Clone the repo.
2. In the Toybot folder (the directory which contains Program.cs), create a new file named `appsettings.json`.
3. Paste the following contents into the file:
```json
{  
  "Logging": {  
  "LogLevel": {  
  "Default": "Information",  
  "Microsoft": "Warning",  
  "Microsoft.Hosting.Lifetime": "Information"  
  }  
 },  "ConnectionStrings": {  
  "DefaultConnection": "Server=localhost;Port=5432;Database=Your database name;"  
  },  
  "DiscordToken": "Your token here"  
}
```
4. Open Toybot.sln in your IDE and hit the run button, NuGet will restore dependencies and the app should run.
