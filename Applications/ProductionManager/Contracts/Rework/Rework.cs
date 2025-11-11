using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework class
    /// </summary>
    public class Rework
    {
        /// <summary>
        /// Rework captured at
        /// </summary>
        public DateTimeOffset? CapturedAt { get; set; }

        /// <summary>
        /// Rework category
        /// </summary>
        public ReworkCategory Category { get; set; }

        /// <summary>
        /// Description of the rework
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the rework
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Material of the parts to rework
        /// </summary>
        public string? Material { get; set; }

        /// <summary>
        /// Name of the order
        /// </summary>
        public string? OrderName { get; set; }

        /// <summary>
        /// Quantity of parts to rework
        /// </summary>
        public int QuantityOfReworks { get; set; }

        /// <summary>
        /// Reason for the rework
        /// </summary>
        public ReworkReason Reason { get; set; }

        /// <summary>
        /// Rejection details if available
        /// </summary>
        public RejectionDetails? RejectionDetails { get; set; } = null;

        /// <summary>
        /// Rework state
        /// </summary>
        public ReworkState State { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public Collection<AdditionalDataEntity>? Attachments { get; set; }
    }
}