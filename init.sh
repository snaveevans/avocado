# First initial install of npm to ensure it runs npm
npm install --prefix Avocado.Web/ClientApp
# ./npmStart.sh # runs npm i and then start in a new process

# Setup a postgres docker and migrate the tables to it - if this fails, run the following commands to remove the container and re-create it
# `docker ps` - copy container ID and replace [container ID] in `docker stop [411b2c4fd3e9]` then `docker rm [411b2c4fd3e9]`
docker run --name avocado-db -e POSTGRES_DB=avocado -e POSTGRES_USER=avocado-user -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres
dotnet ef database update -p ./Avocado.Infrastructure/ -s ./Avocado.Web/ -c AvocadoContext

# Install dotnet dev-certs for https and then trust them
dotnet tool install --global dotnet-dev-certs #will warn if already installed
dotnet dev-certs https # linux
dotnet dev-certs https --trust # mac/windows