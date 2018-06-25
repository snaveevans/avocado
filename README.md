

# Dev Infrastructure Notes
To add a migration navigate into infrastructure directory and enter `dotnet ef migrations add {migration name} -s ../Avocado.Web/`

To update database with migrations navigate to infrastructure directory and enter `dotnet ef database update -s ../Avocado.Web/`
