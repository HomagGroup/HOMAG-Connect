# OptimizationRequestAction

Namespace: HomagConnect.IntelliDivide.Contracts.Request

Defines the actions which should get performed on a the request.

```csharp
public enum OptimizationRequestAction
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [OptimizationRequestAction](./homagconnect.intellidivide.contracts.request.optimizationrequestaction.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| ImportOnly | 0 | Creates a new optimization in intelliDivide using the provided data. |
| Optimize | 1 | Creates a new optimization in intelliDivide using the provided data and executes the optimization. |
| Send | 2 | Creates a new optimization in intelliDivide using the provided data, executes the optimization and sends the balanced solution to the machine. |
