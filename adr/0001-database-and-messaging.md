# 0001: Database and Messaging

## Status
Accepted

## Context
The project requires a reliable relational database and a message broker to coordinate work between bounded contexts. The database must be widely supported in the .NET ecosystem and integrate well with tooling. For messaging, we need a broker and library that provide durable delivery and simple integration with .NET services.

## Decision
Use **Microsoft SQL Server** as the primary relational database and **RabbitMQ** with **MassTransit** for asynchronous messaging.

## Consequences
- SQL Server becomes a core dependency and influences deployment and local development environments.
- RabbitMQ and MassTransit add operational overhead but give us reliable, durable messaging and a rich ecosystem.
- Future changes to database or messaging technologies will require new ADRs to capture the trade‑offs.
