# Configuration data management (cDM)

With the HOMAG Connect Client, the custom tables of a configuration data management (cDM) library can be read and written for further programmatic evaluation. Custom tables are used, for example, to drive order configurators with external master data.

<strong>Example GetLibraries:</strong>

```c#
// Create new instance of the configurationDataManagement client:
var client = new ConfigurationDataManagementClient(subscriptionId, authorizationKey);

var libraries = await client.GetLibraries();
```

The response is a list of [LibraryOverview](../../../../Contracts/ConfigurationDataManagement/LibraryOverview.cs) which exposes the available cDM libraries.

<strong>Example GetCustomTables:</strong>

```c#
var libraryId = "Default"; // the library name is its identifier

var customTables = await client.GetCustomTables(libraryId);
```

The response is a list of [CustomTableOverview](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableOverview.cs).

<strong>Example GetCustomTableStructure:</strong>

```c#
var structure = await client.GetCustomTableStructure(libraryId, "MyCustomTable");
```

The response is a [CustomTableStructure](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableStructure.cs), which describes the column definitions of the custom table, including its `ETag`.

<strong>Example GetCustomTableData:</strong>

```c#
var data = await client.GetCustomTableData(libraryId, "MyCustomTable");
```

The response is a [CustomTableData](../../../../Contracts/ConfigurationDataManagement/CustomTables/CustomTableData.cs), which contains the complete content (all rows) of the custom table, including its `ETag`.

<strong>Example SetCustomTableData:</strong>

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

The sample code can be found at [OrderManager - Configuration data management sample](ConfigurationDataManagementSamples.cs).
