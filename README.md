# .Net Book Store Web API
 - An API is designed in this repository, where CRUD(Create, Read, Update,Delete) operations are made.
---
## Used Techs
- .Net 6 WebAPI
- Fluent Validation
- AutoMapper
- Entity Framework Core
- Dependency Injection
- Bearer Token
- Swagger

# Create Token
- First, create user : https://localhost:7151/api/user
- Create access token for user :  https://localhost:7151/api/user/connect/token
- Create refresh token for user : https://localhost:7151/api/user/refreshToken?token={refreshToken}
## Run Project
```
cd webapi
dotnet watch run
```
Runs the app in the development mode. Open http://localhost:{port} to view it in your browser.


