## Create your database
Install Microsoft SQL Server, replace Server name and Authentication of ConnectionStrings.DefaultConnection in appsettings.json file (BE folder).

Create database by using command:

.Net Cli:
```bash
dotnet ef database update
```
or Visual studio Power shell:
```
Update-Database
```
## Run BE
Replace your SendGrid API key and Sendgrid Template in appsettings.json file (BE folder).

Onpen BE folder with Visual Studio, press F5, choose Debug > Start with debugging from the Visual Studio menu, or select the green Start arrow and project name on the Visual Studio toolbar.


## Remark
There are 3 main roles: "Administrator", "Staff" and "User" and some APIs require role in this project. 

You can set role for user by using Microsoft SQL Server. In the future, API for setting user role will be added soon.

For understand APIs, developer can read API specifications via swagger: https://localhost:44312/swagger/index.html. You can change the name of domain with modifying appsettings.json file in BE folder