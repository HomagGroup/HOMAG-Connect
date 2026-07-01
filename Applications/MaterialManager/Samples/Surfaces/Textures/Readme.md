# Read texture data

With the HOMAG Connect materialManager textures client, texture data can be retrieved from materialManager for further programmatic evaluation.

The texture client is accessed via the `Textures` property of the MaterialManager client.

## GetTexture

Retrieve a single texture by its ID.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

// Texture ID format: catalog_decorCode_embossing (lowercase, underscores)
var textureId = "hpl_f274_9_st7";

var texture = await client.GetTexture(textureId);

Console.WriteLine($"Texture: {texture.Id}");
Console.WriteLine($"Catalog: {texture.Catalog}");
Console.WriteLine($"DecorCode: {texture.DecorCode}");
Console.WriteLine($"Name: {texture.Name}");
```

## GetTextures

Retrieve textures with pagination support using continuation tokens. Use a `TextureFilter` to narrow results by `Catalog`, `DecorCode`, and/or `Embossing`; pass `null` or an empty `TextureFilter` to return all textures.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

// Get first page with default page size (100)
var result = await client.GetTextures(pageSize: 10);

Console.WriteLine($"Retrieved {result.Textures.Count} textures");

foreach (var texture in result.Textures)
{
    Console.WriteLine($"- {texture.Id} ({texture.Name})");
}

// Use continuation token for next page if available
if (!string.IsNullOrEmpty(result.ContinuationToken))
{
    var nextResult = await client.GetTextures(pageSize: 10, continuationToken: result.ContinuationToken);
    Console.WriteLine($"Next page has {nextResult.Textures.Count} textures");
}
```

## GetTextures by Catalog

Retrieve textures filtered by catalog.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

var result = await client.GetTextures(new TextureFilter { Catalog = "hpl" }, pageSize: 10);

Console.WriteLine($"Retrieved {result.Textures.Count} textures from catalog 'hpl'");

foreach (var texture in result.Textures)
{
    Console.WriteLine($"- {texture.DecorCode}");
}
```

## GetTextures by Catalog and DecorCode

Retrieve textures filtered by catalog and decor code.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

var result = await client.GetTextures(new TextureFilter { Catalog = "hpl", DecorCode = "f274_9" }, pageSize: 10);

Console.WriteLine($"Retrieved {result.Textures.Count} textures");

foreach (var texture in result.Textures)
{
    Console.WriteLine($"Embossing: {texture.Embossing}");
}
```

## GetTextures by Catalog, DecorCode and Embossing

Retrieve textures filtered by catalog, decor code and embossing.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

var result = await client.GetTextures(new TextureFilter { Catalog = "hpl", DecorCode = "f274_9", Embossing = "st7" }, pageSize: 10);

Console.WriteLine($"Retrieved {result.Textures.Count} textures");

foreach (var texture in result.Textures)
{
    Console.WriteLine($"- {texture.Id} ({texture.Name})");
}
```

# Import or Update texture

With the HOMAG Connect materialManager textures client, new textures can be imported or updated from Roomle material definitions.

## ImportOrUpdateTexture

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

// Create a Roomle material definition
var material = new Material
{
    // Roomle material ID (format: catalog:decorcode_embossing, lowercase)
    Id = "hpl:f274_9_st7",
    ExternalIdentifier = "HPL_F274_9_ST7",
    Label = "HPL F274 9 ST7",
    Catalog = "HPL",
    Active = true,
    Hidden = false,
    Created = DateTimeOffset.UtcNow,
    Updated = DateTimeOffset.UtcNow,
    Version = 1,
    Language = "en",
    VisibilityStatus = 0,
    Shading = new Shading
    {
        Version = "2.0.0",
        Alpha = 1.0,
        Basecolor = new ColorRgb { R = 1.0, G = 1.0, B = 1.0 },
        Metallic = 0,
        Roughness = 0.8
    },
    TextureObjects = [],
    Textures = [],
    Tags = ["HPL", "Decor"],
    Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
};

var materialDefinition = new MaterialDefinitionRoomle
{
    Material = material
};

var importedTexture = await client.ImportOrUpdate(materialDefinition);

Console.WriteLine($"Imported Texture: {importedTexture.Id}");
Console.WriteLine($"Name: {importedTexture.Name}");
Console.WriteLine($"Modified: {importedTexture.ModifiedAt}");
```

## ImportOrUpdateTexturesBatch

Import or update multiple textures in a single batch operation.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

var materials = new List<Material>
{
    new()
    {
        Id = "hpl:f274_9_st7",
        ExternalIdentifier = "HPL_F274_9_ST7",
        Label = "HPL F274 9 ST7",
        Catalog = "HPL",
        Active = true,
        Hidden = false,
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Version = 1,
        Language = "en",
        VisibilityStatus = 0,
        Shading = new Shading
        {
            Version = "2.0.0",
            Alpha = 1.0,
            Basecolor = new ColorRgb { R = 1.0, G = 1.0, B = 1.0 },
            Metallic = 0,
            Roughness = 0.8
        },
        TextureObjects = [],
        Textures = [],
        Tags = ["HPL"],
        Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
    },
    new()
    {
        Id = "hpl:f275_0_st9",
        ExternalIdentifier = "HPL_F275_0_ST9",
        Label = "HPL F275 0 ST9",
        Catalog = "HPL",
        Active = true,
        Hidden = false,
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Version = 1,
        Language = "en",
        VisibilityStatus = 0,
        Shading = new Shading
        {
            Version = "2.0.0",
            Alpha = 1.0,
            Basecolor = new ColorRgb { R = 0.9, G = 0.9, B = 0.9 },
            Metallic = 0,
            Roughness = 0.75
        },
        TextureObjects = [],
        Textures = [],
        Tags = ["HPL"],
        Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
    }
};

var materialDefinitions = new MaterialDefinitionsRoomle
{
    Materials = materials
};

var result = await client.ImportOrUpdate(materialDefinitions);

Console.WriteLine($"Imported {result.Length} textures");

foreach (var texture in result)
{
    Console.WriteLine($"- {texture.Id}: {texture.Name}");
}
```

# Delete texture

With the HOMAG Connect materialManager textures client, textures can be deleted.

**Example:**

```csharp
var client = new MaterialManagerClientTextures(subscriptionId, authorizationKey);

// Texture ID format from GetTexture result or computed as catalog_decorCode_embossing
var textureId = "hpl_f274_9_st7";

await client.DeleteTexture(textureId);

Console.WriteLine($"Deleted texture: {textureId}");
```
