# TolerantEnumConverter

Namespace: HomagConnect.IntelliDivide.Contracts.Converter

We use this converter in order to extend in the future enum entries without breaking the deserialization of old
 assembly versions
 See also: https://stackoverflow.com/questions/22752075/how-can-i-ignore-unknown-enum-values-during-json-deserialization

```csharp
public class TolerantEnumConverter : Newtonsoft.Json.JsonConverter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → [TolerantEnumConverter](./homagconnect.intellidivide.contracts.converter.tolerantenumconverter.md)

## Properties

### **CanRead**

```csharp
public bool CanRead { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanWrite**

```csharp
public bool CanWrite { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **TolerantEnumConverter()**

```csharp
public TolerantEnumConverter()
```

## Methods

### **CanConvert(Type)**

```csharp
public bool CanConvert(Type objectType)
```

#### Parameters

`objectType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadJson(JsonReader, Type, Object, JsonSerializer)**

```csharp
public object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
```

#### Parameters

`reader` JsonReader<br>

`objectType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`existingValue` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`serializer` JsonSerializer<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **WriteJson(JsonWriter, Object, JsonSerializer)**

```csharp
public void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
```

#### Parameters

`writer` JsonWriter<br>

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`serializer` JsonSerializer<br>
