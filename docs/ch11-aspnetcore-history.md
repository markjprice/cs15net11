# History of Microsoft web-related development technologies

ASP.NET Core is part of a history of evolving Microsoft technologies used to build websites and services that work with data:
- ActiveX Data Objects (ADO) was released in 1996 and was Microsoft’s attempt to provide a single set of Component Object Model (COM) components to work with data. With the release of .NET, an equivalent was created named ADO.NET, which is still the faster method to work with data in .NET, with its core classes, DbConnection, DbCommand, and DbDataReader. Object-Relational Mappers (ORMs) like EF Core use ADO.NET internally.
- Active Server Pages (ASP) was released in 1996 and was Microsoft’s first attempt at a platform for dynamic server-side execution of website code. I have included this bullet so that you understand where the ASP initialism comes from because it is still used today in modern ASP.NET Core.
- ASP.NET Web Forms was released in 2002 with the .NET Framework and was designed to enable non-web developers, such as those familiar with Visual Basic, to quickly create websites by dragging and dropping visual components and writing event-driven code in Visual Basic or C#. Web Forms should be avoided in new .NET Framework web projects in favor of ASP.NET MVC.
- Windows Communication Foundation (WCF) was released in 2006 and enables developers to build SOAP and REST services. SOAP is powerful but complex, so it should be avoided unless you need advanced features, such as distributed transactions and complex messaging topologies.
- ASP.NET MVC was released in 2009 to cleanly separate the concerns of web developers between models, which temporarily store the data; views, which present data using various formats in the UI; and controllers, which fetch a model and pass it to a view. This separation enables improved reuse and unit testing.
- ASP.NET Web API was released in 2012 and enables developers to create HTTP services (aka REST services) that are simpler and more scalable than SOAP services.
- ASP.NET SignalR was released in 2013 and enables real-time communication for websites by abstracting underlying technologies and techniques, such as WebSockets and long polling. This enables website features, such as live chat, and updates to time-sensitive data, such as stock prices, across a wide variety of web browsers, even when they do not support an underlying technology, such as WebSockets.
- ASP.NET Core was released in 2016 and combines modern implementations of .NET Framework technologies, such as MVC, Web API, and SignalR, with alternative technologies, such as Razor Pages, gRPC, and Blazor, all running on modern .NET. Therefore, ASP.NET Core can execute across platforms. ASP.NET Core has many project templates to get you started with its supported technologies.

> **Good practice**: Choose ASP.NET Core to develop websites and web services because it includes web-related technologies that are modern and cross-platform.

# Classic ASP.NET and IIS versus modern ASP.NET Core and Kestrel

Until modern .NET, ASP.NET was built on top of a large assembly in .NET Framework, named System.Web.dll, and it was tightly coupled to Microsoft’s Windows-only web server, named Internet Information Services (IIS). Over the years, this assembly has accumulated a lot of features, many of which are not suitable for modern cross-platform development.

ASP.NET Core is a major redesign of ASP.NET. It removes the dependency on the System.Web.dll assembly and IIS and is composed of modular lightweight packages, just like the rest of modern .NET. Using IIS as the web server is still supported by ASP.NET Core, but there is a better option.

You can develop and run ASP.NET Core applications across platforms on Windows, macOS, and Linux. Microsoft has even created a cross-platform, super-performant web server, named Kestrel, and the entire stack is open source.

ASP.NET Core 2.2 or later projects default to the new in-process hosting model. This gives a 400% performance improvement when hosting in Microsoft IIS, but Microsoft still recommends using Kestrel for even better performance.

In a Build 2025 session on ASP.NET Core and Blazor futures, principal program managers Daniel Roth and Mike Kistler said that the Kestrel server in ASP.NET Core 10 or later has the ability to trim memory dynamically. The feature runs automatically and doesn't need to be enabled or configured manually. Until .NET 10, memory demanded by peak usage in Kestrel-hosted web projects remained in use after demand subsided. You can read more about the automatic eviction from the memory pool at the following link:

https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0#automatic-eviction-from-memory-pool.
