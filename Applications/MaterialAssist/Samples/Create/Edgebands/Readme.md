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

By choosing ManagementType.Single there will be a new Id for every entity, while the quantity on each entity will be 1. The amount of entities created depends on the property Quantity of MaterialAssistRequestBoardEntity.

When choosing ManagementType.Stack or ManagementType.GoodsInStock only one entity item will be created, which has the quanity set in the Quantity property of MaterialAssistRequestBoardEntity and a single Id.
