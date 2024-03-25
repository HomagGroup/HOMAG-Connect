# SolutionMaterial

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Represents the material used in a solution.

```csharp
public class SolutionMaterial : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionMaterial](./homagconnect.intellidivide.contracts.result.solutionmaterial.md)<br>
Implements IExtensibleDataObject

## Properties

### **Boards**

Gets or sets the boards used in the solution.

```csharp
public IReadOnlyCollection<SolutionMaterialBoard> Boards { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialBoard&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Edgebands**

Gets or sets the edgebands used in the solution.

```csharp
public IReadOnlyCollection<SolutionMaterialEdgeband> Edgebands { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialEdgeband&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Offcuts**

Gets or sets the offcuts used in the solution.

```csharp
public IReadOnlyCollection<SolutionMaterialOffcut> Offcuts { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialOffcut&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **OffcutsProduced**

Gets or sets the offcuts produced in the solution.

```csharp
public IReadOnlyCollection<SolutionMaterialOffcutProduced> OffcutsProduced { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialOffcutProduced&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionMaterial()**

```csharp
public SolutionMaterial()
```
