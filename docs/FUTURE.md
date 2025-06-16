# Future State of the Application

## GraphQL vs Rest
The way this assignment is presented, GraphQL would work better as we want to understand the MBTA based on the different Routes and Stops. GraphQL also provides a great mechanism for events. As the application progresses, there is information that can change such as delays and scheduling. These could be event handled instead of querying everytime.

## External Cache Mechanism
This interview assignment didn't need anything more than an internal caching mechanism. If this were a real service, I would extend this to have an external caching mechanism such as Elasticache or Redis.

## Better Cache miss handling
Currently, cache misses are handled with throwing exceptions. This won't work as the data becomes more volatile. When this occurs, we should handle cache misses by reaching out to the MBTA Api.