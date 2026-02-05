using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
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
        #region Board parameters     

        public string? BoardEntityId { get; set; }

        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.BoardEntityProperties_Comments))]
        public string? Comments { get; set; }

        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.BoardTypeProperties_BoardCode))]
        public string? BoardCode { get; set; }

        public BoardTypeType BoardTypeType { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the DividedAt timestamp
        /// </summary>
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.DividedAt))]
        public DateTimeOffset DividedAt { get; set; }

        /// <summary>
        /// Gets or sets the OptimizationId
        /// </summary>
        [Display(AutoGenerateField = false)]
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
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Part))]
        public PartHistoryPart Part { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationId on which the dividing occured
        /// </summary>
        [Display(AutoGenerateField = false)]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationName on which the dividing occured
        /// </summary>
        [Display( ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.AllocationProperties_Workstation))]
        public string WorkstationName { get; set; }
    }
}