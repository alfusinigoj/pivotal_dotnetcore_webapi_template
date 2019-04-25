## Pivotal .NetCore WebApi Template
- ```dotnet new``` template includes
    - .Net Core Web Api
    - Microsoft Logging Abstractions
        - Console logging
        - Debug logging
    - Microsoft Configuration 
        - Json
        - Environment variables
        - VCAP Services
        - Config Server
    - Steeltoe
        - Service Discovery
        - Health Actuators
    - Fluent
        - Custom Model Validation
    - Mediator Pattern
    - Custom Model Binding
    - Autofac IoC
    - Unit and Integration tests
        - XUnit
        - Moq
        - Microsoft.AspNetCore.TestHost
    - Sample AzureDevops pipeline
    
## Steps to install the template
- Compile the solution
- Navigate to `Pivotal.NetCore.WebApi.Template` folder and execute `./nupublish.bat %1` where `%1` is the minor version number of the package 
- The above command will add a package to `C:\MyLocalNugetRepo`
- Run `dotnet new -i Pivotal.NetCore.WebApi.Template` if uou have added `C:\MyLocalNugetRepo` as one of your package sources, else use the command `dotnet new -i C:\MyLocalNugetRepo\Pivotal.NetCore.WebApi.Template\Pivotal.NetCore.WebApi.Template.nupkg` to install from the nuget file on the file system
- You can verify the installation using `dotnet new -l` command
- To create a scaffold project from this template, you can execute `dotnet new pvtlwebapi -n <PROJECT_NAME>`

For more details about templating, please refer to https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates
