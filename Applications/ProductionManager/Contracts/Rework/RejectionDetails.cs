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
        /// Additional data.
        /// </summary>
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Rejected by user
        /// </summary>
        public string? RejectedBy { get; set; }

        /// <summary>
        /// Date and time when the item was rejected.
        /// </summary>
        public DateTimeOffset? RejectedOn { get; set; }

        /// <summary>
        /// Rejection comment
        /// </summary>
        public string? Comment { get; set; }
    }
}