# OffcutParameters

Namespace: HomagConnect.MaterialManager.Contracts.Processing.Optimization

Model for material dependent offcut parameters.

```csharp
public class OffcutParameters
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OffcutParameters](./homagconnect.materialmanager.contracts.processing.optimization.offcutparameters.md)

## Properties

### **Enabled**

Gets or sets whether the offcut parameters are enabled.

```csharp
public bool Enabled { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **MinimumArea**

Gets or sets the minimum area of the offcut. The value is dependent on the unit system (Metric: m², Imperial: ft²).

```csharp
public Nullable<double> MinimumArea { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MinimumLength**

Gets or sets the minimum length of the offcut. The value is dependent on the unit system (Metric: mm, Imperial: inch).

```csharp
public Nullable<double> MinimumLength { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MinimumWidth**

Gets or sets the minimum width of the offcut. The value is dependent on the unit system (Metric: mm, Imperial: inch).

```csharp
public Nullable<double> MinimumWidth { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Value**

Gets or sets the value of the offcut in %.

```csharp
public Nullable<double> Value { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **OffcutParameters()**

```csharp
public OffcutParameters()
```
