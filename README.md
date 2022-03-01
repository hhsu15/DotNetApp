# ASP.NET App with Angular

## Installation

- Download .Net Core: <https://dotnet.microsoft.com/en-us/>
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

### Use HTTPS for Augular SSL

Refer to the vedio section 3-28.
Basically you will have/generate two files like: server.crt, server.key

<details>

<summary>for Mac</summary>

1. Double click on the certificate (server.crt)
2. Select your desired keychain (login should suffice)
3. Add the certificate
4. Open Keychain Access if it isn’t already open
5. Select the keychain you chose earlier
6. You should see the certificate localhost
7. Double click on the certificate
8. Expand Trust
9. Select the option Always Trust in When using this certificate
10. Close the certificate window and you should be asked to enter password!

The certificate is now installed.

</details>

<details>
  <summary>For windows</summary>

1. Double click on the certificate (server.crt)
2. Click on the button “Install Certificate …”
3. Select whether you want to store it on user level or on machine level
4. Click “Next”
5. Select “Place all certificates in the following store”
6. Click “Browse”
7. Select “Trusted Root Certification Authorities”
8. Click “Ok”
9. Click “Next”
10. Click “Finish”

</details>

Once certificate is installed, we need to go to `angular.json` and add the following in "server":

```json
{
  "server": {
    "options": {
      "sslCert": "./ssl/server.crt",
      "sslKey": "./ssl/server.key",
      "ssl": true,
      "browserTarget": "test:build"
    }
  }
}
```

Then, re-run the API server and angular app.

## Server side API Learning Goaals

In this one, we will learn:

- How to store password in BD
- Use inheritance in C#
- Use debugger
- Use DTO (data transfer objects)
- Validation
- JWT
- Use services in C#
- Middleware for authentication
- Extension methods

## Authentication

The idea is hasing the password and on top of that, use something called salting to scramble the hashed password to make it more secure. For the real world app, we will be using ASP.NET Core Identity which is widely used and battle hardened.

Refer to `API.Entities.AppUser`

Once the new properties are added you need to run migrations again:

```bash
dotnet ef migrations add UserPasswordAdded
```

Then the class will be added to the `Migrations`.

Then you need to update the actual db table by running:

```bash
dotnet ef database update

```

Then you can go to the database (use the command palette search for "SQL")

## VS Code debugger

In `.vscode/launch.json`, you can see under `configuration`, there is this:

```json
{
  "name": ".NET Core Attach",
  "type": "coreclr",
  "request": "attach"
}
```

Then you can go _Run and Debug_ button and select ".Net Core Attached".

For Windows, then you would search some keyword "API" and it will attach to the .exe file and then once you click on it, it will start the debugger where the process stops at your breakpoint.

### DTO

In order to receive the properties from the request body (as opposed to query string) we need to use DTO(Data transfer property). Also you can hide certain properties of an object so it's a good idea to return to the client with DTO.

Make sure when you send the requst from Postman, use the type JSON(application/json), otherwise you get 415 error.

### Add validation for data parameters

One of the features of BaseApiController attribute is to automatically provide validation for the attributes sent to the endpoint.

You can use something called "Data validation" in the DTO. You use the "[Required]" attribute. Then you can do all kinds of validation such as Regex, length etc.

```cs
// a simple requred
[Required]
public string Username { get; set; }
```

### Clean up the db

```bash
dotnet ef database drop # drop the db
dotnet ef datanase update # this will look and migration and recreate the db

```

## JWT

Scructure: JWT has three parts:

- header
- payload: contains
  - userid
  - nbf(not before date)
  - exp (expiration date)
  - iat (issue at date)
- token signatue (this only part that is really encripted and can be only decrypted by server)

The process for JWT:

- client logs in -> request with id/pw -> server
- server validates credentials and creates a token -> client
- client stores token in browser, and sends JWT (via header) with further requests

Benefits of JWT

- No session to manage - JWTs are self contained tokens (bc it has exp date etc)
- portable - a single token can be used with multiple backends
- no cookie required
- Server does not need to make a query to db to verify user once a token issued (bc it can just use the signature)

### Add TokenService

We create an interface for best practice but the important thing here is to create a separate service whose purpose is only creating the token.
See `Services.TokenService.cs`.

Next, we need to add go to the `Startup.cs` and add the serivce to the dependency injection container so we can take in the `config` from the `Startup` class. We use services.AddScoped. Tells the service how long should the service be alive for after we start the service. For a service we want to just create a token, it does not need to be around forever. Scoped is appropriate since it will be teared down as soon as the request is resolved.

Then, go to the Nuget Gallary and search for `System.IdentityModel.Tokens.JWT`, insall the dependency.

### Add Authentication middleware

!This is very usefule!

To authorize users using attributs such as [AllowAnonymous] which will allow everyone. Refer to `UserController.cs`.

We will need another package for authorization. Go to nuget gallary -> Microsoft.AspNetCore.Authentication.JwtBearer.

Once the dependency is added, be sure to go to `Startup.cs` and use services.AddAuthentication and a bunch of code.

Once the authentication is added, in order to be authenticated, when we make a getUser request we need to add the token to the `headers`, using the Postman with the following

```bash
key: Authorization
value: bearer <token>

```

## Extention methods

C# Extension allows us to create methods for exising class without having to create a derived/inherited class.

Here we extends the IServiceCollection to add some more methods.

Create a folder called `extensions` and then create a `public static class`, inside the class, create a public static method. This way, in the `Startup.cs` you can just do `services.your_method` (be sure to bring in the API.Extensions) to make your code cleaner.

## Client side Angular Learning Goaals

Goals:

- create Angular components
- Use Angular Template forms
- use Angular services
- Understand Observables
- Use Angular structural directives to conditionally display elements
- Component communication

### Create Components

Use this commnad to see the things you can generate wiith angular command

```bash

ng g -h  # g means --generate
```

To create a component, do the following. Make sure you do this under the `src/app` folder

```bash
ng g c {name}  --skip-tests# c means component and skip creating test files

```

By doing so, it will create a folder with the files and also add the component to the `app.module.ts`.

#### Use Boostrap

We can cheat a little bit...

For example, to build a `nav bar`, we can just go to getbootsrap.com and serarch in the examples. When you find a good one, then open inspector and grap the <nav> element with its class :P.

### Angular FormsModule

Use Angular FormsModule. Go to the `add.module.ts` and put in the imports.

In the component.html file, you can make a form an Angular Form by using this:

```typescript
<form
        #loginForm="ngForm"
        (ngSubmit)="login()"
        <button type="submit">Submit</buttion>
</form>
```

the `(ngSubmit)` will let you specify on submit which method from the component should be called.

#### 2-way binding

In Angular, to use the 2-way binding, you need to give it a property called "name", and add `[(ngModel)]="model.your_data"
For example:

```typescript
<input
  name="username"
  [(ngModel)]="model.username"
  type="text"
>
</input>

```

So now you will be able to take the input values from the user from the `model` variable.

### Angular services

The way Angular services work is that you have your service class which is decorated by injectable function. Looks like below. You specify the root it will then be automatically creates an instance and inject into your component where you need this service:

```typescript
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}
```

Then make sure you configure
and then you can use it like this in your component:

```typescript
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // inject service into this component - dependency injection
  constructor(private accountService: AccountService) {}
  ...

```

Refer to `src/app/_services` where you created your \_services folder and run:

```bash
ng g s account --skip-tests
```

This will create an angular service called "account".

Couple of points here for angular services:

- services are injectable. I.e., inject the services into a component
- services are singleton. They will be arond untile say, the browser is closed, even if they are not being used.

### Angular directives

There are angular directives you use in the `*.component.ts` file.
For example:

- \*ngIf="name_of_field" # when the field is a type of boolean

Or things like this:

```html
<form
  #loginForm="ngForm"
  class="form-inline mt-2 mt-md-0"
  (ngSubmit)="login()"
  autocomplete="off"
>
  <input
    name="username"
    [(ngModel)]="model.username"
    class="form-control mr-md-0"
    type="text"
    placeholder="Username"
  />
</form>
```

### ngx-boostrap

https://valor-software.com/ngx-bootstrap/#/components

Add the module in the `app.modules.ts`.

- In our `nav.compponent.html` we use the dropdown directive (it's just standalone "dropdown" in a div tag)

### Observables

- New standard for ES2016 (ES7)
- lazy collections of multiple values over time
- like newsletter, only subscribers of the newsletter receive the newsletter
  - if no one subscribes, then it won't be printed out

#### Observables and RxJS

You can use `observable.pipe(...)` to process the data before it gets to the subscribers. For example:

```typescript
// return an array of member id
getMembers(){
  return this.http.get("api/users").pipe(
    map(members => {
      console.log(member.id)
      return member.id
    })
  )
}

```

#### Async Pipe

Automatically subscribes/unsubscribes from the Observable

```html
<li *ngFor="let number of service.getMembers() | async">{{member.username}}</li>
```
