## Lecture 15. Monolithic vs Microservices Architecture Comparison

Now let’s compare Monolith and Microservices.

- Application architecture. Monolith is a single, simple, straightforward unit. Microservices have a complex structure that consists of various heterogeneous services and databases.
- Scalability. Monolith is scaled as a single unit. Microservices can be scaled independently. It encourages companies to move to microservices to save money.
- Deployment. Monolith is easier and faster to deploy. Microservices are harder to deploy, but later they provide zero-downtime deployment and CI/CD automation.
- Development teams. If your team doesn't have experience with microservices and container systems, building microservices will be difficult.

Questions
1. What does zero-downtime deployment mean and why can’t a monolith have it?
2. Why will building microservices be difficult if a team does not have previous experience working with microservices? Aren't microservices just split monoliths?
 
 
Answers
1. Zero-downtime deployment means the possibility to deploy a new version of an application without any service interruptions for users. In short, a monolith can have zero downtime, but it is harder to do because it is not designed to run on multiple instances, which is required for zero-downtime deployment.
2. First of all, microservices are not just split monoliths. In a monolith, a method call is an in-memory call, which is processed in a single application. With microservices, it is a network call, which is slow, unreliable, and more difficult in general. Inexperienced developers can underestimate these issues. This is just one example; there are other problems developers usually do not face while developing monolithic solutions.
