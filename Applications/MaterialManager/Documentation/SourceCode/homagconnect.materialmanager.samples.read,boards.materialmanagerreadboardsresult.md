<span style="color:red">[This is preliminary documentation and is subject to change.] </span>
# MaterialManagerReadBoardsResults

Namespace: HomagConnect.MaterialManager.Samples.Read.Boards

The MaterialManagerReadBoardsResults class provides sample code for reading board materials through the materialManager Read API.

## Methods

### **GetMaterialCodes(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)**

Gets the material codes and traces them to the console.

```csharp
public static async Task GetMaterialCodes(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
```

#### Parameters

`IMaterialManagerClientMaterialBoards` materialManagerClientMaterialBoards

#### Return Value

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **GetThumbnails(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)**

Gets the board material thumbnails and traces them to the console.

```csharp
public static async Task GetThumbnails(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
```

#### Parameters

`IMaterialManagerClientMaterialBoards `materialManagerClientMaterialBoards

#### Return Value

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **GetLocations(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards, IEnumerable<string> boardCodes)**

Gets the locations of specified board materials and traces them to the console.

```csharp
public static async Task GetLocations(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards,
    IEnumerable<string> boardCodes)
```

#### Parameters

`IMaterialManagerClientMaterialBoards` materialManagerClientMaterialBoards
`IEnumerable<string>` boardCodes

#### Return Value

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
