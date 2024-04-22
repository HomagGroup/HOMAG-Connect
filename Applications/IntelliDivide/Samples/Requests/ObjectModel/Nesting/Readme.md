# Nesting optimization request using the object model

As for all optimization requests, you can specify the optimization name, the machine, and the parameters to be used.

```c#
request.Name = "Sample";
request.Machine = "productionAssist Nesting";
request.Parameters = "Default";
```

## Adding parts to the request

The object model allows you to specify the parts to be optimized. 

The sample below shows how to add a part to the request having the required properties set.

``` c#

var mprFiles = new List<ImportFile>();

{ // Part A

    var mprA = await ImportFile.CreateAsync(new FileInfo(@"Requests\ObjectModel\Nesting\PartA.mpr"));

    request.Parts.Add(new OptimizationRequestPart
    {
        Description = "Part A",
        MprFileName = mprA.Name, // The reference to the import file.
        MaterialCode = "P2_Gold Craft Oak_19",
        Grain = Grain.Lengthwise,
        Quantity = 2
    });

    mprFiles.Add(mprA);
}

// The mpr files need to be added to the request.

var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles);
```

The file types MPR, MPRX and MPRXE are supported. The name of the ImportFile must match the <i>MprFileName</i> property of the <i>OptimizationRequestPart</i>.

> For a detailed example, please refer to <i>NestingRequest_ObjectModel_RequiredProperties_ImportOnly</i> in the file [NestingRequestUsingObjectModelSamples.cs](NestingRequestUsingObjectModelSamples.cs).

## Adapting MPRs by using MPR program variables.

If the MPR program has variables defined, they can get passed to the optimization request using the <i>MprProgramVariables</i> property of the <i>OptimizationRequestPart</i>.

``` c#
var mpr = await ImportFile.CreateAsync(new FileInfo(@"Requests\ObjectModel\Nesting\Generic.mpr"));
var mprReference = mpr.Name;
       
// Part A

request.Parts.Add(new OptimizationRequestPart
{
    Description = "Part A",
    MprFileName = mprReference,
    MaterialCode = "P2_Gold Craft Oak_19",
    Grain = Grain.Lengthwise,
    MprProgramVariables = new Collection<MprProgramVariable>
    {
        new() { Name = "L", Value = "980.0" }, // Set the length variable to 980.0
        new() { Name = "B", Value = "450.0" } // Set the width variable to 450.0
    },
    Quantity = 2
});

// Part B

request.Parts.Add(new OptimizationRequestPart
{
    Description = "Part B",
    MprFileName = mprReference,
    MaterialCode = "P2_Gold Craft Oak_19",
    Grain = Grain.Lengthwise,
    MprProgramVariables = new Collection<MprProgramVariable>
    {
        new() { Name = "L", Value = "720.0" }, // Set the length variable to 720.0
        new() { Name = "B", Value = "380.0" } // Set the width variable to 380.0
    },
    Quantity = 3
});

mprFiles.Add(mpr);

// Send the request
var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles);
```

By doing so, it becomes feasible to define one rectangular MPR and convey variables for length and width to the optimization request.
