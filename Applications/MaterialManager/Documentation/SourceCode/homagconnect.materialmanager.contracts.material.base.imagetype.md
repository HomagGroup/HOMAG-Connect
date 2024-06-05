# ImageType

Namespace: HomagConnect.MaterialManager.Contracts.Material.Base

Image types

```csharp
public enum ImageType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ImageType](./homagconnect.materialmanager.contracts.material.base.imagetype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Picture | 0 | The main picture. |
| Drawing | 1 | A technical drawing. |
| Measurement | 2 | A technical picture used for measurement that also shows identifier for measurement values. |
| SpecSheet | 3 | A specification sheet usually taken by the user from the manufacturer and uploaded |
| Label | 4 | A pdf containing a label used for printing |
| Texture | 5 | Image used for 3D rendering of f.e. cabinets |
