# Viewz
## Synopsis
[Viewz API](https://viewz.azurewebsites.net) is a RESTful service that takes markdown as input and generates a Wiki on output.
It consumes Markdown by Github and parses the result for a cleaner and more optimal HTML conversion within the context of Wiki page generation. Maximum size of input per conversion is 400KB, which provides a large workspace for large projects.
Client-side operations are handled using Angular, including the Angular HttpClient for consumption of Viewz.

### Additional Technologies
* Utilized Azure Pipelines for continuous integration, delivery, and deployment of Viewz codebase. This was done on initiation of this project in order to increase efficiency of production and lower the probability of fatal errors occurring after production.
* Postman was used to test API functionality and verify responses against expected results.
* Integrated Swagger to produce service contract.
* Accomplished parsing of the returned HTML content with Html Agility Pack.
* Used DockerHub to containerize deployment with Azure App Service.
