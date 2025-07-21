# Create edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities and types can be created. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
{
    Id = "42",
    EdgebandCode = "White Edgeband 19mm",
    ManagementType = ManagementType.Single,
    Comments = "This is a comment",
    Quantity = 1,
    Length = 1000,
    CurrentThickness = 1.0
};

var newEdgebandEntity = await client.CreateEdgebandEntity(edgebandEntityRequest);

Console.WriteLine($"Created edgeband entity: {newEdgebandEntity.Id}");
```
When creating a edgeband entity you have the option to choose between the management types `ManagementType.Single`, `ManagementType.Stack` and `ManagementType.GoodsInStock`. 

When choosing `ManagementType.Single` the quantity has to be set to 1. There must be a unique id given for every single entity. 
By choosing `ManagementType.Stack` you can add a quantity greater or euqal to 1 allowing to stack multiple entities within the same Id.
With `ManagementType.GoodsInStock` it is possible to stack the entities within the same id and additionally, when a location is set, new `ManagementType.GoodsInStock` entities of the same edgeband type will be added to this entity id when same location is used.


<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
var edgebandTypeRequest = new MaterialManagerRequestEdgebandType() 
{
    EdgebandCode = "EB_White_1mm",
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 50.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = EdgebandingProcess.Other,
};
var newEdgebandEntity = await client.CreateEdgebandType(edgebandTypeRequest); 
```
