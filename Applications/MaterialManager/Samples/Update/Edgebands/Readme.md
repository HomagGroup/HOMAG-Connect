# Update edgeband type

With the HOMAG Connect materialManager edgebands client, existing edgeband types can be updated.

## UpdateEdgebandType

**Example:**

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    Thickness = 1.1,
    // Add other properties
};

var updatedEdgebandType = await client.UpdateEdgebandType("EB_White_1mm", edgebandTypeUpdate);

Console.WriteLine($"Updated Edgeband Type: {updatedEdgebandType.Code}");
```

## UpdateEdgebandType with technology macro

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    MachineTechnologyMacro = new Dictionary<string, string>
    {
        { "hg0000000000", "ABS_1.00_RM_HM"}
    },
    // other properties
};

var updatedEdgebandType = await client.UpdateEdgebandType("ABS_White_1mm", edgebandTypeUpdate);
```

Available technology macro names and machines can be requested within the same client.

## UpdateEdgebandType with additional data
It is also possible to attatch additional data, like a image, to a board type at creation.

<strong>Example:</strong>

```csharp

var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
var additionalDataImage = new FileReference("Red.png", imageFilePath);

 var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
 {
    EdgebandCode = edgebandCode,
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 23.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = EdgebandingProcess.Other,
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

var updateEdgebandType = await materialManager.UpdateEdgebandType(edgebandCode, edgebandTypeUpdate, [additionalDataImage]);

Console.WriteLine($"Created Edgeband Type: {newEdgebandType.Code}");
```