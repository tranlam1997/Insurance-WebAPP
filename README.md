## Create your database and schema:
Install Microsoft SQL Server, replace Server name and Authentication of ConnectionStrings.DefaultConnection in appsettings.json file.

.Net Cli:
```bash
dotnet ef database update
```
Visual studio Power shell:
```
Update-Database
```
## Run BE: 
Replace your SendGrid API key and Sendgrid Template in appsettings.json file

Press F5, choose Debug > Start with debugging from the Visual Studio menu, or select the green Start arrow and project name on the Visual Studio toolbar.
