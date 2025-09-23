using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation
{
    /// <summary>
    /// 
    /// </summary>
    [AppEvent("FeedbackWorkstation")]
    public abstract class FeedbackWorkstationEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the WorkstationId the Dividing plan is part of
        /// </summary>
        [Required]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the Workstation name
        /// </summary>
        [Required]
        public string? Workstation { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationType
        /// Default is configurable, but can be overridden in subclasses.
        /// </summary>
        [Required]
        public virtual WorkstationType WorkstationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual PartProcessedAction PartProcessedAction { get; set; }

        /// <summary>
        /// Gets or sets the Length of the Cutting plan 
        /// </summary>
        [Required]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the Width of the Cutting plan 
        /// </summary>
        [Required]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the DimZ of the Cutting plan 
        /// </summary>
        [Required]
        public double? Thickness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? OrderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Material { get; set; }
    }
}
