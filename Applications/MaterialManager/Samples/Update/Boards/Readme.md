# Update board type

With the HOMAG Connect materialManager boards client, existing board types can be updated. 

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeUpdate = new MaterialManagerUpdateBoardType
{
    Length = 500.0,
    // Add other properties
};

var updatedBoardType = await client.UpdateBoardType("HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);

Console.WriteLine($"Updated Board Type: {updatedBoardType.Code}"); 
```

# Update board type
It is also possible to attatch additional data, like a image, to a board type at creation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
var additionalDataImage = new FileReference("Red.png", imageFilePath);

var boardTypeUpdate = new MaterialManagerUpdateBoardType
{
    MaterialCode = materialCode,
    BoardCode = boardCode,
    Length = 4100.0,
    Width = 650.0,
    Thickness = 12.0,
    Type = BoardTypeType.Board,
    MaterialCategory = BoardMaterialCategory.Undefined,
    CoatingCategory = CoatingCategory.Undefined,
    Grain = Grain.None,
    AdditionalData = new List<AdditionalDataEntity>
    {
        new AdditionalDataImage
        {
            Category = "Decor",
            DownloadFileName = additionalDataImage.Reference,
            DownloadUri = new Uri(additionalDataImage.Reference, UriKind.Relative)
        }
    }
};

var updateBoardType = await materialManager.UpdateBoardType(boardCode, boardTypeUpdate, [additionalDataImage]);

Console.WriteLine($"Updated Board Type: {updateBoardType.Code}");
```
