# OffcutParameterSet

Namespace: HomagConnect.MaterialManager.Contracts.Processing.Optimization

```csharp
public class OffcutParameterSet : System.ComponentModel.DataAnnotations.IValidatableObject, HomagConnect.Base.Contracts.Interfaces.IContainsUnitSystemDependentProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OffcutParameterSet](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameterset.md)<br>
Implements IValidatableObject, IContainsUnitSystemDependentProperties

## Properties

### **MaterialGroupName**

Gets or sets the name of the material group.

```csharp
public string MaterialGroupName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaterialCodes**

Gets or sets the material codes for which the [OffcutParameterSet](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameterset.md) is valid.

```csharp
public String[] MaterialCodes { get; set; }
```

#### Property Value

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaterialManagerLink**

Gets or sets the [OffcutParameterSet.MaterialManagerLink](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameterset.md#materialmanagerlink).

```csharp
public string MaterialManagerLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OffcutParameters**

Gets or sets the [OffcutParameterSet.OffcutParameters](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameterset.md#offcutparameters).

```csharp
public OffcutParameters OffcutParameters { get; set; }
```

#### Property Value

[OffcutParameters](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameters.md)<br>

### **LargeOffcutParameters**

Gets or sets the [OffcutParameterSet.OffcutParameters](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameterset.md#offcutparameters).

```csharp
public OffcutParameters LargeOffcutParameters { get; set; }
```

#### Property Value

[OffcutParameters](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameters.md)<br>

### **UnitSystem**

```csharp
public UnitSystem UnitSystem { get; set; }
```

#### Property Value

UnitSystem<br>

## Constructors

### **OffcutParameterSet()**

```csharp
public OffcutParameterSet()
```

## Methods

### **Validate(ValidationContext)**

```csharp
public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
```

#### Parameters

`validationContext` ValidationContext<br>

#### Returns

[IEnumerable&lt;ValidationResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
