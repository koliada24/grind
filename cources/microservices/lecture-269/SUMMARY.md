## Lecture 269. Event-Driven Microservices Architecture.

- Event-Driven Microservices Architecture - it is when microservices communicate with each other by publishsing and subscribing to events.
- When a service needs to communicate with another service, it publishes an event to a message queue. Other service can then subscribe to that event and take actions when the event received.
- Event-Driven Microservices Architecture uses Asynchronous communication.
- Decoupled communication.
- Real-time processing - events are published and consummed immediately right after.
- High volume events. Well-suited for handling high pvolume events, as they can scaly horizontally by adding more event consumers as needed. Can be scaled independently to handle increased load.