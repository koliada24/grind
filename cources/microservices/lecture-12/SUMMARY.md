## Lecture 12. Challenges of Microservices Architecture

Despite having a lot of benefits choosing microservices architecture can lead to several challenges:
- Complexity. Each microservice is simpler, but the entire system is more complex. Deployment and communication can be complicated for hundreds of microservices. There are more "moving" parts in such solutions.
- Network problems and latency. Microservices communicate with each other through the network which increases time of the operations. Also there could be possible network problems which should be taken into account as well.
- Development and testing. It is more difficult to test E2E process in microservices architecture compared to monolithic architecture. The E2E and integration tests become more important and harder than in monolithic system.
- Data integrity. Each microservice has its own database, which leads to lack of global transactions and strong consistency. There are no guarantees that every operation will be done successfully so there should be used other patterns which are not needed in monolith.

E2E process - it is a complete business flow from start to finish, crossing multiple components and services.

Data integrity - set of characteristics of a data. It means there should be no missing data, no contradictory data, no invalid states, no unintended duplicates, correct relationships between data 