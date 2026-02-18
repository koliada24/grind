## Lecture 266. Benefits of Microservices Asynchronous Communication

- New Subscriber Service. It is easy to add a new subscriber by just subscribing to a message we want to receive. The producer does not need to know about the new subscriber, it just keeps sending messages to a queue. So we can add/remove subscribers without affecting producer.
- Scalability. With async communication it is easier to manage scalability issues: we can scale producer, consumer and message broker system independently. We can scale services according to incoming messages into event bus system. To calculate how much instances to use for each service we can use Kubernetes or KEDA or other Auto-scalers.
- Event-driven Microservices. With async communication, we can provide event-driven architecture.