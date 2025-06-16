# Understanding the Design

## Rest API
There are only two get methods, one for getting all routes and one for getting all the stops by route id.

### `/Routes`
This returns all route ids that can be used to query for stops. This only returns a list of strings as there isn't any other data needed for Routes.

### `/Routes/{route_id}/stops`
This takes in a `route_id` in order to determine what list of stops should be returned. Stops should be in a connected order and contain a list of each connecting route and stop. If a provided Id does not exist in the internal cache, an error will be thrown.

## Caching

The data needed for this assignment rarely changes as there is a real world constraint on the data changing. As such, we can use a caching system to better increase performance of our service when requesting the data we need.

## Design Issues

### Caching on Start up
I'd have preferred to do this all during Service Start up/Initialization time. This way the end user is not constantly checking if the Cache is already loaded.
This would also solve a secondary issue. There is currently a race condition in the cache around isCacheLoaded. I would want to lock this. C# does not allow locking of this type and I couldn't find a good locking mechanism that worked with async/await.

### Throwing an error on Cache miss
In general, we should handle cache misses by reaching out to the MBTA service for the data. This isn't necessary for this assignment. The MBTA can't change that rapidly as there is a real world/physical constraint preventing this. In fact, the data could be uploaded from a data file and we'd be none the wiser. As such, I have opted instead for throwing an error instead.
