## Create your database and schema:
Install Microsoft SQL Server, change Server name and Authentication of ConnectionStrings.DefaultConnection in appsettings.json file.
.Net Cli:
```bash
dotnet ef database update
```
Visual studio Power shell:
```
Update-Database
```
## Run BE: 
Press F5, choose Debug > Start with debugging from the Visual Studio menu, or select the green Start arrow and project name on the Visual Studio toolbar.
