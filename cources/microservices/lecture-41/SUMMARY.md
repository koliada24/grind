## Lecture 41. CQRS - Command Query Responsibility Segregation

What is CQRS?
- CQRS is used to avoid complex queries to get rid of inefficient joins.
- Separates read and write operations with separating databases.
- Commands: change the state of the app data.
- Quierise: handling complex join operations and returnning a result, don't change the state of app data.
- Large-scaled microservices architecture needs to manage high-volume data requirements.
- Single database for services can cause bottlenecks.
- Use of both CQRS and Event Sourcing patterns improved application perfomance.
- CQRS offers to separate read and write data that provide to maximize query perfomance and scalibility.

Usually, read database uses NoSQL databases with denormilized data, and write database uses relational databases wuth fully normalized data and supports strong data consistency.