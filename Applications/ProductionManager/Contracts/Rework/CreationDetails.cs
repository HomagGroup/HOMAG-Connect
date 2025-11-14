using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Creation details
    /// </summary>
    public class CreationDetails
    {
        /// <summary>
        /// Attachments.
        /// </summary>
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Creation comment.
        /// </summary>
        public string? Comment { get; set; }
    }
}