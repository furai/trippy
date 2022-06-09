# Trippy

Application's aim is to provide a portal for carpooling. Drivers should be able to post offers which users can apply for.

## Requirements

* Globally installed dotnet and dotnet-ef.
* Docker.
* Bootstrap v5.1.3
* Dotnet libraries:
    * AspNetCore.SassCompiler; Version: 1.52.2
    * Mailjet.Api; Version: 2.0.2
    * Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore; Version: 6.0.5
    * Microsoft.AspNetCore.Identity.EntityFrameworkCore; Version: 6.0.5
    * Microsoft.AspNetCore.Identity.UI; Version: 6.0.5
    * Microsoft.EntityFrameworkCore; Version: 6.0.5
    * Microsoft.EntityFrameworkCore.Design; Version: 6.0
    * Microsoft.EntityFrameworkCore.SqlServer; Version: 6.0.5
    * Microsoft.EntityFrameworkCore.Tools; Version: 6.0
    * Microsoft.VisualStudio.Web.CodeGeneration.Design; Version: 6.0.5
    * Pomelo.EntityFrameworkCore.MySql; Version: 6.0.1
    * Select.HtmlToPdf.NetCore; Version: 22.1.0

## How to develop

* Run `docker compose up --build` in the root of the project. This should be enough to create database.
* `dotnet watch --project TrippyWeb run` will run the application in development watch mode.
* If your database is empty it will be automatically populated with schema if it's run in development mode.
* Alternatively you can use migrations.
