# SolutionMaterial

Namespace: HomagConnect.IntelliDivide.Contracts.Result

```csharp
public class SolutionMaterial : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionMaterial](./homagconnect.intellidivide.contracts.result.solutionmaterial.md)<br>
Implements IExtensibleDataObject

## Properties

### **Boards**

```csharp
public IReadOnlyCollection<SolutionMaterialBoard> Boards { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialBoard&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Edgebands**

```csharp
public IReadOnlyCollection<SolutionMaterialEdgeband> Edgebands { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialEdgeband&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Offcuts**

```csharp
public IReadOnlyCollection<SolutionMaterialOffcut> Offcuts { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionMaterialOffcut&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **OffcutsProduced**

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
