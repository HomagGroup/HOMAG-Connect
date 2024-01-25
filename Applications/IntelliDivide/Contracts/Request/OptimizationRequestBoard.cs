using System.Runtime.Serialization;

using HomagConnect.Base.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    public class OptimizationRequestBoard : IExtensibleDataObject
    {
        public string BoardCode { get; set; } = string.Empty;

        public double Costs { get; set; }

        public Grain Grain { get; set; }

        public double Length { get; set; }

        public string MaterialCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 999;

        public double Thickness { get; set; }

        public OptimizationBoardType Type { get; set; }

        public double Width { get; set; }

        public ExtensionDataObject ExtensionData { get; set; }
    }
}