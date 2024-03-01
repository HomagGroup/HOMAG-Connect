# OptimizationMachine

Namespace: HomagConnect.IntelliDivide.Contracts.Common

Machine for which the optimization is performed.

```csharp
public class OptimizationMachine
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationMachine](./homagconnect.intellidivide.contracts.common.optimizationmachine.md)

## Properties

### **Name**

Gets or sets the name of the machine.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationType**

Gets or sets the [OptimizationMachine.OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationmachine.md#optimizationtype).

```csharp
public OptimizationType OptimizationType { get; set; }
```

#### Property Value

[OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>

### **SupportsSending**

Gets or sets whether sending is supported. Only productionAssist Cutting / Nesting and machine connected to tapio are supported.

```csharp
public bool SupportsSending { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **OptimizationMachine()**

```csharp
public OptimizationMachine()
```
