# ReactRoastDotnet
A coffee shop website using React with Vite and React Router, and ASP.NET with Entity Framework and Authentication.

## Local Setup
*Note*:
You must set up a local enviornment with [Postgresql](https://www.postgresql.org) and change the following in 
appsettings.Development.json and replace {} with your settings:
<br />
`Server=localhost;Port={};User Id={};Password={};Database=app`
1. Clone this repository: 
    <br />
    `git clone https://github.com/AmielCyber/ReactRoastDotnet`
    <br />
2. Go to the API repository
    <br />
    `cd ReactRoastDotnet/ReactRoastDotnet.API`
    <br />
3.Download and install NuGet dependencies:
    <br />
    `dotnet restore`
    <br />
3. Build the .NET application: 
    <br />
    `dotnet build`
    <br />
4. Create SQLite database:
    <br />
    `dotnet ef database update --project ../ReactRoastDotnet.Data`
    <br />
5. Test the application with SwaggerUI:
   <br />
   `dotnet run`
   <br />

## Database Schema
![SQL Draw Database Schema](/Assets/AppDbSchema.png)
## To Do
- [ ] API testing
- [ ] Front-end application 
