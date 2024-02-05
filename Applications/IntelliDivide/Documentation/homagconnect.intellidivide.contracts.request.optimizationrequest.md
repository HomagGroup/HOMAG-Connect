# OptimizationRequest

Namespace: HomagConnect.IntelliDivide.Contracts.Request

```csharp
public class OptimizationRequest
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md)

## Properties

### **Action**

```csharp
public OptimizationRequestAction Action { get; set; }
```

#### Property Value

[OptimizationRequestAction](./homagconnect.intellidivide.contracts.request.optimizationrequestaction.md)<br>

### **Boards**

Optional. If no boards are provided the required boards are retrieved from materialManager.

```csharp
public List<OptimizationRequestBoard> Boards { get; set; }
```

#### Property Value

[List&lt;OptimizationRequestBoard&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **ImportTemplate**

```csharp
public string ImportTemplate { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Machine**

Optional. If no machine is provided the first cutting machine sorted by name is used.

```csharp
public string Machine { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Name**

Optional. If no name is provided it gets automatically generated.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Parameters**

Optional. If no parameter is provided the first parameter, sorted by name which fits the machine type is used.

```csharp
public string Parameters { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Parts**

```csharp
public List<OptimizationRequestPart> Parts { get; set; }
```

#### Property Value

[List&lt;OptimizationRequestPart&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

## Constructors

### **OptimizationRequest()**

```csharp
public OptimizationRequest()
```
