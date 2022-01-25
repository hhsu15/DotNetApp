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

### Entity Framework

The Microsoft ORM framework.

We will need the package manager. first, search `nuget gallery` in extensions in VS code and instll it. Once done, we will use Command Palette and type `nuget`. Select the first one that pops up. Now you will be able to obtain libraries for C#!

Here, we want to install `Microsoft.EntityFrameworkCore.SQlite`. By doing so, you will find in `API.csprog` file now shows the package reference.

**tips for VS Code**

- if you type `prop` and enter it will generate properties for now
- When you get the red error line, click on the bolb icon and it provides a list of fixes or options to auto gerneate code for you. Like generating a constructor etc.
- use `CMD + P` you can quickly search for a file so you don't have to look for it

### Setting up SQL connection

In the example, we will use SQLlite for our database. See `appsetting.Development.json` for configuration. The code will be in `Startup.cs`.
Then, we will need to download dotnet-ef using the command from this [link](nuget.org/packages/dotnet-ef/). Basically the command looks like this:

```bash
dotnet tool install --global dotnet-ef --version 6.0.1
```

After we will make a migration which is going to create a database.
Run this command:

```bash

dotnet ef miggrations add InitalCreate -o Data/Migrations
```

After running this you will get an error saying "Your startup project 'API' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again."

Ok, go the command palette and open nuget gallary and search for Microsoft.EntityFrameworkCore.Design.

Now install it and then once you check API.csproj what's going to happen is this will be added as a reference in your project file (API.csproj). Now if you return the migration command it should work. Great!

So the commpand used is not to actually create the database but rather create a bunch of classes under the `Data/Migrations` folder which will later on allow us to set up the databse or tear down the database at will.

#### create database

Now you can run below command to create a db.

```bash
dotnet ef database update
```

Now the database is created. To see the database and tables, we download the extension called "SQLite".
Once download and you can run command palette and type SQLite to open the database. Then you will find some files have been created.

You can then find that under Explorer there is a section called SQLITE EXPLORER. From there you can right click to run Query. We first use it to insert some records into the table.

### Use Async code

For the api endpoint functions the best practice is to use make them asynchronous. This is can be done using the `async` keyword with `await`.

## Angular

First just make sure we have node.js greater than v16.10.

Install angular:

```bash

npm install -g @angular/cli@12
```

To create an angular project, run

```bash
ng new client --strict  false
```

Now, go to the extensions and install `Angular Language Service`.

To run the app, simply run

```bash

ng serve
```

### Interpolation

Interpolation in angular means that you can render the data from `something.component.ts` file to the `something.component.html` using double curly brackets like {{title}}

### Some VS code extension

1. Angular Language Service
2. Angular Code snippets
3. Bracket Pair colorizer

### HttpClient

For angular app to make a http request we do something like this:

```typescript
// in app.component.ts
// onInit is a lifecycle hook that gets called after data bound properties are created
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any;

  // constructor with HttpClient obj
  constructor(private http: HttpClient) {}

  // this is required method for OnInit
  ngOnInit() {
    this.getUsers();
  }

  // this is an async fucntion. This gets your "observable" which you then have to "subscirbe"
  // to get the response.
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(
      response => {
        this.users = response; // you then override the properties
      },
      error => {
        console.log(error);
      }
    );
  }
}
```

#### CORS

In order to solve the issue for CORS, on the server side, we need to add this, refer to `Srartup.cs`:

```
services.AddCors();
```

### Angular Bootstrap

Install angular bootstrap 4

```bash
ng add ngx-bootstrap
```

#### Install font-awesome

```
npm install font-awesome
```

### Use HTTPS in Mac (SSL)
