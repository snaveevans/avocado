# Project Avocado

## Infrastructure Notes
To add a migration `dotnet ef migrations add {migration name} -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext|IdentityContext}`

To update database with migrations `dotnet ef database update -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext|IdentityContext}`

## JWT Notes
`JwtKey` must be added to the dotnet secrets store for development use `dotnet user-secrets` in the `Avocado.Web` directory.

## Database
To run local database
- First time: `docker run --name avocado-db -e POSTGRES_DB=avocado -e POSTGRES_USER=avocado-user -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres`
- Following: `docker start avocado-db`