# Understanding the Design

## Rest API
Using a basic Rest API will give us the information we need.

## Caching

The data requested shouldn't change ever. We should probably just cache this and re-fetch on start up.


## Design Issues

### Caching on Start up
I'd have preferred to do this all during Service Start up/Initialization time. This way the end user is not constantly checking if the Cache is already loaded.
This would also solve a secondary issue. There is currently a race condition in the cache around isCacheLoaded. I would want to lock this. C# does not allow locking of this type and I couldn't find a good locking mechanism that worked with async/await 