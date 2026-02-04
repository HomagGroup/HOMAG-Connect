using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using System;
using System.ComponentModel.DataAnnotations;
using Resources = HomagConnect.Base.Contracts.Resources;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    /// <summary>
    /// Returns the history of parts divided
    /// </summary>
    public class PartHistory : ISupportsLocalizedSerialization
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

        /// <summary>
        /// Gets or sets the BoardEntity information
        /// </summary>
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.BoardEntity))]
        public BoardEntity BoardEntity { get; set; }

        /// <summary>
        /// Gets or sets the BoardType information
        /// </summary>
        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.BoardEntityProperties_BoardType))]
        public BoardType BoardType { get; set; }

        /// <summary>
        /// Gets or sets the DividedAt timestamp
        /// </summary>
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.DividedAt))]
        public DateTimeOffset DividedAt { get; set; }

        /// <summary>
        /// Gets or sets the OptimizationId
        /// </summary>
        [Display(AutoGenerateField = false, ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OptimizationId))]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Obsolete value for OptimizationId
        /// </summary>
        [Obsolete("Use OptimizationId instead")]
        public Guid JobId
        {
            get
            {
                return OptimizationId;
            }
            set
            {
                OptimizationId = value;
            }
        }


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

        /// <summary>
        /// Gets or sets the WorkstationId on which the dividing occured
        /// </summary>
        [Display(AutoGenerateField = false, ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.WorkstationId))]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationName on which the dividing occured
        /// </summary>
        [Display( ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.AllocationProperties_Workstation))]
        public string WorkstationName { get; set; }
    }
}