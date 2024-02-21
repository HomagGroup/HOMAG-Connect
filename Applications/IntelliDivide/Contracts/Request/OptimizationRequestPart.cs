using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.IntelliDivide.Contracts.Common;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Describes a part which should get optimized.
    /// </summary>
    [DebuggerDisplay("{Description}, {MaterialCode}, {Length} x {Width}, #{Quantity}")]
    public class OptimizationRequestPart : OptimizationBasePart
    {
        #region (2a) Cutting

        /// <summary>
        /// Gets or sets the name and position with in a grain matching template.
        /// </summary>
        [JsonProperty(Order = 22)]
        public string Template { get; set; }

        #endregion

        #region (1) Required properties

        /// <summary>
        /// Gets or sets the quantity how often the part is needed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the quantity how often the part can get produced optional.
        /// </summary>
        [JsonProperty(Order = 14)]
        public int QuantityPlus { get; set; }

        #endregion

        #region (2b) Nesting

        /// <summary>
        /// Gets or sets the name of the mpr file which describes the nesting contour.
        /// </summary>
        [JsonProperty(Order = 22)]
        public string MprFileName { get; set; }

        /// <summary>
        /// Gets or sets the mpr program variable values.
        /// </summary>
        [JsonProperty(Order = 23)]
        public List<MprProgramVariable> MprProgramVariables { get; set; }

        /// <summary>
        /// Gets or sets the allowed rotation angle.
        /// </summary>
        [JsonProperty(Order = 24)]
        public RotationAngle? AllowedRotationAngle { get; set; }

        #endregion

        /// <inheritdoc />
     public override string ToString()
        {
            return $"{Description}({MaterialCode})";
        }
    }
}