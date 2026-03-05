# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, rework data can be retrieved from productionManager for further programmatic evaluation.

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

## GetReworks

Retrieves rework records with flexible filtering options including date range, state, identifier, and pagination.

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

## GetReworkHistory

Retrieves rework history records with filtering options for date range, identifier, and rework ID.

<strong>Example 1: Get rework history from the last 14 days</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get rework history from the last 14 days
var history = await client.GetReworkHistory(daysBack: 14).ToListAsync();

// Use the retrieved data
history.Trace();
```

<strong>Example 2: Get rework history with specific date range</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get rework history with specific date range
var from = DateTime.UtcNow.AddMonths(-1);
var to = DateTime.UtcNow;
var history = await client.GetReworkHistory(
    from: from,
    to: to,
    take: 100).ToListAsync();

// Use the retrieved data
history.Trace();
```

<strong>Example 3: Get rework history by rework ID</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get rework history by rework ID
var history = await client.GetReworkHistory(
    daysBack: 30,
    reworkId: "REWORK-456",
    take: 10).ToListAsync();

// Use the retrieved data
history.Trace();
```