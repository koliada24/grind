## Lecture 265. Microservices Asynchronous Communication

Key points:
- Asynchronous communication - client sends a request to the server and does not wait for the response. Client should not have been blocked while waiting for the response.
- The most popular protocol for async communication is AMQP - Advanced Message Quering Protocol.
- Using AMQP protocols, client sends the message with the using of message broker (Kafka, RabbitMQ, Nats).
- Noone waiting for the response suddenly.
- If there is busy interaction in microservices, the use asynchronous messaging platforms.

Questions:
1) What is "busy interaction" in microservices?

Answers:
1) Busy interaction - same as chatty interaction - case when a service needs to make a lot of small calls to the service to complete the request
