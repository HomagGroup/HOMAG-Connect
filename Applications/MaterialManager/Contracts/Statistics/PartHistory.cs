using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Boards;

using Resources = HomagConnect.Base.Contracts.Resources;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    public class PartHistory
    {
        public BoardEntity BoardEntity { get; set; }

        public BoardType BoardType { get; set; }

        public DateTimeOffset DividedAt { get; set; }

        public Guid JobId { get; set; }

        [Obsolete("Use OptimizationName instead")]
        public string JobName
        {
            get
            {
                return OptimizationName;
            }
            set
            {
                OptimizationName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(OptimizationName))]
        public string OptimizationName { get; set; }

        /// <summary>
        /// Gets or sets the part.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Part))]
        public PartHistoryPart Part { get; set; }

        public Guid WorkstationId { get; set; }

        public string WorkstationName { get; set; }
    }
}