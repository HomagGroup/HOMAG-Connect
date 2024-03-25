# SolutionExportType

Namespace: HomagConnect.IntelliDivide.Contracts.Result

The type of the solution export.

```csharp
public enum SolutionExportType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [SolutionExportType](./homagconnect.intellidivide.contracts.result.solutionexporttype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Saw | 0 | SAW file containing the solution to be used with HOMAG machines. |
| Ptx | 1 | PTX file containing the solution to be used with non HOMAG machines. |
| Pdf | 2 | Handout describing the solution in pdf format. |
| MaterialDemand | 3 | Excel file containing the material demand. |
