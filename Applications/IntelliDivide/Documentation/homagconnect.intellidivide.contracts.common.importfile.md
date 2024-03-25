# ImportFile

Namespace: HomagConnect.IntelliDivide.Contracts.Common

Wrapper for handling of import files.

```csharp
public class ImportFile
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ImportFile](./homagconnect.intellidivide.contracts.common.importfile.md)

## Properties

### **Name**

Gets or sets the name which is used as reference.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Stream**

Gets or sets the content as stream

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

Creates a new instance of [ImportFile](./homagconnect.intellidivide.contracts.common.importfile.md).

```csharp
public static Task<ImportFile> CreateAsync(FileInfo fileInfo)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task&lt;ImportFile&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown, if the specified was not found.
