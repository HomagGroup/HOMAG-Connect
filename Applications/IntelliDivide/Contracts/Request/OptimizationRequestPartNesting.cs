using System.Collections.Generic;
using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    public class OptimizationRequestPartNesting : OptimizationRequestPart
    {
        public string MprFileName { get; set; }

        public RotationAngle RotationAngle { get; set; } = RotationAngle.Angle90;

        public List<MprProgramVariable>? MprProgramVariables { get; set; }
    }
}