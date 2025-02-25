# Kafka Proof of Concept

A .NET 8 solution demonstrating Kafka message production and consumption using Azure Functions.

## Project Structure

- **KafkaPoc.Api**: REST API for producing messages to Kafka
  - Endpoints:
    - POST `/api/message`: Accepts an array of strings and publishes them to Kafka

- **KafkaPoc.Functions**: Azure Functions project for consuming Kafka messages
  - Functions:
    - `KafkaMessageFunction`: Listens to Kafka topic and logs received messages

- **KafkaPoc.Domain**: Contains domain entities
  - Entities:
    - `Message`: Represents the message structure with Id, Content, and Topic

- **KafkaPoc.Infrastructure**: Implementation of message handling
  - Services:
    - `KafkaMessageProducer`: Handles message production to Kafka

## Setup

1. Start Kafka using Docker Compose:
```bash
docker-compose up -d