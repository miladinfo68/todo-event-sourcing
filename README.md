# Event Sourcing with CQRS

.Net 6 Event Sourcing implementation with Kafka, Entity Framework Core and PostgreSQL. Commands and Queries are separated based on project. (Project based separation implementation of CQRS)

## Write API (Command Project)

Project domain is a simple Todo Domain. There is my own Event Store implementation. Command project executes following use-case operations.

- **Create Todo**
- **Update Title**
- **Update Content**
- **Change Status**
- **Complete Todo**

### Implementations

- Onion Architecture
- Entity Framework Core - PostgreSQL
- Outbox Pattern
- MediatR
- DbContextHandler

## Outbox Worker (Worker Service)

Reads Command Project's Outbox table and Produces messages for Read API to consume. (There can be many consumers.)

### Implementations

- Worker Service
- Dapper
- Confluent Kafka


## Read API (Query Project)

This project consumes messages and projects to its own database. (MongoDB) Every query project can create its own read model. 
In this project i have worked on same data model with command project.

### Implementations

- Onion Architecture
- MongoDB
- Confluent Kafka
- Worker Service
- MediatR

# Run

## Docker

- To run Kafka, Zookeeper, MongoDB, PostgreSQL and pgAdmin follow this command on Console.

```bash
docker-compose up -d
```

## Migration

- To apply migrations follow this command on Package Manager Console for Write.API. (Set starting project to API and set default project to Infrastructure on Package Manager Console)

```bash
update-database
```

## Kafka

- Topic must be created to be able to consume messages on Read.API. Kafka let us create topics over console but we can create topics with produce requests. For this, start Write.API and OutboxWorker first and Create a Todo item over Write.API. Topic will be created when OutboxWorker read Write.API's Outbox table and Produce the messages.

## PostgreSQL

- Default cridentials are **"admin"** and **"sa1234"**. User and password can be changed over docker-compose.yml.

## pgAdmin

- Default cridentials are **"admin@aspnetrun.com"** and **"admin1234"**. Username and password can be changed over docker-compose.yml.