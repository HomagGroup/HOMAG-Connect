using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders 
{
    /// <summary>
    /// Describes the overview per period of the usage of the owned licenses 
    /// </summary>
    [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(UsageOverview))]
    public class UsageOverview : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// The month during which the licenses are active
        /// </summary>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(Period))]
        [JsonProperty(Order = 1)]
        public DateTime Period { get; set; }

        /// <summary>
        /// The licenses owned for the current period
        /// </summary>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(Licenses))]
        [JsonProperty(Order = 2)]
        public Collection<License> Licenses { get; set; }

        /// <summary>
        /// The quantity of parts that can be released for the current period
        /// </summary>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(QuantityOfPartsCoveredByTheLicenses))]
        [JsonProperty(Order = 3)]
        public int QuantityOfPartsCoveredByTheLicenses { get; set; }

        /// <summary>
        /// The quantity of parts that have been released for the current period
        /// </summary>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(QuantityOfReleasedParts))]
        [JsonProperty(Order = 4)]
        public int QuantityOfReleasedParts { get; set; }
    }
}
