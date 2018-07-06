# Project Avocado

## Infrastructure Notes
To add a migration navigate into infrastructure directory and enter `dotnet ef migrations add {migration name} -s ../Avocado.Web/ -c {AvocadoContext|IdentityContext}`

To update database with migrations navigate to infrastructure directory and enter `dotnet ef database update -s ../Avocado.Web/ -c {AvocadoContext|IdentityContext}`

## JWT Notes
`JwtKey` must be added to the dotnet secrets store for development use `dotnet user-secrets` in the `Avocado.Web` directory.

## Authentication
### Google
Register 2 redirectUri's `http://{host}/api/token/google/register` & `http://{host}/api/token/google/login` in google's console api.

Add the following `dotnet user-secrets` for Google Authentication 
 - `Authentication:Google:RedirectURI` exclude **/register** & **/login** from the uri
 - `Authentication:Google:ClientSecret`
 - `Authentication:Google:ClientId`