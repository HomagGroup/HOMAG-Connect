# OptimizationRequestResponse

Namespace: HomagConnect.IntelliDivide.Contracts.Request

```csharp
public class OptimizationRequestResponse
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationRequestResponse](./homagconnect.intellidivide.contracts.request.optimizationrequestresponse.md)

## Properties

### **ErrorDetails**

Gets or sets the error details

```csharp
public string ErrorDetails { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ErrorText**

Gets or sets the error text

```csharp
public string ErrorText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Link**

```csharp
public Uri Link { get; set; }
```

#### Property Value

Uri<br>

### **OptimizationId**

```csharp
public Guid OptimizationId { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **OptimizationStatus**

```csharp
public OptimizationStatus OptimizationStatus { get; set; }
```

#### Property Value

[OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>

### **ValidationErrors**

```csharp
public OptimizationValidationError[] ValidationErrors { get; set; }
```

#### Property Value

[OptimizationValidationError[]](./homagconnect.intellidivide.contracts.request.optimizationvalidationerror.md)<br>

## Constructors

### **OptimizationRequestResponse()**

```csharp
public OptimizationRequestResponse()
```
