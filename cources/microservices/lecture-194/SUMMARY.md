## Lecture 193. Strongly-typed IDs

Purpose of the strongly-typed IDs is to remove redudant types for IDs. For example: there is ProductId and CustomerId, both are Guid types. That can create confusion, which called "Primitive Obsession". To resolve this, you can create a new class for each Id (CustomerId, ProductId)