## The “SharedGoals” Project

**Shared Goals Website** is my defense project for **ASP.NET Core MVC** course at [SoftUni](https://softuni.bg/ "SoftUni") (June 2021).

## :pencil2: Overview

**Shared Goals** is a website for goal tracking. Goals can be created, read, edited, deleted and finished. Comments may be added to goals, too. Also, users can work on created goals, e.g. to create a goal work. 
* All authorized users have access to see all goals and their details.
* All authorized users can comment on goals.
* Goals may be edited and deleted all the time only by their creator or by the administrator.
* When a goal is deleted, its goal works are deleted, too.
* No one can work on a finished goal.
* All goal works and all users in the administration part are reloaded after 5 minutes because of cache.
* Users can become creators, but creators cannot become users again.

## :performing_arts: User Types

**Administrator** - created from site owner – username: “admin@mail.com”, password: “pass123#”
* Create, read, edit, delete and finish all goals on the site (the administrator is a creator).
* Work on all unfinished goals and see their own goal works.
* Comment on goals.
* See all goal works.
* See all users.
* See all comments.

**Creator** - logged-in user, who has become a creator through the “Become a Creator” functionality and has a “creator name”
* Read all goals on the site.
* Create goals and edit, delete and finish them. Creators can manage only goals they created!
* Comment on goals.
* See their own goal works from the time before they became a creator.
* Creators cannot work on any goals!

**User** - logged-in user, who is not a creator
* Read all goals on the site.
* Work on all goals and see their own goal works.
* Comment on goals.
* Can become a creator.

## :hammer: Built With
* ASP.NET [CORE 5.0] MVC
	- 5 controllers + 5 more in the “Admin” area
	- 6 entity models
	- 11 views + 4 more in the “Admin” area + partial views where appropriate
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
* jQuery
* XUnit
* MyTested.AspNetCore.Mvc

## :clipboard: Test Coverage
![Screenshot_61](https://user-images.githubusercontent.com/69080997/130662171-d1111bac-1902-4490-9641-b290fa5e6a2c.png)



## :wrench: DB Diagram
![image](https://user-images.githubusercontent.com/69080997/130662472-3f54f949-854a-4da9-b237-5bbce0b92084.png)



