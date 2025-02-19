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
        Id = "productionRack 1004",
        CustomerName = "HOMAG",
        OrderName = "Kitchen 123",
        OrderDate = DateTime.Today,
        OrderItem = "1.1",

        Notes = "This part shows how to set all properties.",

        Description = "Side panel right",
        MaterialCode = "P2_Gold_Craft_Oak_19.0",
        Grain = Grain.Lengthwise,
        Length = 400,
        Width = 200,

        Quantity = 1,
        QuantityPlus = 5,

        EdgeDiagram = "011:011:000:000",
        EdgeFront = "PP_White_1.3_22_HM",
        EdgeBack = "PP_White_1.3_22_HM",
        EdgeLeft = "PP_White_1.3_22_HM",
        EdgeRight = "PP_White_1.3_22_HM",

        LaminateTop = "HPL_U961_2_0.8",
        LaminateBottom = "HPL_U961_2_0.8",

        FinishLength = 400,
        FinishWidth = 200,

        SecondCutLength = 399,
        SecondCutWidth = 199,

        CncProgramName1 = "SortingS1004",
        CncProgramName2 = "SortingS1004_2",

        LabelLayout = "Label#1",

        Lot = "Lot #1",

        AdditionalProperties = new Dictionary<string, object> { { "DeliveryRegion", "North" } }
    });
```

> For a detailed example, please refer to <i>CuttingRequest_ObjectModel_AdditionalProperties_Optimize</i> in the file [CuttingRequestUsingObjectModelSamples.cs](CuttingRequestUsingObjectModelSamples.cs).

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
        Template = "2 Parts (2 x 1):1.1:1:1" // Position defined like in the app
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
        Template = new GrainMatchTemplateReference // Position defined in as structured object
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

### For creating a grain match template use the following: 
##### Parameter "GrainPattern"

  GrainPattern                | Description 
------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------
                              | The grain match template has 12 default templates and their names are localized with the language set into Tapio account for a particular subscription
                              | English names are: "2 Parts (2 x 1)", "2 Parts (1 x 2)", "2+1 part, longitudinal right (2 x 2)", "1+2 part, longitudinal left (2 x 2)", "2+1 part, crosswise bottom (2 x 2)",
                              | "1+2 part, crosswise top (2 x 2)", "3 Parts (3 x 1)", "3 Parts (1 x 3)", "4 Parts (2 x 3)", "4 Parts (2 x 2)", "4 Parts (1 x 4)", "4 Parts (4 x 1)".
                              | German names are: "2 Teile (2 x 1)", "2 Teile (1 x 2)", "2+1 Teil längs rechts (2 x 2)", "+2 Teil längs links (2 x 2)", "2+1 Teil quer unten (2 x 2)", "1+2 Teil quer oben (2 x 2)",
                              | "3 Teile (3 x 1)", "3 Teile (1 x 3)", "4 Teile (2 x 3)", "4 Teile (2 x 2)", "4 Teile (1 x 4)", "4 Teile (4 x 1)"
                              |
  `value=`                    | name delimiter is ":" and into (a x b) a is the number of columns and b is the number of rows
                              | into the example: value="2 Parts (2 x 1):1.1:1:0" => "2 Parts (2 x 1)" is the name of the grain match template, 2 columns and 1 row
                              |                                                 => "1.1" is the position into the template
                              |                                                 => "1" is instance of the template
							  |                                                 => "0" is the template grain ( 0 = None; 1 = Lengthwise grain; 2 = Cross grain => similar as for the parts;) all the positions into a template instance
							  |                                                       need to have the same grain
                              | 
                              | value can get also multiple positions for the template in case the parameter Quantity has a value bigger that 1 exp: value="2 Parts (2 x 1):1.1 2.1:1"
                              |
  `2 Parts (2 x 1)`           | available positions => 1.1 2.1 
                              |
  `2 Parts (1 x 2)`           |  available positions => 1.1 1.2
                              |  
  `2+1 part, longitudinal right (2 x 2)` |  available positions => 1.1 1.2 2.1
                              |  
  `1+2 part, longitudinal left (2 x 2)` |  available positions => 1.1 2.1 2.2
                              | 
  `2+1 part, crosswise bottom (2 x 2)` |  available positions => 1.1 1.2 2.1
                              | 
 `1+2 part, crosswise top (2 x 2)` |  available positions => 1.1 1.2 2.2
                              | 
 `3 Parts (3 x 1)`            |  available positions => 1.1 2.1 3.1
                              |
 `3 Parts (1 x 3)`            |  available positions => 1.1 1.2 1.3
                              |
 `4 Parts (2 x 3)`            |  available positions => 1.1 1.2 1.3 2.1
                              |
 `4 Parts (2 x 2)`            |  available positions => 1.1 1.2 2.1 2.2
                              |
 `4 Parts (1 x 4)`            |  available positions => 1.1 1.2 1.3 1.4
                              |
 `4 Parts (4 x 1)`            |  available positions => 1.1 2.1 3.1 4.1
  
  ![grain_templates.png](../../../../../../../DataExchange/Assets/grain_templates.png)                          
