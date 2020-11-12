# TrueLayer Challenge

## The task
We would like you to develop a REST API that, given a Pokemon name, returns its Shakespearean description

## The solution

**Technologies and frameworks used:**
1) ASP.NET Core — provides the means to handle HTTP requests, easily serialize/deserialize data, and perform dependency injection
2) RestSharp — a HTTP client library for communication with REST APIs
3) NUnit, Moq & FluentAssertions — for unit testing

**Running the project**

*With VisualStudio*

Prequisites: VisualStudio 2019, .NET Core 3.1 SDK installed
Steps to run:
1) Open `TrueLayerAssignment.sln` in VisualStudio
2) Set `TrueLayerAssignment.Web` as startup project
3) Select `IIS Express` profile for Windows, or `TrueLayerAssignment.Web` for non-Windows
4) Ctrl+F5 to launch the project `http://localhost:5000/`

*With command line*

Prequisites: .NET Core 3.1 SDK installed
1) Go to web project directory: `cd TrueLayerAssignment.Web`
2) Run the app: `dotnet run`

*With Docker*

Prequisites: Docker installed
1) Build the image: `docker build -t truelayerassignment .`
2) Run the container: `docker run -p 5000:80 truelayerassignment`

**API Endpoint:**  
`GET http://localhost:5000/pokemon/{NAME}/{?VERSION}` - get a Shakespearean description of the Pokemon

*Request parameters:*

| Parameter | Data type | Required | Possible values                                          | Description                                                                                                                                |
|-----------|-----------|----------|----------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| {NAME}    | string    | Yes      | Any string between 1 to 100 charcters in length          | The name of the Pokemon whose description you want to get.                                                                                 |
| {VERSION} | string    | No       | Red, Blue, Yellow, Gold, Silver, Crystal, Ruby, Sapphire | The game version the description should be extracted from.  If not specified, will retrieve the description from the first available game. |

Response codes:
1) 200 — Request is successful. The body contains Pokemon name and Shakespearean description.
2) 400 — Request is unsuccessful. Request parameters are incorrect. The body contains validation errors.
3) 404 — Request is unsuccessful. Pokemon with the requested name was not found, or the description from the requested game version was not found. The body contains error details.
4) 500 — Request is unsuccessful. An unexpected error occurrred in the application.

Examples:
```
Request:
GET http://localhost:5000/pokemon/pikachu

Response:
{
  "name": "pikachu",
  "description": "At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms."
}
```
```
Request:
GET http://localhost:5000/pokemon/charizard/ruby

Response:
{
  "name": "charizard",
  "description": "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself."
}
```
```
Request:
GET http://localhost:5000/pokemon/xyz

Response:
{
  "errorMessage": "Pokemon with name 'xyz' not found"
}
```
