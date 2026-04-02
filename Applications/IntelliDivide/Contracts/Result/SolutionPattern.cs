#nullable enable

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides access to cutting or nesting pattern properties.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionPattern : IHasMaterialCode
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets the board code.
        /// </summary>
        [JsonProperty(Order = 3)]
        public string BoardCode
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets the cycle number.
        /// </summary>
        [JsonProperty(Order = 6)]
        public int CycleNumber { get; set; }

        /// <summary>
        /// Gets the quantity of cycles the pattern will get produced.
        /// </summary>
        [JsonProperty(Order = 5)]
        public int Cycles { get; set; }

        /// <summary>
        /// Gets the pattern id.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets a link to a preview image of the pattern.
        /// </summary>
        [JsonProperty(Order = 5)]
        public Uri Preview { get; set; }

        /// <summary>
        /// Gets or sets the name of the generated nesting program for the pattern.
        /// </summary>
        public string ProgramName
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets the total quantity in which the pattern will get produced.
        /// </summary>
        [JsonProperty(Order = 4)]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 2)]
        public string MaterialCode
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets or sets the waste percentage of the pattern.
        /// Unit for metric and imperial unit systems: percent (%).
        /// </summary>
        /// <example>5.5</example>
        [JsonProperty(Order = 7)]
        [Range(0, 100)]
        public double Waste { get; set; }

        /// <summary>
        /// Gets or sets the waste area of the pattern.
        /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>10.9</example>
        [JsonProperty(Order = 8)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Range(0, double.MaxValue)]
        public double WasteArea { get; set; }

        /// <summary>
        /// Gets or sets the waste and offcuts percentage of the pattern.
        /// Unit for metric and imperial unit systems: percent (%).
        /// </summary>
        /// <example>10.5</example>
        [JsonProperty(Order = 9)]
        [Range(0, 100)]
        public double WastePlusOffcuts { get; set; }

        /// <summary>
        /// Gets or sets the waste and offcuts area of the pattern.
        /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>21.9</example>
        [JsonProperty(Order = 10)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Range(0, double.MaxValue)]
        public double WastePlusOffcutsArea { get; set; }

        /// <summary>
        /// Gets or sets the area of produced offcuts of the pattern.
        /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>11.0</example>
        [JsonProperty(Order = 11)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Range(0, double.MaxValue)]
        public double OffcutsProducedArea { get; set; }

        /// <summary>
        /// Gets or sets the number of produced offcuts of the pattern.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>1</example>
        [JsonProperty(Order = 12)]
        [Range(0, int.MaxValue)]
        public int OffcutsProduced { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts of the pattern.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>5</example>
        [JsonProperty(Order = 13)]
        [Range(0, int.MaxValue)]
        public int QuantityOfParts { get; set; }

        /// <summary>   
        /// Gets or sets the parts area of the pattern.
        /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>14.9</example>
        [JsonProperty(Order = 14)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Range(0, double.MaxValue)]
        public double PartArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of head cuts of the pattern.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>0</example>
        [JsonProperty(Order = 15)]
        [Range(0, int.MaxValue)]
        public int HeadCuts { get; set; }

        /// <summary>
        /// Gets or sets the quantity of recuts of the pattern.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>1</example>
        [JsonProperty(Order = 16)]
        [Range(0, int.MaxValue)]
        public int Recuts { get; set; }

        /// <summary>
        /// Gets or sets the number of cuts of the pattern.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>12</example>
        [JsonProperty(Order = 17)]
        [Range(0, int.MaxValue)]
        public int Cuts { get; set; }

        /// <summary>
        /// Gets or sets the cutting length of the pattern.
        /// Unit: meters for <see cref="UnitSystem.Metric"/> and feet for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>148.95</example>
        [JsonProperty(Order = 18)]
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        [Range(0, double.MaxValue)]
        public double CuttingLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum book height of the pattern.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>38.0</example>
        [JsonProperty(Order = 19)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.01, 999.9)]
        public double MaxBookHeight { get; set; }
    }
}