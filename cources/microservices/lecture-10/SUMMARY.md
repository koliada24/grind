## Lecture 10. Microservices characteristics

Microservices characteristics are:
- Componentization via services. Each microservice is a unit of software which is independently replaceable and upgradeable.
- Organized around business capabilities. Microservices are split by business capabilities.
- Products not projects. A separate development team takes full responsibility of not only implementing but also deploying the microservices to production.
- Smart endpoints and dumb pipes. For clarification reasons: endpoints - microservices, pipes - communication mechanism. Microservices should contain all the logic, while pipes should only transfer the data and should remain unaware of business logic - that is why they are dumb.
- Decentralized governance. Each team governs its own technology stack. Only certain minimal deployment and security standards can be shared across teams. Code base should not be shared.
- Decentralized data management. Microservices prefer each service to manage its own separate database.
- Infrastructure automation. There should be a mechanism of automated provisioning, testing and deployment of each service independently across environments.
- Design for failure. Failures in microservices are not exceptional - they are expected. Therefore, microservices should be designed to detect, handle, isolate and recover from failures gracefully.