## Lecture 267. Challenges of Microservices Asynchronous Communication

- Single point of Failure - Message Broker. The message broker becomes a single point of failure. We should not rely on a single node of message brokers, instead we should scale it and use hybrid communication with sync and async in some cases.
- Debugging. It is more difficult to debug issues with async communication due to the need of tracking of the flow of the single operation across service boundaries. Debugging of the flow and the payload of events takes so many times and hard to debug at the same time.
- At least once delivery and Guarantee an order of messages. Mostly brokers use at-least-once delivery and not Guarantee order of messages. Should embrace these message delivery mechanism with applying idempotency consumers and not designing FIFO required cases.

Questions:
1) Explain deeply what the first paragraph about "Single point of Failure" does mean.
2) "It is more difficult to debug issues with async communication due to the need of tracking of the flow of the single operation across service boundaries" - isn't this problem present in a simple synchronous way of communication between microservices? I mean, we still send one request from one service to each other, so we need to deal with boundaries problem anyways.