## Lecture 16. The Database-per-Service pattern - Polyglot persistence

Let's take a view on the database-per-service pattern.
- Core characteristic of microservices - loose coupling. Every service should have its own database and not to share data directly with other microservices.
- In our E-commerce application we will have ordering, shopping cart and product microservices each with its own database. Any changes do not impact other microservices.
- The service's database cannot be directly accessed by other microservices. Its data can only be accessed by the REST API through exposed endpoints.

Benefits of Database-per-Microservice:
- Data schema changes can be made easy without affecting other microservices.
- Each database can scale independently.
- Microservices domain data is encapsulated within the service only.
- If one the database service is down, it will not affect other microservices.
- There is a possibility to select the best optimized database type for each microservice's needs.

Questions:
- What does polyglot persistence means?
- What does it mean to scale the database?
- "Database-per-microservice" - does it mean one database per instance or one database per all the same services?
 
 
Answers:
- Polyglot persistence is an approach where an application uses multiple database technologies, each chosen based on specific data storage needs. "Database-per-Service pattern" enables polyglot persistence.
- Scale a database is the same as scale a microservice. Vertical scaling - adding more power to the instance DB is running on. Horizontal scaling - adding new instances.
- "Database-per-Service" means one database per service type. All instances of the same type share that database.
