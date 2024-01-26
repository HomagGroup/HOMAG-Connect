using System.Runtime.Serialization;

using HomagConnect.IntelliDivide.Contracts.Base;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    public class OptimizationBasePart : IExtensibleDataObject
    {
        public string CncProgramName1 { get; set; }

        public string CncProgramName2 { get; set; }

        public string CustomerName { get; set; }

        [JsonProperty(Order = 1)]
        public string Description { get; set; } = string.Empty;

        public string EdgeBack { get; set; }

        public string EdgeDiagram { get; set; }

        public string EdgeFront { get; set; }

        public string EdgeLeft { get; set; }

        public string EdgeRight { get; set; }

        public double? FinishLength { get; set; }

        public double? FinishWidth { get; set; }

        public Grain Grain { get; set; } = Grain.None;

        public string LaminateBottom { get; set; }

        public string LaminateTop { get; set; }

        [JsonProperty(Order = 3)]
        public double Length { get; set; }

        [JsonProperty(Order = 2)]
        public string MaterialCode { get; set; } = string.Empty;

        public string OrderName { get; set; }

        [JsonProperty(Order = 4)]
        public double Width { get; set; }

        public ExtensionDataObject ExtensionData { get; set; }
    }
}