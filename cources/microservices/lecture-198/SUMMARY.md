## Lecture 198. What is Domain Events and the difference between Domain and Intgration Events

Domain events - event that happened in the past and the other parts of the same domain (service boundary) need to react to this changes.

Real world example: there is an Activity Monitoring feature that saves sessions in the database. SessionSaved - it is the domain event. During saving there are some operation related to other features, for example session anonymization (every session should be anonymized). It is the reaction of other parts of the same domain.

Domain event - side effect of domain operation. It is creadet to achive consistency between agregates in the same domain. Consistency is achived by putting the business logic that should be triggered during the processing of domain operation in the one certain place - Domain Event class.

To use Domain Events in DDD, encapsulate the event details and dispatch them to interested parties.

Domain Events vs Integration Events

Domain Events:
- Published and consumed within the single domain. Strictly within the boundaries of the microservice/domain context.
- Indicate something that has happened within the aggregate.
- In-process and synchronously, sent using an in-memory message bus.
Example: OrderPlacedEvent.

Integration Events:
- Used to communicate state changes or events between different contexts or microservices.
- It is overall system's reaction to certain domain events.
- Asynchronously, sent with a message broker over a queue to other services.
Example: after handling OrderPlacedEvent, an OrderPlacedIntegrationEvent might be published to a message broker, then consumed by other microservices it has been sent to.

Integration Events are usually created as a result of Domain Events.