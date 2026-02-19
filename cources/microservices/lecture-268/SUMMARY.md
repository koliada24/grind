## Lecture 268. Fan-Out and message filtering with Publish/Subscribe Pattern

### Fan-Out
- Fan-Out - a messaging pattern when a message is distributed to a several destination in-parallel.
- The main idea is that several destinations can work and process this message in parallel.
- The flow is: publisher (producer) sends message to a topic (queue) and then this message is sent to all the subscribers (consumers).
- Each service can scale independently and all the services are completely decoupled.
- The publisher and subscribers do not need to know anything about other publishers and subscribers.

### Publish/Subscribe messaging pattern
- Publish/Subscribe messaging pattern is a form of async service-to-service communication.
- Any message of the topic is received by all of the subscribers in the topic.
- It is used to enable event-driven architecture and decouple applications to improve perfomance, reliability and scalability.

Questions:
1) When to use sync and async communication between microservices?
2) What is business invariant?

Answers:
1) Severals rules and examples to understand:
- Use sync when your operation depends on another service to succeed. Use async when it doesn't.
- Example. Students take tests and after the app saves the result. There are two service: one for taking the test, which stores the temporary answers, and after the finish, responces are being sent to the "storage" service. Should the saving of the results be async or sync? The answer is - sync. Despite no need of waiting for the responce after saving the business operation won't be finished until we make sure the results are saved to the storage and data is not lost. Saving results must be synchronous because completing the test requires guaranteed persistence.
- One-line conclusion: Business invariant - sync, reaction - async.

Important nuances:
1) Queue - one consumer. Topic - many consumers.
2) Business invariant - it is a rule that must always remain true in the domain and cannot be violated by any operation. Business invariant - rule the business world woutld never accept braking. After every operation, the business data must still make sense in real life. It is something that should happen and business never allow to exist, like paid order without payment, negative stock, completed test without answers, transfer where money disappears and so on. A business invariant is a rule describing a state the system is never allowed to enter. We must protect business invariants immediately.
Business invariant - condition, that must remain true after the action. This is the rule for the business. There could be sereval business invariants in a single domain.