# ReactRoastDotnet

A coffee shop website using React with Vite and React Router, and ASP.NET with Entity Framework and Authentication.

## Live Demo

https://react-roast.azurewebsites.net/swagger/index.html

**Note:** Please allow application to finish loading. Long loading time is due to
a [cold start](https://azure.microsoft.com/en-us/blog/understanding-serverless-cold-start/)
where the server and the database are being instantiated after being inactive for more than 10 minutes.

You can register an account and copy the token given to the swagger's authorize button.

You can also use the provided demo account:
<br />
Email: `demo@gmail.com `
<br />
Password: `P@ssw0rd`

## Description

A coffee shop website where people can order coffee drinks to-go. Users can order as a register user 
where they can view their previous orders. Guest users can also order to-go when they provide their 
email and name.

## Tools
* C# and [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
* Typescript and [React](https://react.dev) with [Vite](https://vitejs.dev)
* Entity Framework
* Identity Core
* Postgresql
* Azure Web App deployment
* OpenApi/Swagger
* React Router
* Tailwind
* Daisy UI
* Redux

## Local Setup

*Note*:
You must set up a local environment with [Postgresql](https://www.postgresql.org) and change the following in
file appsettings.Development.json and replace {} with your own postgresql settings:

```
Server=localhost;Port={};User Id={};Password={};Database=app
```

1. Clone this repository:
    ```
    git clone https://github.com/AmielCyber/ReactRoastDotnet
    ```
2. Go to the API repository
    ```
    cd ReactRoastDotnet/ReactRoastDotnet.API
    ```
3. Download and install NuGet dependencies:
    ```
    dotnet restore
    ```
4. Build the .NET application:
   ```
   dotnet build
   ```
5. Create SQLite database:
   ```
   dotnet ef database update --project ../ReactRoastDotnet.Data
   ```
6. Test the application with SwaggerUI:
   ```
   dotnet run
   ```

## Database Schema

![SQL Draw Database Schema](/Assets/AppDBSchema.png)

## To Do
- [ ] Guest orders
- [ ] User management like password and email change and also closing their account.
- [ ] API testing
- [ ] React Front-end application 
