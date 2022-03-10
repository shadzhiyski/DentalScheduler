# Dental System

[![build status badge](https://github.com/stoian2662/DentalScheduler/actions/workflows/dotnet.yml/badge.svg)](https://github.com/stoian2662/DentalScheduler/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/stoian2662/DentalScheduler/actions/workflows/codeql.yml/badge.svg)](https://github.com/stoian2662/DentalScheduler/actions/workflows/codeql.yml)
[![Lint Code Base](https://github.com/stoian2662/DentalScheduler/actions/workflows/super-linter.yml/badge.svg)](https://github.com/stoian2662/DentalScheduler/actions/workflows/super-linter.yml)
<!-- https://github.com/fkhoda/checkout-shoppinglist-api -->

## Description

This repository contains demo source code of my **Dental System** project following Clean Architecture and BDD (Behavior Driven Development).

**Dental System** is a system providing appointment scheduling for dental services. Clients can easily request appointment to their desired Dental Team, track history, etc. It supports various Dental Teams and Treatment services.

## Feature Highlights

### Identity

Supports main identity features - user registration (only for clients of the dental system), login and profile management.

### Scheduling

* Request for treatment session - only for clients of the dental system.
* Approval/Reject/Rearrangement of treatment session - only for dental workers of the dental system.
* History tracking.

## Technologies

* [.NET 5](https://dotnet.microsoft.com/)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
* [OData](https://www.odata.org/)
* [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
* [EF Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
* [Specflow](https://specflow.org/)
* [PostgreSQL](https://www.postgresql.org/)
* [Docker](https://www.docker.com/)
* [NGINX](https://nginx.org/)

## Architecture

### Components Relationships

Main components of the system are PostgreSQL database, ASP.NET Web API and Blazor WebAssembly(WASM) App. When published, Blazor WebAssembly project produces static files. NGINX is configured to serve these files. Below is a diagram of components relationships:

![Components Relationships](/Assets/components_relationship.png)

### Layers

This architecture follows a **use case driven approach**. See more about [screaming architecture](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html).

![Dental Scheduler Architecture](/Assets/dental-scheduler-archtecture.svg)

### Domain

This layer is a place for all entities. They are basic building blocks for the system.

### Application

#### Use Cases

This layer contains application-specific business rules. Therein lies the code that specifies what a project actually does. It's a home for all Use Cases (also known as Interactors).

#### Boundaries

The second kind of building blocks which will always reside in this layer is an Interface (also known as Port/Gateway). These are abstractions over anything that sits in the layers between the **Use Cases** and the outer layers **Infrastructure** and **Presentation (Web API and Web UI)** - **Use Cases Ports** to Presentation and **Infrastructure Gateways** for Use Cases.

### Infrastructure

Contains all the code needed for the project to use goodies from External World.

**Persistence** of the database is sitting in this layer. In this project is used PostgreSQL for the primary data store.This layer also contains the Authentication and Authorization management of users.

### Presentation

Presents data to user and handle user interactions.

#### Web API

The Web API of the system. It uses full business logic from use cases.

#### Web UI

The Web UI of the system. It communicates with the Web API to fetch/update data and uses only simple validation logic from use cases.

## Launch

### Prerequisites

To launch the project you need to have [docker](https://www.docker.com/) and [docker-compose](https://docs.docker.com/compose/) installed on your machine. You can get docker from [here](https://docs.docker.com/get-docker/) and docker-compose from [here](https://docs.docker.com/compose/install/).

### Instructions

Clone the solution locally. Go to root directory and run the following command:

```shell
docker-compose up -d
```

`docker-compose up -d` builds the images and starts the containers of the database, Web API and Web UI. It may take a few minutes.

After the containers are started, the Web API is accessible on <http://localhost:5555> and the Web UI is accessible on <http://localhost>.

To stop the running containers execute `docker-compose down`.

```shell
docker-compose down
```
