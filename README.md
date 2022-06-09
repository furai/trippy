# Trippy

Application's aim is to provide a portal for carpooling. Drivers should be able to post offers which users can apply for.

## Requirements

* System installation of `dotnet:6.0.300` and `dotnet-ef:6.0.5`.
* On linux install `libgdiplus` which is required for PDFs.
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
    * Gembox.Pdf; Version: 17.0.1158
    * HarfBuzzSharp.NativeAssets.Linux; Version: 2.8.2


## How to develop

* Run `docker compose up --build` in the root of the project. This should be enough to create database.
* `dotnet watch --project TrippyWeb run` will run the application in development watch mode.
* If your database is empty it will be automatically populated with schema if it's run in development mode.
* Alternatively you can use migrations.


## Troubleshoot

We've noticed that sometimes user doesn't get created. To fix this connect to running mariadb instance by using `docker compose exec mariadb /bin/bash` and then execute `mysql`. This should give you full permissions over database. Once done execute:

```
CREATE USER 'trippy'@'%' IDENTIFIED BY 'trippy';
GRANT ALL PRIVILEGES ON *.* TO 'trippy'@'%';
```
