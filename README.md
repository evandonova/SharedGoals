## The “SharedGoals” Project

**Shared Goals Website** is my defense project for **ASP.NET Core MVC** course at [SoftUni](https://softuni.bg/ "SoftUni") (June 2021).

## :pencil2: Overview

**Shared Goals** is a website for goal tracking. Goals can be created, read, edited, deleted and finished. Also, users can work on created goals, e.g. to create a goal work. 

## :performing_arts: User Types

**Administrator** - created from site owner – username: “admin@mail.com”, password: “pass123#”
* Create, read, edit, delete and finish all goals on the site.
* Work on all goals and see their own goal works.
* See all goal works.
* See all users.

**Creator** - logged-in user, who has become a creator through the “Become a Creator” functionality and has a “creator name”
* Read all goals on the site.
* Create goals and edit, delete and finish them. Creators can manage only goals they created!
* See their own goal works from the time before they became a creator.
* Creators cannot work on any goals!

**User** - logged-in user, who is not a creator
* Read all goals on the site.
* Work on all goals and see their own goal works.

**Guest User** - not logged-in user
* Read all goals on the site.

## :hammer: Built With
* ASP.NET [CORE 5.0] MVC
	- 4 controllers + 4 more in the “Admin” area
	- 5 entity models
	- 10 views + 3 more in the “Admin” area + partial views where appropriate
	- Services
	- Service, view and form models
	- etc.
* Entity Framework Core
* Microsoft SQL Server
* MVC Areas
* ASP.NET CORE Site Template
* AutoMapper
* In-Memmory Cache
* TempData messages
* XUnit
* MyTested.AspNetCore.Mvc

## :wrench: DB Diagram




