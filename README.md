# ReactRoastDotnet

A coffee shop website using React with Vite and React Router, and ASP.NET with Entity Framework and Authentication.

## Live Demo

**Note:** Please allow application to finish loading. Long loading time is due to
a [cold start](https://azure.microsoft.com/en-us/blog/understanding-serverless-cold-start/)
where the server and the database are being instantiated after being inactive for more than 10 minutes.


You can use the provided demo account if you do not want to create an account:
<br />
Email: `demo@gmail.com `
<br />
Password: `P@ssw0rd`

### React Client Application
https://react-roast.azurewebsites.net

### ASP.NET Swagger/OpenAPI Specification
https://react-roast.azurewebsites.net/swagger/index.html
You can register an account or sign in with the demo account provided above and copy the token given to the swagger's
authorize button.

## Preview 
![Desktop Preview](/Assets/DesktopPreview.gif)

![Mobile Preview](/Assets/MobilePreview.gif)

## Technology Stack

### Backend Application
<div style="display: flex; flex-wrap: wrap; gap: 5px">
    <img alt="C Sharp" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg"/>
    <img alt="Dotnet Core" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg"/>
    <img alt="Azure" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/azure/azure-original.svg"/>
    <img alt="PostgreSQL" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original.svg"/>
    <img alt="SQLite" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/sqlite/sqlite-original.svg"/>
</div>

* C# and [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) Web API
* Tested with Xunit
* [Microsoft Azure Web App Deployment](https://azure.microsoft.com/en-us/products/app-service/web)
* PostgreSQL
* SQLite for testing
* [OpenApi/Swagger](https://www.openapis.org)

### Frontend Application
<div style="display: flex; flex-wrap: wrap; gap: 5px">
    <img alt="React" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg"/>
    <img alt="TypeScript" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg"/>
    <img alt="Jest" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/jest/jest-plain.svg"/>
    <img alt="HTML" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-original.svg"/>
    <img alt="CSS" width="30px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-original.svg"/>
</div>

* [React](https://react.dev) with TypeScript and [Vite](https://vitejs.dev)
* Tested with [ViTest](https://vitest.dev)/Jest and [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/)
* React Router
* [Tailwind CSS](https://tailwindcss.com)
* [DaisyUI](https://daisyui.com)

## To Do
- [ ] Toasts for server responses
- [ ] Save cart state for authenticated users in the database
- [ ] Save cart state in local storage or in a cookie
- [ ] Save authenticated user's token in cookies
- [ ] Account page menu for viewing order history or edit account
- [ ] Order history page
- [ ] View receipt on order confirmation
- [ ] User management like password and email change and also closing their account.

## Description

A coffee shop website where people can order coffee drinks to-go. Users can order as a register user 
where they can view their previous orders. Guest users can also order to-go when they provide their 
email and name.

## Tools
* C# and [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
* Typescript and [React](https://react.dev) with [Vite](https://vitejs.dev)
* Entity Framework
* Identity Core
* [PostgreSQL](https://www.postgresql.org)
* [Azure Web App deployment](https://azure.microsoft.com/en-us/products/app-service/web)
* [OpenApi/Swagger](https://swagger.io)
* React Router
* [Tailwind](https://tailwindcss.com)
* [Daisy UI](https://daisyui.com)
* ~~Redux~~ [Zustand](https://github.com/pmndrs/zustand)

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

