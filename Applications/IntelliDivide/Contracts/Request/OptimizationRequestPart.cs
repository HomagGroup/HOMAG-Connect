using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a part to be included in an intelliDivide optimization request. Extends <see cref="OptimizationBasePart" /> with
    /// quantity, grain matching template (cutting), and MPR program reference (nesting) properties.
    /// </summary>
    /// <example>
    /// {
    ///   "description": "Part A",
    ///   "materialCode": "P2_White_19.0",
    ///   "grain": "None",
    ///   "length": 600,
    ///   "width": 300,
    ///   "quantity": 10
    /// }
    /// </example>
    [DebuggerDisplay("{Description}, {MaterialCode}, {Length} x {Width}, #{Quantity}")]
    public class OptimizationRequestPart : OptimizationBasePart
    {
        #region (2a) Cutting

        /// <summary>
        /// Gets or sets the grain matching template reference for cutting optimizations. Specifies the template name,
        /// position within the template, instance, and options such as trims, dividing, and grain direction.
        /// </summary>
        /// <example>2 Parts (2 x 1):1.1:1:1</example>
        [JsonProperty(Order = 22)]
        public GrainMatchTemplateReference Template { get; set; }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Description}({MaterialCode})";
        }

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public OptimizationRequestPart() { }

        /// <summary>
        /// Creates a new instance using the given <paramref name="unitSystem" />.
        /// </summary>
        public OptimizationRequestPart(UnitSystem unitSystem) : base(unitSystem) { }

        #endregion

        #region (1) Required properties

        /// <summary>
        /// Gets or sets the required number of copies of this part. Must be between 1 and 10,000. Defaults to 1.
        /// </summary>
        /// <example>5</example>
        [Required]
        [JsonProperty(Order = 13)]
        [Range(1, 10000)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of additional copies that may be produced optionally. The optimizer may include
        /// up to this many extra parts when they fit the cutting or nesting layout. Must be between 0 and 10,000. Defaults to 0.
        /// </summary>
        /// <example>3</example>
        [JsonProperty(Order = 14)]
        [Range(0, 10000)]
        public int QuantityPlus { get; set; }

        #endregion

        #region (2b) Nesting

        /// <summary>
        /// Gets or sets the file name of the MPR program that defines the nesting contour for this part.
        /// The corresponding file must be included as an import file in the optimization request.
        /// </summary>
        /// <example>PartA.mpr</example>
        [JsonProperty(Order = 22)]
        public string MprFileName { get; set; }

        /// <summary>
        /// Gets or sets the variable values passed to the MPR program referenced by <see cref="MprFileName" />.
        /// Use these to parameterize the nesting contour (e.g. length and width).
        /// </summary>
        /// <example>
        /// [
        ///   { "name": "L", "value": "980" },
        ///   { "name": "B", "value": "450" }
        /// ]
        /// </example>
        [JsonProperty(Order = 23)]
        public Collection<MprProgramVariable> MprProgramVariables { get; set; }

        /// <summary>
        /// Gets or sets the allowed rotation angle for nesting optimizations. Determines how the part
        /// may be rotated on the board. Supported values: <c>Angle0</c>, <c>Angle90</c>, <c>Free</c>, or <c>Grain</c>
        /// (rotation restricted by grain direction). When <c>null</c>, the optimizer applies the default rotation behavior.
        /// </summary>
        /// <example>Angle90</example>
        [JsonProperty(Order = 24)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.OptimizationRequestPart_AllowedRotationAngle))]
        public RotationAngle? AllowedRotationAngle { get; set; }

        #endregion
    }
}