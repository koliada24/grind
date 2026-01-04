## Lecture 13. When to use Microservices Architecture

As discovered in the Lecture 12, Microservices Architecture can lead to a bunch of challenges. So when to use them?
- Make sure you have a "Really Good Reason" for implementing microservices. Check if your application can do without microservices. Implement microservices if your application requires agility to time-to-market with zero downtime deployments.
- Iterate with small changes and keep the Single-process monolith as your default. Start with monolith and then iteratively refactor it turning single module into a microservices system.
- Required to independently deploy new functionality with zero downtime. When an organization needs to make changes to existing functionality without affecting the rest of the system - it is a good choice to use microservises architecture.
- Required to independenlty scale a certain part of application without scaling all the app. If only one part of your functionality needs an scale, you can scale it without scaling the whole system.

What are the benefits of independent scaling compared to running monolith on all the instances?

1. **POINT**❗: When changes to one part of functionality are made, you only need to redeploy the instances that are running this part, not all the instances.
 
 
**QUESTION**❓: But is it a problem if the deployment is still authomated?

**ANSWER**: yes✅. Automation helps, but it does not eliminate all problems. 

2. **POINT**❗: You do not load all the code, only the code of the functionality you want to scale.
 
 
**QUESTION❓**: But does this allocated memory for unused components tooks a lot of resources? And do startup time really matters?

**ANSWER**: yes✅

Agility - flexibility, approach focused on rapid delivery and fast adding of new features.