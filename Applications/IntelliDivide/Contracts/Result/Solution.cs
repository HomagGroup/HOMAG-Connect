using System;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.IntelliDivide.Contracts.Constants;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Represents a solution of a cutting or nesting optimization.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    [DebuggerDisplay("Id={Id}, OptimizationId={OptimizationId}, Name={Name}")]
    public class Solution : IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the unique identifier of the solution.
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the solution. See <see cref="SolutionName" /> for more details.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optimization id.
        /// </summary>
        [JsonProperty(Order = 3)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SolutionOverview" />.
        /// </summary>
        [JsonProperty(Order = 5)]
        public SolutionOverview Overview { get; set; } = new SolutionOverview();

        /// <summary>
        /// Gets or sets a flag indicating whether this solution was the one transferred to or not.
        /// </summary>
        [JsonProperty(Order = 15)]
        public bool WasTransferred { get; set; }

        /// <summary>
        /// Gets or sets the total score of the solution. The <see cref="SolutionName.BalancedSolution" /> has typically the
        /// highest score. The solutions are listed in the app sorted by the score (highest first).
        /// </summary>
        [JsonProperty(Order = 80)]
        public double TotalScore { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}