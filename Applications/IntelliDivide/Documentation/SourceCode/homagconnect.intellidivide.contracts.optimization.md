# Optimization

Namespace: HomagConnect.IntelliDivide.Contracts



```csharp
public class Optimization
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Optimization](./homagconnect.intellidivide.contracts.optimization.md)

## Properties

### **Id**

Gets or sets the optimization id.

```csharp
public Guid Id { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **LastModifiedDate**

Gets or sets the last datetime the optimization was modified.

```csharp
public DateTime LastModifiedDate { get; set; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **Link**

Gets or sets a link to open the optimization in the app.

```csharp
public Uri Link { get; set; }
```

#### Property Value

Uri<br>

### **Machine**

Gets or sets the machine the optimization is done for.

```csharp
public string Machine { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

Gets or sets the optimization name.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationType**

Gets or sets the [Optimization.OptimizationType](./homagconnect.intellidivide.contracts.optimization.md#optimizationtype)

```csharp
public OptimizationType OptimizationType { get; set; }
```

#### Property Value

[OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>

### **ParameterName**

Gets or sets the optimization parameter set name.

```csharp
public string ParameterName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PartsCount**

#### Caution

Replaced with QuantityOfParts

---



```csharp
public int PartsCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ProductionTime**

Gets or sets the estimated production time.

```csharp
public TimeSpan ProductionTime { get; set; }
```

#### Property Value

[TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

### **QuantityOfParts**

Gets or sets the quantity of parts.

```csharp
public int QuantityOfParts { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Scrap**

#### Caution

Replaced with Waste

---



```csharp
public double Scrap { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Status**

Gets or sets the status.

```csharp
public OptimizationStatus Status { get; set; }
```

#### Property Value

[OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>

### **Waste**

Gets or sets the waste.

```csharp
public double Waste { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **Optimization()**

```csharp
public Optimization()
```
