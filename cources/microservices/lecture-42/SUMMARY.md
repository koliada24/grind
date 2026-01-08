## Lecture 42. Logical and physical implementations of CQRS

What are the logical and physical CQRS implementations?
- Logical implementation of CQRS: splitting operations, not databases. Separate the read (query) from the write (command) operations at the code level, but not necessarily at the database level (can be one DB).
- Even though the same database is used, the paths for reading and writing data are distinct.
- Physical implementation of CQRS: Separate databases. Splitting the read and write operations not just at the code level but also physically using separate databases.
- Introduces data consistency and synchronization problems.

CONCLUSION: Logical = lite physical.

When to use each?

Use logical when:
- You want clean separation of read and write in your code.
- System is small or medium.
- Mostly to clean up a code.
- You want achieve CQRS benefits (no god entities, clear intent) without high complexity.
Use physical when:
- Read load is MUCH HIGHER than write load.
- You need independent scaling of reads and writes.
- You have high performance requirements.
- System is large and distributed.

CONCLUSION: Start with logical CQRS, move to physical when scale or performance forces you.