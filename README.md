# HSDK
Managed by Eye Soft company.
Goals

Hyperion Software Development Kit (aka HSDK) is a "real world collection of components" for enterprise application development using the latest technologies:
- C#, IO, Task Parallel Library
- NHibernate, Entity Framework, RavenDB
- CommonServiceLocator, Castle Windsor
- WPF, WCF

It has a multilayered architecture and it's made up of several blocks that interact each other. 
However, since their loose coupled nature, you can easily replace or extend them without impact the overall functionality of the application.
Our main goal is to provide a complete out-of-the-box solution to use as blueprint for your applications.
Areas

Developing an enterprise application (even the smallest one) you will surely need common functionality that can be divided in different areas.

For the documentation see the unit tests in the solution.

These are some of the main areas we covered by our Nuget packages:

[EyeOpen.Core](https://www.nuget.org/packages/EyeSoft.Core)

Contracts ensuring, Messanging, LINQ expression parser and extension methods, Task Parallel Library helpers, Type auto-mapping for storage and benchmark.
Tags: contract checking, message broker, LINQ, expression parser, IO, TPL, type auto-mapper, benchmark.

[EyeOpen.Data](https://www.nuget.org/packages/EyeSoft.Data)


Data components to simplify read/write operations on RDBMS.
Tags: data database sql

[EyeOpen.Domain](https://www.nuget.org/packages/EyeSoft.Domain)


Base classes to implement a Domain Model also using DDD (Domain Driven Development) and to abstract the ORM or the storage and change it simply.
Tags: domain model DDD aggregate aggregateroot ORM wrapping

[EyeSoft.Windows.Model](https://www.nuget.org/packages/EyeSoft.Windows.Model)


Simplify the adoption of the MVVM pattern using WPF.
Tags: MVVM AOP DialogService MessageBroker mediator conventions

[EyeSoft.Data.EntityFramework](https://www.nuget.org/packages/EyeSoft.Data.EntityFramework)



A wrapper for EntityFramework to make ORM switch and the adoption of DDD pattern easier.
Tags: entity framework caching tracing SQL database


[EyeOpen.Data.Nhibernate](https://www.nuget.org/packages/EyeOpen.Data.Nhibernate)

NHibernate mapping using System.ComponentModel.DataAnnotations and wrappers for tests and ORM switching.
Tags: nhibernate dataannotations mapping ORM abstraction


[EyeOpen.Testing](https://www.nuget.org/packages/EyeOpen.Testing)

Test helpers for domain and WCF.
Tags: domain WCF testing test TDD MOQ



Every part has been developed using Agile methodologies like Scrum & TDD, so you will find unit tests that cover the most important functionalities. 
In order to speed up your learning, we prepared different samples that higlight the key-features.

RoadMap

Functional and architectural roadmap, explaining which features are implemented and will be available in future.


Bibliography, resourses used during project development and growing.

Get Started


Matteo Migliore
Founder of DevBull - The acadamy to become a software architect - https://devbull.it
Founder of Sviluppatore Migliore - Courses .NET for companies - https://sviluppatoremigliore.com
Founder of LEGALDESK - The n°1 software for italian lawyers - https://legaldesk.it

![alt tag](https://avatars2.githubusercontent.com/u/432974?s=64&v=4)
