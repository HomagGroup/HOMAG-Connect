using System;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Base class for allocations in materialManager.
    /// </summary>
    public abstract class Allocation
    {
        /// <summary>
        /// AdditionalData of the allocation.
        /// </summary>
        public AdditionalDataEntity AdditionalData { get; set; } = new();

        /// <summary>
        /// AdditionalProperties of the allocation.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Comments of the allocation.
        /// </summary>
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// CreatedAt of the allocation.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// CreatedBy of the allocation.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Name of the allocation.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Source of the allocation.
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Workstation of the allocation.
        /// </summary>
        public string Workstation { get; set; } = string.Empty;
    }
}