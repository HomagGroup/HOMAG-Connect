<h1 id="createBoardType"> Create board type</h1>

With the HOMAG Connect materialManager boards client, new board types can be created.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeRequest = new MaterialManagerRequestBoardType
{
    //The material code is the identifier of the material type
    MaterialCode = "HPL_F274_9_12.0",
    //The board code is the identifier of the board type
    BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
    Length = 2070.0,
    Width = 2800.0,
    Thickness = 19.0,
    Type = BoardTypeType.Board,
    MaterialCategory = BoardMaterialCategory.Undefined,
    CoatingCategory CoatingCategory = CoatingCategory.Undefined,
    Grain = Grain.None,
};

var newBoardType = await client.CreateBoardType(boardTypeRequest);

Console.WriteLine($"Created Board Type: {newBoardType.Code}");
```

It is also possible to attatch additional data, like a image, to a board type at creation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeRequest = new MaterialManagerRequestBoardType
{
    //The material code is the identifier of the material type
    MaterialCode = "HPL_F274_9_12.0",
    //The board code is the identifier of the board type
    BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
    Length = 2070.0,
    Width = 2800.0,
    Thickness = 19.0,
    Type = BoardTypeType.Board,
    MaterialCategory = BoardMaterialCategory.Undefined,
    CoatingCategory CoatingCategory = CoatingCategory.Undefined,
    Grain = Grain.None,
};

var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
var fileReferences = new FileReference[]
{
    new FileReference("BoardPicture", testFilePath)
};

var newBoardType = await client.CreateBoardType(boardTypeRequest, fileReferences);

Console.WriteLine($"Created Board Type: {newBoardType.Code}");
```

