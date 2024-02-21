using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Describes a part which in context of an optimization.
    /// </summary>
    [DebuggerDisplay("{Description}, {MaterialCode}, {Length} x {Width}")]
    public class OptimizationBasePart : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        /// <inheritdoc cref="IExtensibleDataObject" />
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        #region (1) Required properties

        /// <summary>
        /// Gets or sets a description for the part.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        [StringLength(50, MinimumLength = 1)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Grain" /> of the part.
        /// </summary>
        [Required]
        [JsonProperty(Order = 12)]
        public Grain Grain { get; set; } = Grain.None;

        #endregion

        #region (2) Cutting / Nesting

        /// <summary>
        /// Gets or sets the length of the part.
        /// </summary>
        [JsonProperty(Order = 20)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the width of the part.
        /// </summary>
        [JsonProperty(Order = 21)]
        public double Width { get; set; }

        #endregion

        #region (3) Edgebanding

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
        /// </summary>
        [JsonProperty(Order = 31)]
        public string EdgeFront { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
        /// </summary>
        [JsonProperty(Order = 32)]
        public string EdgeBack { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
        /// </summary>
        [JsonProperty(Order = 33)]
        public string EdgeLeft { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
        /// </summary>
        [JsonProperty(Order = 34)]
        public string EdgeRight { get; set; }

        /// <summary>
        /// Gets or sets how the edgebands should get applied.
        /// </summary>
        [JsonProperty(Order = 35)]
        public string EdgeDiagram { get; set; }

        #endregion

        #region (4) CNC processing

        /// <summary>
        /// Gets or sets the program name of the CNC program to execute.
        /// </summary>
        [JsonProperty(Order = 41)]
        public string CncProgramName1 { get; set; }

        /// <summary>
        /// Gets or sets the program name of the CNC program to execute.
        /// </summary>
        [JsonProperty(Order = 42)]
        public string CncProgramName2 { get; set; }

        #endregion

        #region (5) Laminating

        /// <summary>
        /// Gets or sets the material code of the laminate type which should get applied on the top.
        /// </summary>
        [JsonProperty(Order = 51)]
        public string LaminateTop { get; set; }

        /// <summary>
        /// Gets or sets the material code of the laminate type which should get applied on the bottom.
        /// </summary>
        [JsonProperty(Order = 52)]
        public string LaminateBottom { get; set; }

        #endregion

        #region (6) Order

        /// <summary>
        /// Gets or sets the name of the customer who has ordered the part.
        /// </summary>
        [JsonProperty(Order = 60)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        [JsonProperty(Order = 61)]
        public string OrderName { get; set; }

        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        [JsonProperty(Order = 62)]
        public double? FinishLength { get; set; }

        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        [JsonProperty(Order = 63)]
        public double? FinishWidth { get; set; }

        #endregion
    }
}