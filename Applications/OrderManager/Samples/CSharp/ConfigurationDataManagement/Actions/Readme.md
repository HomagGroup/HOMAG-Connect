# Configuration data management (cDM)

With the HOMAG Connect Client, the custom tables of a configuration data management (cDM) library can be read and written for further programmatic evaluation. Custom tables are used, for example, to drive order configurators with external master data.

#### Example GetLibraries

```c#
// Create new instance of the configurationDataManagement client:
var client = new ConfigurationDataManagementClient(subscriptionId, authorizationKey);

var libraries = await client.GetLibraries();
```

The response is a list of [LibraryOverview](../../../../Contracts/ConfigurationDataManagement/LibraryOverview.cs) which exposes the available cDM libraries.

#### Example GetCustomTables

```c#
var libraryId = "Default"; // the library name is its identifier

var customTables = await client.GetCustomTables(libraryId);
```

The response is a list of [CustomTableOverview](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableOverview.cs).

#### Example GetCustomTableStructure

```c#
var structure = await client.GetCustomTableStructure(libraryId, "MyCustomTable");
```

The response is a [CustomTableStructure](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableStructure.cs), which describes the column definitions of the custom table, including its `ETag`.

#### Example GetCustomTableData

```c#
var data = await client.GetCustomTableData(libraryId, "MyCustomTable");
```

The response is a [CustomTableData](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableData.cs), which contains the complete content (all rows) of the custom table, including its `ETag`.

#### Example SetCustomTableData

```c#
var request = new SetCustomTableDataRequest
{
	Rows =
	[
		new CustomTableRow
		{
			// RowId omitted => a new id is generated.
			Cells =
			[
				new CustomTableCell { ColumnId = "Name", Value = "Row 1" },
				new CustomTableCell { ColumnId = "Width", Value = "100" }
			]
		}
	]
};

var result = await client.SetCustomTableData(libraryId, "MyCustomTable", request);
```

`SetCustomTableData` replaces the complete content of the custom table in a single call; all existing rows that are not part of the request are removed. When `SetCustomTableDataRequest.ETag` is set, the change is only applied if the current content still matches it, so a concurrent change is not accidentally overwritten; otherwise the call fails and nothing is changed.

```c#
var current = await client.GetCustomTableData(libraryId, "MyCustomTable");

current.Rows.Add(new CustomTableRow
{
	Cells = new Collection<CustomTableCell>
	{
		new CustomTableCell { ColumnId = "Name", Value = "Added row" }
	}
});

var request = new SetCustomTableDataRequest
{
	ETag = current.ETag,
	Rows = current.Rows
};

var result = await client.SetCustomTableData(libraryId, "MyCustomTable", request);
```

#### Example SyncCustomTable

A custom table linked to materialManager can be synchronized with its materialManager data source. The synchronization runs asynchronously: `StartCustomTableSync` returns a [CustomTableSyncResult](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableSyncResult.cs) with a `CorrelationId`, which is used to query the progress with `GetCustomTableSyncState`.

```c#
var result = await client.StartCustomTableSync(libraryId, "MyCustomTable");
var correlationId = result.CorrelationId ?? string.Empty;

if (result.State == CustomTableSyncState.Error ||
	!string.IsNullOrEmpty(result.ErrorText) ||
	string.IsNullOrEmpty(correlationId))
{
	// The synchronization could not be started, e.g. the custom table is not linked to materialManager.
	return new CustomTableSyncStateData
	{
		CorrelationId = correlationId,
		State = result.State,
		ErrorTexts = [ result.ErrorText ?? "Custom table synchronization could not be started" ]
	};
}

using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(1));
CustomTableSyncStateData? state;

do
{
	await Task.Delay(TimeSpan.FromSeconds(2), cancellation.Token);

	state = await client.GetCustomTableSyncState(correlationId);
}
while (state?.State is CustomTableSyncState.Queued or CustomTableSyncState.Started);
```

`StartCustomTableSync` does not throw when the synchronization could not be started; check `State` and `ErrorText` instead. `GetCustomTableSyncState` returns a [CustomTableSyncStateData](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableSyncStateData.cs) with the [state](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableSyncState.cs), the progress (0-100) and the error and warning texts; it throws when the correlation id is unknown.

The sample code can be found at [OrderManager - Configuration data management sample](ConfigurationDataManagementSamples.cs).
