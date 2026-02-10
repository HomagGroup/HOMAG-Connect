#nullable enable
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.IntelliDivide.Contracts.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Represents a solution of a cutting or nesting optimization.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    [DebuggerDisplay("Id={Id}, OptimizationId={OptimizationId}, Name={Name}")]
    public class Solution : IContainsUnitSystemDependentProperties
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        public Solution() : this(UnitSystem.Metric) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class with the specified unit system.
        /// </summary>
        public Solution(UnitSystem unitSystem) 
        {
            UnitSystem = unitSystem;
        }

        #endregion

        /// <summary>
        /// Gets or sets the unique identifier of the solution.
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the solution. See <see cref="SolutionName" /> for more details.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Name
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the solution.
        /// </summary>
        [JsonProperty(Order = 5)]
        public string Description
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets or sets the primary characteristic of the solution.
        /// </summary>
        [JsonProperty(Order = 3)]
        public SolutionCharacteristic Characteristic { get; set; } = SolutionCharacteristic.Unknown;

        /// <summary>   
        /// Gets or sets the characteristics in addition to the primary characteristic of the solution.
        /// </summary>
        [JsonProperty(Order = 4)]
        public SolutionCharacteristic[]? CharacteristicsInAddition { get; set; } 

        /// <summary>
        /// Gets or sets the optimization id.
        /// </summary>
        [JsonProperty(Order = 3)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SolutionOverview" />.
        /// </summary>
        [JsonProperty(Order = 8)]
        public SolutionOverview Overview { get; set; } = new();
        
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
        
        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc/>
        [JsonProperty(Order = 99)]
        public UnitSystem UnitSystem { get; set; }

        #endregion
    }
}