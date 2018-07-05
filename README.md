# Project Avocado

## Infrastructure Notes
To add a migration navigate into infrastructure directory and enter `dotnet ef migrations add {migration name} -s ../Avocado.Web/ -c {AvocadoContext}`

To update database with migrations navigate to infrastructure directory and enter `dotnet ef database update -s ../Avocado.Web/ -c {AvocadoContext}`

## JWT Notes
`JwtKey` must be added to the dotnet secrets store for development use `dotnet user-secrets`