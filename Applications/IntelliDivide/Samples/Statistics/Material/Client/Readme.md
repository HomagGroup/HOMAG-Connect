# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the material statistics data can be retrieved from intelliDivide for further programmatic evaluation.

<strong>Example:</strong>

```c#
 // Create new instance of the intelliDivide client:
var client = new IntelliDivideClient(subscriptionId, authorizationKey);

// Define the parameters:

DateTime from = DateTime.Now.AddMonths(-3);
DateTime to = DateTime.Now.AddDays(-1);
int take = 1000;
            
// Get the data
var materialStatistics =  await client.GetMaterialStatisticsAsync(from, to, take).ToListAsync();

// Use the retrieved data
var totalBoardsUsedInSquareMeter = materialStatistics.Sum(m => m.BoardsUsed);
var totalOffcutGrowthInSquareMeter = materialStatistics.Sum(m => m.OffcutsGrowth);

materialStatistics.Trace();
``` 

The sample code can be found at [intelliDivide - Material statistics sample reports](MaterialStatisticsSample.cs).