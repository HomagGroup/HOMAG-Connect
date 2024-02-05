# OptimizationStatus

Namespace: HomagConnect.IntelliDivide.Contracts

The state of an optimization

```csharp
public enum OptimizationStatus
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Unknown | 0 | Fallback state, if the state is not known; should normally not happen |
| New | 1 | The has been saved but the optimization has been started yet. |
| Started | 2 | The execution of the optimization has been started. |
| Canceled | 3 | The execution of the optimization was canceled. |
| Faulted | 4 | The execution failed. |
| Optimized | 5 | The execution was finished successfully. |
| Archived | 6 | The job has been archived |
| Transferred | 7 | The optimization result has been downloaded or transferred to machine. |
