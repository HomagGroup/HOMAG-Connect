# Create board entity

With the HOMAG Connect materialAssist board client, board entities can be created. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the create board entity request:
var boardEntityRequest = new MaterialAssistRequestBoardEntity()
{
    Id = "42",
    //The board code is the identifier of the board type
    BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
    ManagementType = ManagementType.Single,
    Comments = "This is a comment",
    Quantity = 1
};

// Create the board entity
var newBoardEntity = await client.CreateBoardEntity(boardEntityRequest);

// Use the created board entity for further processing
Console.WriteLine($"Created board entity: {newBoardEntity.Id}");
```
When creating a board entity you have the option to choose between the management types ManagementType.Single, ManagementType.Stack and ManagementType.GoodsInStock. 

By choosing ManagementType.Single there will be a new Id for every entity, while the quantity on each entity will be 1. The amount of entities created depends on the property Quantity of MaterialAssistRequestBoardEntity.

When choosing ManagementType.Stack or ManagementType.GoodsInStock only one entity item will be created, which has the quanity set in the Quantity property of MaterialAssistRequestBoardEntity and a single Id.