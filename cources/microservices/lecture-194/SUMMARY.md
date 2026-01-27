## Lecture 194. Anemic-domain vs Rich-domain model

Anemic-domain model - simple data container, all the business logic is placed outside of the class. Faster to develop but harder to maintain because related classes usually placed in the other layers.

Rich-domain model - entity classes contain related business logic. Takes more time to develop but easier to maintain because logic are placed close to the class entity declaration.

Better to use Anemic-domain model in the simple CRUD apps without any complex logic.
Better to use Rich-domain model in the bigger apps with a complex business logic.