# OptimizationStatusDetails

Namespace: HomagConnect.IntelliDivide.Contracts



```csharp
public class OptimizationStatusDetails
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationStatusDetails](./homagconnect.intellidivide.contracts.optimizationstatusdetails.md)

## Properties

### **Error**

Error message

```csharp
public string Error { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **StartedAt**

The time the optimization was started

```csharp
public DateTimeOffset StartedAt { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **StatePercentage**

The percentage of the optimization

```csharp
public Nullable<double> StatePercentage { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Status**

The state of the optimization

```csharp
public OptimizationStatus Status { get; set; }
```

#### Property Value

[OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>

### **ValidationErrors**

A list of possible validation errors

```csharp
public IList<string> ValidationErrors { get; set; }
```

#### Property Value

[IList&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)<br>

## Constructors

### **OptimizationStatusDetails()**

```csharp
public OptimizationStatusDetails()
```
