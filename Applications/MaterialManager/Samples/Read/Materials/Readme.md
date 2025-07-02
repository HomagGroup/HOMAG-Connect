# Read material data

With the HOMAG Connect materialManager boards client, material data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Retrieve the first 10 materials, skipping none: 
var materials = await client.GetMaterials(take: 10, skip: 0);

foreach (var material in materials) 
{ 
    Console.WriteLine($"Code: {material.Code}, Name: {string.Join(", ", material.ProductName)}"); 
}
```
