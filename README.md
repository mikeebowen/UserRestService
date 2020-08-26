# UserRestService

This is User REST Service app written in .net core with a code first database created with Entity Framework Core. To test the API import the file `UserRestService.postman_collection.json` in the root of the project.

## Run the API

* Replace the `ctxStr` with your database connection string in `UserDatabase\UserContext.cs`.
  * TODO: get connection string from environment
* Set the Startup Project to `UserDatabase`
* Open the Package Manager console and set the Default Project to `UserDatabase`.
* In the Package Manager console enter the command `Update-Database` to create the database and run the migrations.
* Set the Startup Project to `UserRestService`
* Debug the `UserRestService`.
* Open `UserRestService.postman_collection.json` in Postman
* Create some Users with passwords and then test the API.