using HomagConnect.IntelliDivide.Contracts.Common;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    public class OptimizationRequestPart : OptimizationBasePart
    {
        public int Quantity { get; set; } = 1;

        public int QuantityPlus { get; set; } = 0;

        public string MprFileName { get; set; }

        public RotationAngle RotationAngle { get; set; } = RotationAngle.Angle90;

        public List<MprProgramVariable>? MprProgramVariables { get; set; }

        public string Template { get; set; }
    }
}