# TaskManagerAPI

## Basic Load Testing with JMeter:
- Analyze and document your results, focusing on the API’s performance under
moderate load.


When load testing we used Apache JMeter to create test plans. We created three different test plans with different amounts of thread groups (users) on the same HTTP request, which was the GET of the /TaskManager endpoint. The test plans are OLA2_Test_Plan_50 with 50 users, OLA2_Test_Plan_100 with 100, and OLA2_Test_Plan_50l (l for loop) with 50 users each sending 30 requests. For each test, we used the Gaussian Random Timer, so that there would be a pause in between requests of between 0.5 and 1.5 seconds. We also used the response assertion to assert whether or not the response code was 200 (OK) for all tests. The results of the load tests can be found in the corresponding folders https://github.com/cph-Pack/SQOLA2/tree/master/Load%20Testing. 

The tests show that surprisingly the best overall performing test was the 50l test.
It was the fastest test in terms of response times, as it had a mean response time of 3.9 ms, while the 50 test had a mean response time of 29.2 ms, and 100’s was 38.5 ms. All tests did have some outliers in response time with the max response time being significantly higher than the mean at 237, 447, and 306 ms respectively. These slower response times are all past the 90 percentiles, so most users won’t experience those response times, but even the lowest response time for the 50 and 100 test was higher than the 50l mean response time at 8 ms for both.

## Reflection on Coverage and Performance:
- Reflect on how you ensured code coverage and maintained a balance between
unit and integration tests.

We made sure to test all the methods that we made. Any code that was given by microsoft/mongodb is considered good code and need not be tested in our eyes.
We have far more integration tests compared to unit tests. This is not by design but due to poor decoupling of our code. We are aware of this issue and we are therefor considering a test driven development (TDD) style for the next project.


- Briefly discuss the importance of code coverage and how you could optimize performance based on your load testing and benchmarking results.

Code coverage is a good metric to show that some effort has been made to test the code. It does not reflect the quality of the tests however it does show potential paths of your code uncovered by the tests. Typically this is when an error occurs as most people test the happy path.

When refactoring or writing new code we can quickly monitor if it is being targeted by any tests. If a certain % of code coverage is required for a project to build down the CI/CD pipeline this catches any potential bugs that may have been introduced.

 To optimize our performance, we should analyze the difference in the load tests further. We could also think about implementing caching strategies, so we could cache frequently used resources to reduce the response times across all our tests. 
