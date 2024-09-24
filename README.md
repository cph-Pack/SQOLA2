# OLA3

## Peer Code Review
During our code review, we followed a walk-through style, and we reviewed the classes one at a time and wrote down our feedback.

### TaskManager
We noticed an inconsistence use of boolean expressions within the same function. The author had used both `== false` and `!`, so we noted that we would like it to be consistent, and it was changed to only `!` as it is less verbose. 

We also discussed naming conventions, specifically with `TaskIsValid` vs `UniqueTask`, since the functions have similar uses and therefore could be named more similarly. They were changed to `IsValidTask` and `IsUniqueTask`. We also suggested to further improve these functions and where they're used, as they currently throw exceptions both within and when used.

We also discussed that we would like to consistently use guard clauses. Guard clauses are conditional checks placed at the beginning of methods to validate input parameters or preconditions, and if the conditions aren't met, an exception is thrown. We use them to improve code readability as they clearly state the expectations and constrains, and since we then fail fast.

### TaskManagerController
We discussed if we should improve our exception handling by having specific catches in our `try-catch` blocks. This is a possibly improvement for later, as we currently only throw one type of exception.

We also noticed some commented-out code, which we agreed should from now on be removed before merging into the master branch (except in special cases).

We also noticed that we can add an id to the task from the endpoints. This is something we will change later, as it shouldn't be possible.

### Coding standard overall
Overall we agreed that our code was quite readable, but we discussed adding some XML comments to document our code. 

We looked at these [coding standards](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) by Microsoft and noticed that we do mostly adhere to them. We noticed that we deviate when it comes to comments, however, we do not think it affects the readability or maintainability of our code.

### Testing and mocking
We believe that we have followed the best practices regarding using mocks in testing. We used mocks to simulate our database, which ensured that the tests focused solely on the behavior of the code which interacts with it, which minimizes the risk of false results because of problems with the database. 

## Software Review
During our peer software review, we followed a technical review style and focused mostly on the suitability of the software based on the functional- and non-functional requirements.

### Functional correctness
The code performs as expected and we follow the business rules.

### Non-functional aspects
**Performance**: We aren't experiencing any significant performance issues, as we use a local database, and based on our prior load tests, we think that the performance is satisfactory. 

**Maintainability**: We focused on improving maintainability during our code review. Our key take aways were being consistent for better readability as well, and using guard clauses to make sure correct conditions are always met and that if the system fails, it fails fast. 

We improved the TaskManager by adding a database interface to its constructor instead of a hardcoded coupling to DBManager. This will make it easier to swap out or update the database layer in the future.

**Testability**: Testability has also been greatly improved, since we added the database interface, since we can easier mocking the database through the interface. This allows us to isolate the code logic from the actual database. 

We also added restrictions on certain methods, which made it easier for us to test the full functionality of the methods.

### Adherence to best practices
We have been consistent in following coding standards, focusing on code readability and structure. 

We aim to avoid commented-out code on the master branch unless it's absolutely necessary, in which case we will clearly add an explanation as to why. 

We want to add more documentation in the form of XML comments. 

Although our style of commenting doesn't necessarily adhere to Microsoft's coding standards, we like the amount and way we use comments in our code, and will continue in a similar way.




## Reflection on Testing and Code Quality
- The importance of mocking in unit testing for isolating external dependencies.

By mocking, we can created controlled simulated versions of the code to test the behaviour of the code and not nescessarily the dependencies within the system. We also don't have to rely on other dependencies running, as we can run the tests in an instant, which eliminates a delay that could occur with e.g. a database. By using mock data, we ensure that our tests are isoslated, fast and reliable while also testing the behaviour of the code, not the functionality of its dependencies

-   The value of performing both code reviews and software reviews in ensuring quality.

While doing code and software reviews we found issues ranging from minor to major which lead to some good discussions about how we want to write our code. The result of this is more streamlined code with naming being more consistent, guard blocks being set up the same way, exception handling at the same layer and more decoupling to make testing single functionality easier. We still have more things we would like to implement, but we decided to keep these ideas in mind for our future projects. The project enforces our rules for business logic and works as intended.


-   How Equivalence Partitioning and Boundary Value Analysis influenced your test design.

Equivalence Partitioning and Boundary Value Analysis influenced our test design as it helped us get efficient coverage of both valid and invalid input ranges. 

Using `[Theory]` and `[InlineData]` for the tests allowed us to test multiple values from different equivalence classes (valid and invalid) and the boundary values in a compact way. This also reduced redundancy, as I avoided writing separate tests for each value, which means a more organized and maintainable test suite. This approach ensured that we covered critical edge cases, and that the business rules are enforced. 
