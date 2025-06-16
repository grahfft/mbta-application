# Quality Strategy

## Objectives
- Verify I built the service right
- Validate I built the right service

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

## Observability


## Release Process


## Stakeholders