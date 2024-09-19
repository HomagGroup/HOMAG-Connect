# Retrieve optimization results

The HOMAG Connect IntelliDivide Client can be used to read the list of optimizations, solutions and solution details.

```c#
// Get the list of the first 3 optimizations of type cutting having the status optimized.
var optimizations = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 5);

// Get the optimization by id.
var optimization = await intelliDivide.GetOptimization(optimizations.First().Id);

// Get the solutions including the main key figures
var solutions = await intelliDivide.GetSolutions(optimization.Id).ToListAsync();
``` 

The list of solutions can be filtered by status and sorted by the main key figures.

```yaml
[
  {
    "Id": "a4efd0f4-751d-49fe-a0ee-89b5159cd04c",
    "Name": "Balanced Solution",
    "OptimizationId": "06319cf0-77d2-419f-9b64-c4a18b22ab94",
    "Overview": {
      "Figures": {
        "Material": {
          "Waste": 6.07,
          "WastePlusOffcuts": 61.18,
          "WholeBoards": 1,
          "OffcutsTotal": 1,
          "OffcutsProduced": 1
        },
        "Production": {
          "ProductionTime": "00:12:34",
          "ProductionTimePerPart": 10.93,
          "QuantityOfParts": 69,
          "Cycles": 1,
          "AverageBookHeight": 19.0
        },     
        #...
      },
      //...
  },
  {
    "Id": "5266998e-ad80-4a89-967f-74d6a45ec9b8",
    "Name": "OptimizedForOffcuts",
    //...
  }
]

``` 

The solution details can be retrieved by the solution id.

```c#
var balancedSolution = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First().Id);
``` 

```json
{
  "Id": "a4efd0f4-751d-49fe-a0ee-89b5159cd04c",
  "Name": "Balanced Solution",
  "OptimizationId": "06319cf0-77d2-419f-9b64-c4a18b22ab94",
  "Overview": {
    "Figures": {
      "Material": {
        "Waste": 6.0786749482401765,
        "WastePlusOffcuts": 61.18012422360248,
        "WholeBoards": 1,
        "OffcutsTotal": 1,
        "OffcutsProduced": 1
      },
      //...
    },
    "Pattern": [
      {
        "Id": "00001",
        "MaterialCode": "P2_White_19.0",
        "BoardCode": "P2_White_19.0_2800_2070",
        "Quantity": 1,
        "Cycles": 1,
        "CycleNumber": 1
      }
    ]
  },
  "Parts": [
    {
      "Quantity": 5,
      "Description": "Part A",
      "MaterialCode": "P2_White_19.0",
      "QuantityTotal": 5,
      "Length": 300.0,
      "Width": 300.0,
      "FinishLength": 0.0,
      "FinishWidth": 0.0
    },
   //...
  ],
  "Material": {
    "Boards": [
      {
        "MaterialCode": "P2_White_19.0",
        "BoardCode": "P2_White_19.0_2800_2070",
        "Length": 2800.0,
        "Width": 2070.0,
        "Thickness": 19.0,
        "Demand": 1
      }
    ],    
    "OffcutsProduced": [
      {
        "MaterialCode": "P2_White_19.0",
        "Length": 2800.0,
        "Width": 1140.6,
        "Thickness": 19.0,
        "Quantity": 1
      }
    ],
    //...
  },
  "Exports": [
    "Saw",
    "Ptx",
    "Pdf",
    "MaterialDemand",
    "ZIP"
  ],
  "TotalScore": 705.9885908328455
}
``` 

The exports can get downloaded by specifying the export type.

```c#
var sawFile = new FileInfo(optimization.Name + ".saw");
await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolution.Id, SolutionExportType.Saw, sawFile);
``` 

> For samples, please refer to [OptimizationRetrievalSamples.cs](OptimizationRetrievalSamples.cs).
