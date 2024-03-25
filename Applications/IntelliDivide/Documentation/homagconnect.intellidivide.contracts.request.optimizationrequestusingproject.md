# OptimizationRequestUsingProject

Namespace: HomagConnect.IntelliDivide.Contracts.Request

Optimization request class to use to create requests using a project file.

```csharp
public class OptimizationRequestUsingProject : OptimizationRequestBase, System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationRequestBase](./homagconnect.intellidivide.contracts.request.optimizationrequestbase.md) → [OptimizationRequestUsingProject](./homagconnect.intellidivide.contracts.request.optimizationrequestusingproject.md)<br>
Implements IExtensibleDataObject

## Properties

### **Action**

Gets or sets the [OptimizationRequestAction](./homagconnect.intellidivide.contracts.request.optimizationrequestaction.md).

```csharp
public OptimizationRequestAction Action { get; set; }
```

#### Property Value

[OptimizationRequestAction](./homagconnect.intellidivide.contracts.request.optimizationrequestaction.md)<br>

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

### **Boards**

Optional. If no boards are provided the required boards are retrieved from materialManager.

```csharp
public List<OptimizationRequestBoard> Boards { get; set; }
```

#### Property Value

[List&lt;OptimizationRequestBoard&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **OptimizationRequestUsingProject()**

```csharp
public OptimizationRequestUsingProject()
```
