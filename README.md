Frameworks - Tools - Libraries

ASP.NET Core
ASP.NET MVC 6
Entity Framework 7
Angular 2
Typescript
Gulp
Bower

Installation instructions - Part 1

Install ASP.NET Core according to your development environment from .
Install NPM by installing Node.js.
Install Bower, Gulp, Typescript and Typescript Definition Manager globally by typing the following commands on the console/terminal:
npm install -g bower
npm install -g gulp
npm install -g typescript
npm install -g typings
npm install -g tsd

Installation instructions - Part 2

Download and install Visual Studio 2015 from here.
Open Visual Studio 2015 and install any update related to ASP.NET Core.
Download the source code and open the solution.
By the time you open the solution, VS 2015 will try to restore Nuget, NPM and Bower packages.
In case it fails to restore NPM and Bower packages, open a console and navigate at the src/RockPaperScissors path where the package.json and bower.json files exist. Run the following commands:
npm install
typings install
bower install
gulp

Installation instructions - Part 3

Open appsettings.json file and alter the database connection string to reflect your SQL Server environment.
Run the migrations with the followind commands

dotnet ef migrations add initial
dotnet ef database update


Installation instructions - Part 4

dotnet build
dotnet run

Open localhost:5000