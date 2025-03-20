# Create edgeband entity

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
Console.WriteLine($"Created edgeband entity: {newEdgebandEntity.Id}");
```
When creating a edgeband entity you have the option to choose between the management types ManagementType.Single, ManagementType.Stack and ManagementType.GoodsInStock. 

When choosing ManagementType.Single the quantity has to be set to 1. There must be a unique id given for every single entity. 
By choosing Stack you can add a quantity greater or euqal to 1 allowing to stack multiple entities within the same Id.
With GoodsInStock it is possible to stack the entities within the same id and additionally, when a location is set, new GoodsInStock entities of the same edgeband type will be added to this entity id when same location is used.
