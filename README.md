<div align="center" style="margin-bottom:20px">
  <img src="https://raw.githubusercontent.com/sajjadsaharkhan/Recommerce/main/assets/recommerce-logo.png" alt="recommerce logo" />
    <div align="center">
                <a href="https://github.com/sajjadsaharkhan/Recommerce/blob/main/README.md"><img alt="build-status" src="https://img.shields.io/github/license/meysamhadeli/booking-modular-monolith?color=%234275f5&style=flat-square"/></a>
      <a href="https://github.com/sajjadsaharkhan/Recommerce/actions/workflows/NET.yml"><img alt="build-status" src="https://github.com/sajjadsaharkhan/Recommerce/actions/workflows/NET.yml/badge.svg?branch=main&style=flat-square"/></a>
    </div>   
</div>


# Recommerce 

The main idea of creating this project is implementing an infrastructure that provides a **product recommendation system** as a service that can be installed independently and locally on e-commerce systems.
The system offers personalized recommendations to users based on various factors such as *customer information, product information, customer behavior, comments and ratings, and similar customer behavior* in the past.
If you are looking for a recommender system that can be installed independently and locally on e-commerce systems, **Recommerce** is a great option. It uses various artificial intelligence techniques to offer personalized recommendations to users based on their behavior, location, and preferences. Additionally, the project uses a range of modern technologies and tools that ensure scalability, performance, and reliability.

<a href="https://gitpod.io/#https://github.com/sajjadsaharkhan/Recommerce"><img alt="Open in Gitpod" src="https://gitpod.io/button/open-in-gitpod.svg"/></a>

## Table of Contents
- [How it Works](#how-it-works)
- [Benefits of Recommender Systems](#benefits-of-recommender-systems)
- [System Requirements](#system-requirements)
- [Challenges](#challenges)
- [The Goals of This Project](#the-goals-of-this-project)
- [Plan](#plan)
- [Technologies - Libraries](#technologies---libraries)
- [The Domain And Bounded Context](#the-Domain-and-bounded-context)
- [Artificial Intelligence Techniques](#artificial-intelligence-techniques)
- [Support](#support)
- [Contribution](#contribution)
- [Architecture Decision Records](#architecture-decision-records)
- [License](#license)
## How it Works
The recommender system offers the best products expected by the user by analyzing the following factors:
-   Customer information: gender, height, weight, device, etc.
-   Product information: categories, brand, size, weight, color, etc.
-   Customer behavior in the system: search history, product visit history, favorite products, wish list, purchase history.
-   Comments and ratings written by users in the system for products or in other words: UGC (user generated contents).
-   System user sessions (page or product visiting, search history, clicks history).
-   History of system transactions (orders, baskets, financial transactions, discounts).
-   Similar customer behavior in the past.
-   Recommendations based on time analysis! For example, on Sunday at 10 am, some products may be offered that are not offered on Wednesday evening.
-   Recommendations based on users location. addresses are based on IPs or addresses that they have accurately registered in the system.

## Benefits of Recommender Systems

Recommender systems can offer a number of benefits, including:

-   **Increased sales:**  Recommender systems can help to increase sales by suggesting products that users are likely to be interested in. This can lead to more purchases and higher revenue.
-   **Improved customer satisfaction:**  Recommender systems can help to improve customer satisfaction by providing users with personalized recommendations. This can make users feel valued and appreciated, which can lead to repeat business.
-   **Reduced churn:**  Recommender systems can help to reduce churn by identifying users who are at risk of leaving and providing them with personalized recommendations that are likely to keep them engaged.
-   **Increased engagement:**  Recommender systems can help to increase engagement by providing users with interesting and relevant content. This can lead to users spending more time on the site and interacting with the content.

**Effects of Recommender Systems on Sales and Revenue**

Recommender systems can have a significant impact on sales and revenue. A study by McKinsey found that recommender systems can increase sales by up to 20%. Another study by Forrester Research found that recommender systems can increase revenue by up to 10%.

The effects of recommender systems on sales and revenue vary depending on a number of factors, such as the type of product or service being sold, the size of the customer base, and the level of competition. However, in general, recommender systems can have a positive impact on sales and revenue.


## System Requirements
There are a few key requirements for recommender systems to be effective:

-   **Data:**  Recommender systems require data about users and products in order to make recommendations. This data can come from a variety of sources, such as purchase history, search history, and product ratings.
-   **Computational power:**  Recommender systems can be computationally expensive to run, especially for large datasets. Therefore, it is important to have the necessary computational resources available.
-   **Algorithms:**  There are a variety of algorithms that can be used to build recommender systems. The choice of algorithm will depend on the specific requirements of the system.
-   **Evaluation:**  It is important to evaluate the performance of recommender systems to ensure that they are effective. This can be done by measuring metrics such as accuracy, coverage, and diversity.

## Challenges 
**Challenges of Implementing a Recommender System in Medium-Sized Projects**

There are a number of challenges that can be faced when implementing a recommender system in a medium-sized project. These include:

-   **Limited resources:**  Medium-sized projects often have limited resources, both in terms of time and money. This can make it difficult to implement a complex recommender system.
-   **Little knowledge and experience:**  Medium-sized projects often have little knowledge and experience in recommender system implementation. This can make it difficult to choose the right algorithm and to implement the system effectively.
-   **Data quality:**  The quality of the data used to train a recommender system is critical to its success. Medium-sized projects may not have access to high-quality data, which can limit the effectiveness of the recommender system.
-   **Scalability:**  Recommender systems can be computationally expensive to run, especially for large datasets. Medium-sized projects may not have the necessary infrastructure to scale the recommender system to meet their needs.
-   **User privacy:**  Recommender systems often collect and use personal data about users. It is important to take steps to protect user privacy when implementing a recommender system.

Despite these challenges, there are a number of ways to overcome them and implement a successful recommender system in a medium-sized project. These include:

-   **Choose the right algorithm:**  There are a number of different algorithms that can be used to build recommender systems. It is important to choose the right algorithm for the specific needs of the project.
-   **Use data from multiple sources:**  Data from multiple sources can be used to improve the accuracy of a recommender system. Medium-sized projects should try to collect data from as many sources as possible.
-   **Use a scalable architecture:**  A scalable architecture is essential for a recommender system that needs to be able to handle a large number of users and items. Medium-sized projects should use a scalable architecture to ensure that their recommender system can grow with their business.
-   **Protect user privacy:**  It is important to take steps to protect user privacy when implementing a recommender system. This includes collecting only the data that is necessary, anonymizing data where possible, and using secure storage and transmission methods.

By taking these steps, medium-sized projects can overcome the challenges of implementing a recommender system and build a system that is effective and meets their needs.
**And we provide these steps ready and open source.**

## The Goals of This Project

- :sparkle: Using `Clean Architecture` for architecture level.
- :sparkle: Using `Message Broker` on top of `Cap` for `Event Driven Architecture` between our services.
- :sparkle: Using `Inbox Pattern` on top of `Cap` for ensuring message idempotency for receiver and `Exactly once Delivery`. 
- :sparkle: Using `Outbox Pattern` on top of `Cap` for ensuring no message is lost and there is at `Least One Delivery`.
- :sparkle: Using `CQRS` implementation with `MediatR` library.
- :sparkle: Using `PostgreSQL` for database in our project.
- :sparkle: Using `Event Store` for `write side` of Transactions to store all `historical state` of aggregate.
- :sparkle: Using `Unit Testing`, `Integration Testing` for testing level.
- :sparkle: Using `Fluent Validation` and a `Validation Pipeline Behaviour` on top of `MediatR`.
- :sparkle: Using `Docker-Compose`, `GitHub Actions` and `Kubernetes` for our deployment mechanism.


## Plan

> 🌀This project is a work in progress, new features will be added over time.🌀

I will try to register future goals and additions in the [Issues](https://github.com/sajjadsaharkhan/Recommerce/issues) section of this repository.

High-level plan is represented in the table

| Feature           | Status         |
| ----------------- | -------------- |
| Entities and Db       | Done ✔️   |
| Data Insert and Update mechanism | Done ✔️|
| AI Analytics    | ToDo 🚧   |
| Recommendation System Services   | Done ✔️   |

## Technologies - Libraries

- ✔️ **[`.NET 6`](https://dotnet.microsoft.com/download)** - .NET Framework and .NET Core, including ASP.NET and ASP.NET Core
- ✔️ **[`API Versioning`](https://github.com/microsoft/aspnet-api-versioning)** - Set of libraries which add service API versioning to ASP.NET Web API, OData with ASP.NET Web API, and ASP.NET Core
- ✔️ **[`EF Core`](https://github.com/dotnet/efcore)** - Modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations
- ✔️ **[`MediatR`](https://github.com/jbogard/MediatR)** - Simple, unambitious mediator implementation in .NET.
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
- ✔️ **[`Serilog`](https://github.com/serilog/serilog)** - Simple .NET logging with fully-structured events
- ✔️ **[`Scrutor`](https://github.com/khellang/Scrutor)** - Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
- ✔️ **[`EasyCaching`](https://github.com/dotnetcore/EasyCaching)** - Open source caching library that contains basic usages and some advanced usages of caching which can help us to handle caching more easier.
- ✔️ **[`Mapster`](https://github.com/MapsterMapper/Mapster)** - Convention-based object-object mapper in .NET.
- ✔️ **[`Hellang.Middleware.ProblemDetails`](https://github.com/khellang/Middleware/tree/master/src/ProblemDetails)** - A middleware for handling exception in .Net Core
- ✔️ **[`MagicOnion`](https://github.com/Cysharp/MagicOnion)** - gRPC based HTTP/2 RPC Streaming Framework for .NET, .NET Core and Unity.
- ✔️ **[`EventStore`](https://github.com/EventStore/EventStore)** - The open-source, functional database with Complex Event Processing.
- ✔️ **[`Masstransit`](https://github.com/MassTransit/MassTransit)** - MassTransit is free software/open-source .NET-based Enterprise Service Bus.
- ✔️ **[`GitHub Actions`](https://github.com/features/actions)** - GitHub Actions makes automate all your software workflows, now with world-class CI/CD. Build, test, and deploy your code right from GitHub

## The Domain And Bounded Context

- `Presentation`: The Presentation service handles API endpoints for the recommender system.
- `Services`: The Services project contains the system's business logic.
- `Data`: The Data project defines domain entities and manages database interactions.
- `Infrastructure`: The Infrastructure project provides shared extensions and resources for all modules.

## Artificial Intelligence Techniques

Logic uses various artificial intelligence techniques to analyze customer behavior and transaction data to identify similar customers or products and make personalized recommendations. These techniques include:

-   **Collaborative filtering**: Analyzing customer behavior and transaction data to identify similar customers or products and make recommendations based on their preferences.
-   **Content-based filtering**: Analyzing product attributes such as category, brand, and price to make recommendations based on customer preferences.
-   **Association rule mining**: Analyzing transaction data to identify products that are frequently bought together and make recommendations based on these associations.
-   **Deep learning-based models**: Using deep learning models such as neural networks to analyze customer behavior and make personalized recommendations.

## Structure of Project
In this project I used [clean architecture](https://jasontaylor.dev/clean-architecture-getting-started/), and I used [feature folder structure](http://www.kamilgrzybek.com/design/feature-folders/) to structure my files.

we used [RabbitMQ](https://github.com/yang-xiaodong/Savorboard.CAP.InMemoryMessageQueue) as my MessageBroker for `async` communication between modules using the eventual consistency mechanism. Each modules uses [MassTransit](https://github.com/MassTransit) to interface with [RabbitMQ](https://www.rabbitmq.com/) for easy use messaging, availability, reliability, etc.

modules are `event based` which means they can publish and/or subscribe to any events occurring in the setup. By using this approach for communicating between modules, each module does not need to know about the other module or handle errors occurred in other modules.

Instead of grouping related action methods in one controller, as found in traditional ASP.net controllers, I used the [REPR pattern](https://deviq.com/design-patterns/repr-design-pattern). Each action gets its own small endpoint, consisting of a route, the action, and an `IMediator` instance (see [MediatR](https://github.com/jbogard/MediatR)). The request is passed to the `IMediator` instance, routed through a [`Mediatr pipeline`](https://lostechies.com/jimmybogard/2014/09/09/tackling-cross-cutting-concerns-with-a-mediator-pipeline/) where custom [middleware](https://github.com/jbogard/MediatR/wiki/Behaviors) can log, validate and intercept requests. The request is then handled by a request specific `IRequestHandler` which performs business logic before returning the result.

The use of the [mediator pattern](https://dotnetcoretutorials.com/2019/04/30/the-mediator-pattern-in-net-core-part-1-whats-a-mediator/) in my controllers creates clean and [thin controllers](https://codeopinion.com/thin-controllers-cqrs-mediatr/). By separating action logic into individual handlers we support the [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single_responsibility_principle) and [Don't Repeat Yourself principles](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself), this is because traditional controllers tend to become bloated with large action methods and several injected `Services` only being used by a few methods.

I used CQRS to decompose my features into small parts that makes our application:

- Maximize performance, scalability and simplicity.
- Easy to maintain and add features to. Changes only affect one command or query, avoiding breaking changes or creating side effects.
- It gives us better separation of concerns and cross-cutting concern (with help of mediatr behavior pipelines), instead of bloated service classes doing many things.

Using the CQRS pattern, we cut each business functionality into vertical slices, for each of these slices we group classes (see [technical folders structure](http://www.kamilgrzybek.com/design/feature-folders)) specific to that feature together (command, handlers, infrastructure, repository, controllers, etc). In our CQRS pattern each command/query handler is a separate slice. This is where you can reduce coupling between layers. Each handler can be a separated code unit, even copy/pasted. Thanks to that, we can tune down the specific method to not follow general conventions (e.g. use custom SQL query or even different storage). In a traditional layered architecture, when we change the core generic mechanism in one layer, it can impact all methods.

# Support

If you like my work, feel free to:

- ⭐ this repository. And we will be happy together :)

Thanks a bunch for supporting me!

## Contribution

Thanks to all [contributors](https://github.com/sajjadsaharkhan/Recommerce/graphs/contributors), you're awesome and this wouldn't be possible without you! The goal is to build a categorized community-driven collection of very well-known resources.
**Contributing Guidelines**

When contributing to the project, please follow these guidelines:
-   Use the latest version of the code.
-   Make sure your changes are well-tested.
-   Document your changes.
-   Follow the coding style guidelines.
-   Be respectful of other contributors.

We follow the standard GitHub flow for pull requests, which means that your changes will be reviewed and discussed before being merged into the main repository.

#### Documentation 
If you would like to contribute to the project's documentation, you can do so by submitting a pull request with your changes. This includes updating the README.md file or adding documentation to the codebase.

#### Testing
Another way to contribute to the project is by writing tests. This helps ensure that the system is working correctly and prevents regressions. You can write tests using the testing frameworks already set up in the project, and you can also add new tests to cover additional use cases.
#### Code Reviews 
If you are not able to contribute code or documentation, you can still contribute by reviewing pull requests and providing feedback. This helps ensure that the codebase is maintainable and adheres to best practices.

We appreciate all contributions to the project, big or small. Thank you for helping us make Logic a better product recommender system! 
**Thank you for your contributions! ❤️**

## Architecture Decision Records

Architecture decisions are documented in the [adr](adr/) directory.

- [0001-database-and-messaging](adr/0001-database-and-messaging.md)

## License
This project is made available under the MIT license. See [LICENSE](https://github.com/sajjadsaharkhan/Recommerce/blob/main/LICENSE) for details.
