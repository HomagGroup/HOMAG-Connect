using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    public class OptimizationRequestPart : OptimizationBasePart
    {
        public string MprFileName { get; set; }

        public int Quantity { get; set; } = 1;

        public int QuantityPlus { get; set; } = 0;

        public string Template { get; set; }
    }
}