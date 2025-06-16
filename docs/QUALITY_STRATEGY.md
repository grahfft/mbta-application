# Quality Strategy

## Objectives
- Verify I built the service right.
    - This means to make sure I don't have bugs in the service before deploying it.
- Validate I built the right service
    - This means having testing that mimics stakeholders use cases and getting it infront of stakeholders as soon as possible

## Scope
The current level of testing only covers low level unit and integration tests. If this were a real project, I would advocate for End to End testing. As such, this can be lumped into Exploratory testing without much loss in integrity.

## Testing

### Writing a Test and Testing Philosophy

The testing philosophy of this service is to focus on testing behaviors as opposed to testing ALL methods.

This means testing to make sure a particular behavior of the system occurred. This produces less tests with the same coverage as traditional methods.

This testing suite uses [xUnit](https://xunit.net/) and [Moq](https://github.com/devlooped/moq) to develop and accomplish this goal.

### Executing automated Tests
Running the below command will execute all automated tests in the below system

```dotnet test```

Executes tests within the test project

### Exploratory Testing

Exploratory Testing is done via Swagger. This is used to find issues automated testing can't find.

Running Swagger can be found [here](SETUP.md)

### Contract Testing

It should be noted that the HttpClient is embedded into the MbtaRepository class. This is done by design.

If this were a real project, I would advocated for contract testing that spins up this repository and tests against the actual API.

This type of testing can be expensive and complex since we don't have an internal contract to ingest. For this reason alone, I have skipped this form of testing for this small project.

### User Journies

Included in this testing is the intended User Journey, or how we expect someone to use this service.

The test uses a grey box to verify the functions work together in concert.

### Performance Testing
Performance testing wasn't done on this application. If I were to continue to with this service, we'd want to make sure the performance of my caching mechanism is better than the performance of the MBTA API'S caching mechanism.

### Security Testing
There isn't much data a malicious user could get back from this application. I have foregone security handling.

## Observability
This was a simple application so I did not put in much in terms of obsservability. Below is what I would do if I were to continue this service. 

### Logging
There wasn't much need for logging in this application. Much of the core was placed under unit to make sure everything was working correctly. If I were to continue this service. I would add in logging in the caching mechanism or into the `RouteService` as that would be responsible for handling calls to and from both the external cache and the MBTA API.

### Tracing
Tracing should be done on each `Get` method in order to monitor how long each call takes and to give us insight into how each performs on a given call. Tracing can be used to showcase the locking issue I would have with multiple people using this service at once.

### Exception Handling
It's customary to add in specific exceptions for better understanding of what is going on inside the application. I haven't done this. I would expect that any call to the MBTA API would have its own exception so we can have an easier time debugging issues in the future.

## Release Process

### Build Stage
I would have a build stage for this API. I would then use this build throughout the rest of the process.

### Linting and Basic Security
In these jobs, we look to make sure packages are secure and developers have not done something to make the application insecure. We'd look for thins like checked in credentials or endpoints that don't meet our security criteria.

### Unit and Integration stage
These can be run in parrellel once the Linting and Security stage is done. For this stage, I would expect to run a select group of tests for each job. Once both jobs are completed, we can move onto the next stage.

### User Journies
I have separated this out into its own job. This stage makes sure our current understanding of how the end user will use this API is tested. Passing this does NOT mean we have validated this service just our current understanding.

### Deploy To Testing Environemnt
At this stage, I would deploy to testing environments for:
- Product Owner sign off
- Performance, E2E and additional Security testing

### Create a Release and deploy to Internal Environment
At this point I beleive we should create a release candidate to go out. We are developing an API for this service so a release candidate helps with maintaining our API. With a release candidate deployed to an internal environment, we can now have other teams begin to develop against the newest API before we deploy it to prod. Here we can find if there are potentially breaking changes for future development.

### Blue Green Deployment into Production
Once we have sign off on the candidate, we should deploy this in a manner that does not bring down the current service until the new service is spun up. This is especially important as the cache design will need to make sure it's cache is loaded before it comes online.

## Stakeholders

### Developers
Other developers seem to be the initial designated stakeholder. This is an API without a front end, I would assume another team is handling the front end so I would need to work with them to design our User Journies.

### End User
I don't know who the end user is but ultimately, this service is for that person. Again, I'd want to work with other teams to identify the end user journies in order to make a better API.

### Interviewers
I mean this is an interview project right? So you're definitely a Stakeholder.