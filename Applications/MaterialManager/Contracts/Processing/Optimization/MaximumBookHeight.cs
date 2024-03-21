using System.Diagnostics;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    [DebuggerDisplay("{Type.ToString()}, {Value}")]
    public class MaximumBookHeight
    {
        public MaximumBookHeightType Type { get; set; }

        public double Value { get; set; }
    }
}