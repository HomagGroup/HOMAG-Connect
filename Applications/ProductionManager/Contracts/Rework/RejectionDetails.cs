using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rejection details
    /// </summary>
    public class RejectionDetails
    {
        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Rejected by user
        /// </summary>
        public string? RejectedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was rejected.
        /// </summary>
        public DateTimeOffset? RejectedOn { get; set; }
    }
}