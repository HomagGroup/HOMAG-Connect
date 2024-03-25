# SolutionPattern

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Provides access to cutting or nesting pattern properties.

```csharp
public class SolutionPattern : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionPattern](./homagconnect.intellidivide.contracts.result.solutionpattern.md)<br>
Implements IExtensibleDataObject

## Properties

### **BoardCode**

Gets the board code.

```csharp
public string BoardCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CycleNumber**

Gets the cycle number.

```csharp
public int CycleNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Cycles**

Gets the the quantity of cycles the pattern will get produced.

```csharp
public int Cycles { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Id**

Gets the pattern id.

```csharp
public string Id { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaterialCode**

Get the material code.

```csharp
public string MaterialCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Preview**

Gets a link to a preview image of the pattern.

```csharp
public Uri Preview { get; set; }
```

#### Property Value

Uri<br>

### **Quantity**

Gets the total quantity in which the pattern will get produced.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionPattern()**

```csharp
public SolutionPattern()
```
