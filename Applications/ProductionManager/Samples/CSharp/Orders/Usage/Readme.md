# Access order usage statistics using the HOMAG Connect Client

With the HOMAG Connect Client, you can retrieve order usage statistics from productionManager to monitor license usage and track orders that have been released or reset.

## Get Usage Overview

The usage overview provides a summary of license usage grouped by period, showing how many parts were released compared to the available license capacity.

<strong>Example GetUsageOverviewAsync:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get usage overview for the last 12 months
var usageOverview = await client.GetUsageOverview(monthsAgo: 12);

// Use the retrieved data
foreach (var overview in usageOverview)
{
    Console.WriteLine($"Period: {overview.Period:yyyy-MM}");
    Console.WriteLine($"Parts released: {overview.QuantityOfReleasedParts} / {overview.QuantityOfPartsCoveredByTheLicenses}");
    
    foreach (var license in overview.Licenses)
    {
        Console.WriteLine($"  License: {license.ApplicationLicenseFullName} (Count: {license.LicenseCount})");
    }
}
```

The response is a list of [UsageOverview](../../../../Contracts/Orders/UsageOverview.cs) which shows license usage per period.

The sample code can be found at [ProductionManager - Get Usage samples](GetOrderUsageSamples.cs).

## Get Usage Details

The usage details provide detailed information about individual orders that have been released or reset.

<strong>Example GetUsageDetailsAsync:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get usage details for the last 12 months
var usageDetails = await client.GetUsageDetails(monthsAgo: 12);

// Use the retrieved data
foreach (var detail in usageDetails)
{
    Console.WriteLine($"{detail.Timestamp:yyyy-MM-dd HH:mm} - {detail.OrderNumber} ({detail.OrderName})");
    Console.WriteLine($"  Customer: {detail.CustomerName} ({detail.CustomerNumber})");
    Console.WriteLine($"  Parts: {detail.QuantityOfParts}, Action: {detail.Action}, By: {detail.ChangedBy}");
}

// Calculate totals
var totalParts = usageDetails.Sum(d => d.QuantityOfParts);
var releaseCount = usageDetails.Count(d => d.Action == OrderAction.Release);
var resetCount = usageDetails.Count(d => d.Action == OrderAction.ResetRelease);

Console.WriteLine($"Total parts: {totalParts}");
Console.WriteLine($"Releases: {releaseCount}, Resets: {resetCount}");
```

The response is a list of [UsageDetails](../../../../Contracts/Orders/UsageDetails.cs) which shows detailed order statistics.

## Get Usage Details for Specific Period

You can retrieve usage details for a specific month by providing the period in yyyy-MM format.

<strong>Example GetUsageDetailsForPeriodAsync:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get usage details for January 2025
var janUsage = await client.GetUsageDetailsForPeriod("2025-01");

// Use the retrieved data
foreach (var detail in janUsage)
{
    Console.WriteLine($"{detail.Timestamp:yyyy-MM-dd} - {detail.OrderNumber}: {detail.QuantityOfParts} parts");
}
```

## Get Current Month Usage

You can retrieve the current month's usage details using the dedicated endpoint.

<strong>Example GetCurrentUsageAsync:</strong>

```c#
// Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get current month's usage details
var currentUsage = await client.GetCurrentUsage();

Console.WriteLine($"Current month usage ({DateTime.Now:yyyy-MM}):");

// Use the retrieved data
foreach (var detail in currentUsage)
{
    Console.WriteLine($"{detail.Timestamp:yyyy-MM-dd HH:mm} - {detail.OrderNumber} ({detail.OrderName})");
    Console.WriteLine($"  Customer: {detail.CustomerName}");
    Console.WriteLine($"  Parts: {detail.QuantityOfParts}, Action: {detail.Action}");
}

// Calculate current month statistics
var totalParts = currentUsage.Sum(d => d.QuantityOfParts);
var releaseCount = currentUsage.Count(d => d.Action == OrderAction.Release);
var resetCount = currentUsage.Count(d => d.Action == OrderAction.ResetRelease);

Console.WriteLine($"\nCurrent month totals:");
Console.WriteLine($"Total parts: {totalParts}");
Console.WriteLine($"Releases: {releaseCount}, Resets: {resetCount}");
```

The response is a list of [UsageDetails](../../../../Contracts/Orders/UsageDetails.cs) for the current month.

The sample code can be found at [ProductionManager - Get Usage samples](GetOrderUsageSamples.cs).

## Related Types

- [UsageOverview](../../../../Contracts/Orders/UsageOverview.cs) - Overview of usage per period
- [UsageDetails](../../../../Contracts/Orders/UsageDetails.cs) - Detailed statistics about individual orders
- [License](../../../../Contracts/Orders/License.cs) - License information
- [OrderAction](../../../../Contracts/Orders/OrderAction.cs) - Action performed on the order (Release/ResetRelease)
