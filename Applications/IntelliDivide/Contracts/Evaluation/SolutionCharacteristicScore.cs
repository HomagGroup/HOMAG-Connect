#nullable enable
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

public class SolutionCharacteristicScore
{
    public SolutionCharacteristic Characteristic { get; set; }

    public Dictionary<string, int> Weights { get; set; }

    public double Score { get; set; }
}