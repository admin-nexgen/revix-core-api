The project is currently configured to use an in memory database.

UseInMemoryDatabase=true in the appsettings.json.

Change it to false in order to use a real database.

Navigate to the Revix.Core.Api project and run the following commands to use a postgres database.

Ensure that you have the latest dotnet ef tools setup.

https://docs.microsoft.com/en-us/ef/core/cli/dotnet

dotnet ef database update

Run the api project and it should connect to your local database.