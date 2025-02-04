<h1 id="createEdgebandEntity"> Create edgeband entity</h1>

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be created. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Define the create edgeband entity request:
var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
{
    Id = "42",
    EdgebandCode = "White Edgeband 19mm",
    ManagementType = ManagementType.Single,
    Comments = "This is a comment",
    Quantity = 1,
    Length = 1000,
    CurrentThickness = 19
};

// Create the edgeband entity
var newEdgebandEntity = await client.CreateEdgebandEntity(edgebandEntityRequest);

// Use the created edgeband entity for further processing
Console.WriteLine($"Created edgeband entity: {newEdgebandEntity.Code}");
```

