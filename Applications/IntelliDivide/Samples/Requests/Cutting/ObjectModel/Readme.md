# Cutting optimization request using the object model

As for all optimization requests, you can specify the optimization name, the machine, and the parameters to be used.

```c#
request.Name = "Sample";
request.Machine = "productionAssist Cutting";
request.Parameters = "Default";
```

## Adding parts to the request

The object model allows you to specify the parts to be optimized. 

The sample below shows how to add a part to the request having the required properties set.

``` c#
request.Parts.Add(
    new()
    {
        Description = "Part (required properties)",
        MaterialCode = "P2_White_19",
        Length = 300,
        Width = 300,
        Quantity = 5
    });
```

> For a detailed example, please refer to <i>CuttingRequest_ObjectModel_RequiredProperties_ImportOnly</i> in the file [CuttingRequestUsingObjectModelSamples.cs](CuttingRequestUsingObjectModelSamples.cs).

All other properties which are available in the app can be set as well.

``` c#
request.Parts.Add(
    new()
    {
        Description = "Part (typical properties)",
        MaterialCode = "P2_Gold Craft Oak_19.0",
        Length = 400,
        Width = 200,
        Quantity = 1,

        Grain = Grain.Lengthwise,

        CustomerName = "HOMAG",
        OrderName = "Kitchen 123",

        EdgeDiagram = "011:011:000:000",
        EdgeFront = "PP_White_1.3_22_HM",
        EdgeBack = "PP_White_1.3_22_HM",
        EdgeLeft = "PP_White_1.3_22_HM",
        EdgeRight = "PP_White_1.3_22_HM",

        CncProgramName1 = "SortingS1004",
        CncProgramName2 = "SortingS1004_2"

        // and many more
    });
```

> For a detailed example, please refer to <i>CuttingRequest_ObjectModel_TypicalProperties_ImportOnly</i> in the file [CuttingRequestUsingObjectModelSamples.cs](CuttingRequestUsingObjectModelSamples.cs).

A position in a grain matching template can get referenced as specific formatted string or using a grain matching template object.

``` c#
request.Parts.Add(
    new()
    {
        Description = "Part A",
        MaterialCode = "OAK_19.0",
        Length = 800,
        Width = 600,
        Grain = Grain.Lengthwise,
        Quantity = 1,
        Template = "2 Parts (2 x 1):1.1:1:1"
    });
});

request.Parts.Add(
    new()
    {
        Description = "Part B",
        MaterialCode = "OAK_19.0",
        Length = 800,
        Width = 600,
        Grain = Grain.Lengthwise,
        Quantity = 1,
        Template = new GrainMatchTemplateReference
        {
            Template = "2 Parts (2 x 1)",
            Positions = new[]
            {
                new GrainMatchTemplatePosition
                {
                    Column = 2,
                    Row = 1
                }
            },
            Trims = GrainMatchingTemplateOptionsTrims.AllSides,
            Dividing = GrainMatchingTemplateOptionsDividing.SeparatePattern,
            Grain = Grain.Lengthwise,
            Instance = 1
        }                   
    });
```

> For a detailed example, please refer to <i>CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly</i> in the file [CuttingRequestUsingObjectModelSamples.cs](CuttingRequestUsingObjectModelSamples.cs).