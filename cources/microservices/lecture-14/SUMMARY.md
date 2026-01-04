## Lecture 14. When NOT to use Microservices Architecture

There are some anti-patterns of Microservices:
- Don't do a Distributed Monolith. Make sure you divide the system properly respecting the decoupling like applying bounded context and business capabilities principles. Distributed Monolith is the worst case because you increase system complexity without getting any benefits of microservices.
- Don't do microservices without DevOps or cloud services. Microservices are embraced the distributed cloud-native approaches. And you can only maximize benefits of microservices with following the cloud-native principles.

Questions:
1. What is bounded context?
2. What is decoupling?
3. What is chatty communication?
 
 
Answers:
1. Bounded context (in the context of microservices architecture) - means that the microservices have clear boundaries of responsibility. Each microservice owns only one business capability. Microservice should do not know about what other microservices do and about the business logic inside them.
2. Decoupling - means reducing the dependencies and shared parts between microservices.
3. Chatty communication - it is when two services exchange several calls to complete some process.