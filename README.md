# ASP.NET App with Angular

## Installation

- Download .Net Core: https://dotnet.microsoft.com/en-us/
  - after installation, run `dotnet --info` to verify.
- Install Node.js

## Get started

```bash

dotnet --info # will list all the dotnet sdk you installed. The one on the "Host" is the one running

dotnet --help # to see all the commands

dotnet new -l # to see all the templates

# first we want to create a solution file. A sln file is a container for our project
dotnet new sln # will take the name of the folder and create a sln file

# we also want to create a webapi project (for ASP.NET Core Web API)
dotnet new webapi -o API

# then, add the API project to solution
dotnet sln add API
```

### Set up VS code for C# dev

- Install extension for C# for Visual Studio Code
- In command pallette, search for Assets and run the command
- Install something called `C# extensions`
- Install `Material Icon Theme` from extension
- Go to settings and use `exclude` for patterns like `**/obj` and `**/bin` to hide those folders since we never need to modify them - they just get auto-recreated.

## Clasic Hosting vs New Hosting

In this example we need to use the classic hosting model from .Net 5. The .Net 6 generates a different hosting model, namely `program.cs` and `StartUp.cs`.

Also check the `API/Properties/launchSettings.json`. Here you can configure the port running the svc.

Also check the `API.csproj` file, you can potentially turn off the `ImplicitUsings` but since we started with .Net 6 and it's just easier to leave it enabled so we don't run into issues when using statement is not explicitly used. We are, however, going to comment out the `nullable` flag and we want to make sure everything is not nullable.

## Run application

To run our .Net app, go to the API folder and use

```bash
cd API
dotnet run
```

In order to see this on the browser, we need to do a couple of things:

```bash
dotnet dev-certs https --trust
```

_Hint:_ If you run into issues check this [link](https://stackoverflow.com/questions/64017267/dotnet-dev-certs-certificate-not-trusted) out. Remove `localhost` from system in KeyChain Access and rerun everything.

Now, finanlly, when you run:

```
dotnet run
```

Or even better, run:

```bash
dotnet watch run # this will reflect the file change and restart the server
```

and go to localhost:5001, it will not work. The way webapi works is you have to check `API/Controllers/WeatherForeCastControllers` and you will see the Controller's name. Take the part before the word Controller and use it as a path. So basically go to:

```
localhost:5001/weatherforecast
```

And then you should be able to see the data!

### swagger

Webapi is integrated with swagger. Just go to this endpoint: `localhost:5001/swagger`
