# ImportFile

Namespace: HomagConnect.IntelliDivide.Contracts.Base

Import file

```csharp
public class ImportFile
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ImportFile](./homagconnect.intellidivide.contracts.base.importfile.md)

## Properties

### **Name**

File name

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Stream**

File content as stream

```csharp
public Stream Stream { get; set; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **ImportFile()**

```csharp
public ImportFile()
```

## Methods

### **CreateAsync(FileInfo)**

```csharp
public static Task<ImportFile> CreateAsync(FileInfo fileInfo)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task&lt;ImportFile&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
