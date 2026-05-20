# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, rework data can be retrieved from productionManager for further programmatic evaluation.

## GetRequestedReworks

Retrieves all requested reworks (states: Requested).

<strong>Example:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var requestedReworks = await client.GetRequestedReworks().ToListAsync();

// Use the retrieved data
requestedReworks.Trace();

Assert.IsTrue(requestedReworks.Any());
var reworkIds = requestedReworks.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(reworkIds));
```

## GetApprovedReworks

Retrieves all approved reworks (states: Approved).

<strong>Example:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var approvedReworks = await client.GetApprovedReworks().ToListAsync();

// Use the retrieved data
approvedReworks.Trace();

Assert.IsTrue(approvedReworks.Any());
var reworkIds = approvedReworks.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(reworkIds));
```


## GetCompletedReworks

Retrieves all completed reworks (states: Rejected, Transferred).

<strong>Example:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var completedReworks = await client.GetCompletedReworks().ToListAsync();

// Use the retrieved data
completedReworks.Trace();

Assert.IsTrue(completedReworks.Any());
var reworkIds = completedReworks.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(reworkIds));
```

## GetCurrentReworks

Retrieves active rework records currently visible in productionManager, with support for filtering by date range, state, and pagination.

<strong>Example 1: Get reworks filtered by state</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get reworks with specific states
var reworks = await client.GetCurrentReworks(states: new[] { ReworkState.Approved, ReworkState.Pending }, take: 10).ToListAsync();

// Use the retrieved data
reworks.Trace();
```
<strong>Example 2: Get reworks with specific date range and state filter</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get reworks with specific date range and state filter
  var from = DateTime.UtcNow.AddDays(-30);
  var to = DateTime.UtcNow;
  var reworksFiltered = await productionManager.GetCurrentReworks(
      capturedAtFrom: from,
      capturedAtTo: to,
      states: new[] { ReworkState.Transferred },
      take: 100)!.ToListAsync();
  reworksFiltered.Trace(nameof(reworksFiltered));

// Use the retrieved data
reworksFiltered.Trace();
```


## GetReworks

Retrieves all rework records, including both currently visible and no longer visible entries in productionManager, with support for filtering by date range, state, identifier, and pagination.

<strong>Example 1: Get reworks from the last 7 days</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get reworks from the last 7 days
var reworks = await client.GetReworks(daysBack: 7).ToListAsync();

// Use the retrieved data
reworks.Trace();
```

<strong>Example 2: Get reworks with specific date range and state filter</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get reworks with specific date range and state filter
var from = DateTime.UtcNow.AddDays(-30);
var to = DateTime.UtcNow;
var reworks = await client.GetReworks(
    from: from,
    to: to,
    state: ReworkState.Transferred,
    take: 100).ToListAsync();

// Use the retrieved data
reworks.Trace();
```

<strong>Example 3: Get reworks by production item identifier with pagination</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get reworks by identifier with pagination
var reworks = await client.GetReworks(
    daysBack: 14,
    identifier: "PART-123",
    take: 50,
    skip: 0).ToListAsync();

// Use the retrieved data
reworks.Trace();
```

