# Starships

API client that consumes starship from swapi (star wars api), caches it with
Redis, and measures the response time. This API is swagger/swashbuckle enabled.

Technology
----------
* ASP.NET Core 2.0
* Redis
* Swashbuckle

Endpoints
---------

| Method     | URI                                  | Action                                      |
|------------|--------------------------------------|---------------------------------------------|
| `GET`      | `/api/starships`                     | `Retrieve all starships`                     |
| `GET`      | `/api/starships/{id}`                | `Retrieve starship`                          |


Sample Usage
---------------

`http get http://localhost:5000/api/starships`
```
"networks": [
    {
        "Id": 15, 
        "Name": "Executor"
    }, 
    {
        "Id": 5, 
        "Name": "Sentinel-class landing craft"
    }, 
    {
        "Id": 9, 
        "Name": "Death Star"
    }, 
...
```
logged to console after first request:  
`retrieved starships from remote api in 610ms`

logged to console after second request:  
`retrieved starships from cache in 3.8ms`

Run
---
If you have docker installed,
```
docker-compose build
docker-compose up
Go to http://localhost:5000 and visit one of the above endpoints
```

Alternatively, you will need the .NET Core 2.0 SDK. If you have the SDK installed,
then open `appsettings.json` and point the connection string to your server,
then run:
```
dotnet restore
dotnet run
Go to http://localhost:5000 and visit one of the above endpoints (or /swagger)
```
