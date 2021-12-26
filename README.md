# ASP.NET App with Angular

## Installation

- Download .Net Core: https://dotnet.microsoft.com/en-us/
  - after installation, run `dotnet --info` to verify.
- Install Node.js

## Get started

```bash

dotnet new -h # to see all the templates

# first to create a solution file
dotnet new sln

# then, create a webapi folder
dotnet new webapi -o API

# then, add the API project to solution
dotnet sln add API
```

### Set up VS code for C# dev

- Install extension for C#
- In command pallette, search for Assets and run the command
- Install something called `C# extensions`
- Install `Material Icon Theme` from extension
- Go to settings and use `exclude` for patterns like `**/obj` and `**/bin` to hide those folders since we never need to modify them - they just get auto-recreated.
