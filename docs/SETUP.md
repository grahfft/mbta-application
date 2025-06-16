# Set up and Run the Mbta Service

## Setting the service up
This service uses the environment variable `MbtaApiKey` that can be found when logging into Open Weather.

This is used to make queries against OpenWeather so while the service will start up without this variable, you won't be able to send commands to Open Weather

To set up the environment variable either in your current terminal or in your `~/.bashrc` or `~./zshrc` file add:
```export MbtaApiKey="YOUR_ID_GOES_HERE"```

## Running the service

the commands below expect you to be running this at the solution level

### Building

The solution is set up to run a build of all project in the solution. Running the build command will produce a build for the WeatherApplication project

```dotnet build```

### Run
The server can also be built by running the below command against it. It's not the primary function as this command is used to start up the service locally.

```dotnet run --project MbtaService/MbtaService.csproj```

An alternative to this command is to go into the project directory and execute the run command

```
cd MbtaService

dotnet run
```

Either command should start the server up on `localhost:5283`

### Swagger

You can use the below swagger link to verify the service is up and running

```http://localhost:5283/```