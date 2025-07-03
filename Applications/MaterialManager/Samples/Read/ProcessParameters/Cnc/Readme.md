# Read board data

With the HOMAG Connect materialManager processing cnc client, the cnc milling process parameters, milling material groups and milling tool groups can be read.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientProcessingCnc(subscriptionId, authorizationKey);

var toolGroups = await client.GetMillingParameterToolGroups();
var materialGroups = await client.GetMillingParameterMaterialGroups();
var parameterGroups = await client.GetMillingParameterGroups();
```

    