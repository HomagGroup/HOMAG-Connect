using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

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
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_AdditionalData))]
        public AdditionalDataEntity AdditionalData { get; set; } = new();

        /// <summary>
        /// AdditionalProperties of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_AdditionalProperties))]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Comments of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_Comments))]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// CreatedAt of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_CreatedAt))]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// CreatedBy of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_CreatedBy))]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Name of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_Name))]
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Source of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_Source))]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Workstation of the allocation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.AllocationProperties_Workstation))]
        public string Workstation { get; set; } = string.Empty;
    }
}