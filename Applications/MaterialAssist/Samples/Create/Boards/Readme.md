# Create board entity

With the HOMAG Connect materialAssist board client, board entities can be created. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var boardEntityRequest = new MaterialAssistRequestBoardEntity()
{
    Id = "42",
    //The board code is the identifier of the board type
    BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
    ManagementType = ManagementType.Single,
    Comments = "This is a comment",
    Quantity = 1
};

var newBoardEntity = await client.CreateBoardEntity(boardEntityRequest);

Console.WriteLine($"Created board entity: {newBoardEntity.Id}");
```
When creating a board entity you have the option to choose between the management types "ManagementType.Single", "ManagementType.Stack" and "ManagementType.GoodsInStock". 

When choosing "ManagementType.Single" the quantity has to be set to 1. There must be a unique id given for every single entity. 
By choosing "ManagementType.Stack" you can add a quantity greater or euqal to 1 allowing to stack multiple entities within the same Id.
With "ManagementType.GoodsInStock" it is possible to stack the entities within the same id and additionally, when a location is set, new "ManagementType.GoodsInStock" entities of the same material will be added to this entity id when same location is used.