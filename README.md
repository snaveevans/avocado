# Project Avocado

## Prerequisites

- docker >= `18.06.1-ce`
- node >= `8.11.3`
- npm >= `6.4.1`
- dotnet sdk >= `2.1.403`

## Installation

1. clone repo
2. set env variables in `.bash_profile`, `.zshrc` or equivalent startup script
   ```
   export ASPNETCORE_ENVIRONMENT=Development
   export ASPNETCORE_HTTPS_PORT=5001
   export NODE_ENV=development
   ```
3. source your startup script (ie `source ~/.zshrc`)
   - you can also close your shell(s) and open new shell instances
4. make init script executable `chmod +x init.sh`
5. (optional) edit `hosts` file add entry for local development `127.0.0.1 local.tylerevans.co`
6. run init script `./init.sh`
7. Follow [Running](#Running)

## Running

1. start `avocado-db` if it's not running already `docker start avocado-db`
2. run `npm run watch` to watch & rebuild while developing locally
3. navigate to https://localhost:5001 or https://local.tylerevans.co:5001 (if you added the hosts entry)

## Infrastructure Notes

To add a migration `dotnet ef migrations add {migration name} -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext}`

To update database with migrations `dotnet ef database update -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c {AvocadoContext}`

## JWT Notes

`JwtKey` must be added to the dotnet secrets store for development use `dotnet user-secrets` in the `Avocado.Web` directory.

## Database

To run local database

- First time: `docker run --name avocado-db -e POSTGRES_DB=avocado -e POSTGRES_USER=avocado-user -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres`
- Following: `docker start avocado-db`
