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
        MprFileName = mprA.Name, // The reference to the mpr file
        MaterialCode = "P2_Gold Craft Oak_19",
        Grain = Grain.Lengthwise,
        Quantity = 2
    });

    mprFiles.Add(mprA);
}

// The mpr files need to be added to the request.

var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());
```

> For a detailed example, please refer to <i>NestingRequest_ObjectModel_RequiredProperties_ImportOnly</i> in the file [NestingRequestUsingObjectModelSamples.cs](NestingRequestUsingObjectModelSamples.cs).

