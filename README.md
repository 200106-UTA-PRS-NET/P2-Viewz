# Viewz
## Synopsis
[Viewz API](https://viewz.azurewebsites.net) is a RESTful service built on [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) that takes markdown as input and generates a Wiki on output.
It consumes [Markdown](https://developer.github.com/v3/markdown/) by Github and parses the result for a cleaner and more optimal HTML conversion within the context of Wiki page generation. Maximum size of input per conversion is 400KB, which provides a large workspace for large projects.
Client-side operations are handled using [Angular](https://angular.io/), including the Angular HttpClient for consumption of Viewz.

## Additional Technologies
* Performed version control with [GitHub](https://github.com/200106-UTA-PRS-NET/P2-Viewz)
* Utilized Azure Pipelines for continuous integration, delivery, and deployment of Viewz codebase. This was done on initiation of this project in order to increase efficiency of production and lower the probability of fatal errors occurring after production.
* Performed static analysis via [SonarCloud](https://sonarcloud.io/) 
* Tested API functionality with [Postman](https://www.postman.com/) and Xunit to verify responses against expected results.
* Integrated Swagger to produce service contract.
* Accomplished parsing of the returned HTML content with Html Agility Pack.
* Used DockerHub to host containerized deployment of the front end to be consumed by Azure App Service.
