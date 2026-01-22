## Lecture 143. Microservices synchronous and asynchronous communication

There are two main types of communication between microservices: synchronous and asynchronous.
- Syncronous communication is a type of communication when one service sends a request to the other service and waiting for the response.
- Asynchronous communication means there is a queuq of requests usually configured using message brokers like Kafka and RabbitMQ through which the services send requests to each other.
  
  
Microservices synchronous communication and best practices.
- The client sends the request to another service with using http protocol and waits for the response.
- The synchronous communication protocols can be http ot https.
- Request/response communication with HTTP or HTTPS and REST API (extends gRPC and GraphQL).
  
  
Here are the most popular ways of communication and their most popular usecases:
- REST HTTP APIs - for exposing endpoints from microservices for external services.
- gRPC - for communication between internal services.
- GraphQL - for sending structured flexible data.
- WebSockets - for real-time bi-directional comunication