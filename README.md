# Dental System

![build status badge](https://github.com/stoian2662/DentalScheduler/actions/workflows/dotnet.yml/badge.svg)
<!-- https://github.com/fkhoda/checkout-shoppinglist-api -->

## Description

This repository contains demo source code of my **Dental System** project realized with ASP.NET Core following clean architecture.

**Dental System** is a system providing appointment scheduling for dental services. Clients can easily request appointment to their desired Dental Team, track history, etc. It supports various Dental Teams and Treatment services.

## Feature Highlights

### Identity

Supports main identity features - user registration (only for clients of the dental system), login and profile management.

### Scheduling

* Request for treatment session - only for clients of the dental system.
* Approval/Reject/Rearrangement of treatment session - only for dental workers of the dental system.
* History tracking.

## Architecture

This architecture follows a **use case driven approach**. See more about [screaming architecture](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html).

![Dental Scheduler Architecture](/Assets/dental-scheduler-archtecture.svg)

### Domain

<img src="/Assets/dental-scheduler-archtecture-domain.svg" width="450" />

This layer is a place for all entities. They are basic building blocks for the system.

### Application

<img src="/Assets/dental-scheduler-archtecture-application.svg" width="450" />

#### Use Cases

This layer contains application-specific business rules. Therein lies the code that specifies what a project actually does. It's a home for all Use Cases (also known as Interactors).

#### Boundaries

The second kind of building blocks which will always reside in this layer is an Interface (also known as Port/Gateway). These are abstractions over anything that sits in the layers between the **Use Cases** and the outer layers **Infrastructure** and **Presentation (Web API and Web UI)** - **Use Cases Ports** to Presentation and **Infrastructure Gateways** for Use Cases.

### Infrastructure

<img src="/Assets/dental-scheduler-archtecture-infrastructure.svg" width="450" />

Contains all the code needed for the project to use goodies from External World.

**Persistence** of the database is sitting in this layer. In this project is used PostgreSQL for the primary data store.This layer also contains the Authentication and Authorization management of users.

### Presentation

<img src="/Assets/dental-scheduler-archtecture-presentation.svg" width="450" />

Presents data to user and handle user interactions.

#### Web API

The Web API of the system. It uses full business logic from use cases.

#### Web UI

The Web UI of the system. It communicates with the Web API to fetch/update data and uses only simple validation logic from use cases.

## Technologies

* [.NET 5](https://dotnet.microsoft.com/)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
* [OData](https://www.odata.org/)
* [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
* [EF Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
* [PostgreSQL](https://www.postgresql.org/)

## Launch

### Prerequisites

To launch the project you need to have [docker](https://www.docker.com/) and [docker-compose](https://docs.docker.com/compose/) installed on your machine. You can get docker from [here](https://docs.docker.com/get-docker/) and docker-compose from [here](https://docs.docker.com/compose/install/).

### Instructions

From the top directory of the solution (where **docker-compose.yml** is located) run the following commands:

1. Build and run. `docker-compose up -d` creates the images and builds and runs the containers of the database and the REST API. It may take a few minutes for the first execution of this command.

```shell
docker-compose up -d
```

Check if containers are up and running with:

```shell
docker-compose ps
```

if any State is not "Up" execute `docker-compose down` and then again `docker-compose up -d`.

2. Get the REST API's IP address. To see the IP address given to the container of the server you need to execute the following script:

```shell
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' dentalsystem-webapi
```

3. Open the REST API. Enter the IP address on the browser's address bar as shown in the example below and you are ready.

![address bar example](Assets/brower_address_bar_example.png)

To stop the running containers execute `docker-compose down`.

```shell
docker-compose down
```
