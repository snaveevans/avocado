# Project Avocado

## Infrastructure Notes
To add a migration `dotnet ef migrations add {migration name} -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext|IdentityContext}`

To update database with migrations `dotnet ef database update -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext|IdentityContext}`

## JWT Notes
`JwtKey` must be added to the dotnet secrets store for development use `dotnet user-secrets` in the `Avocado.Web` directory.