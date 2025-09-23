using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting
{
    /// <summary>
    /// Gets or sets an event that occurs when a Cutting or nesting plan has been started or finished.
    /// </summary>
    [AppEvent("WorkstationFeedbackCuttingPlanStarted")]
    public class FeedbackWorkstationCuttingPlanStartedEvent : FeedbackWorkstationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override WorkstationType WorkstationType => WorkstationType.Cutting;

        /// <summary>
        /// Gets or sets the JobName the Dividing plan is part of
        /// </summary>
        [Required]
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the JobId the Dividing plan is part of
        /// </summary>
        [Required]
        public Guid JobId { get; set; }

        /// <summary>        
        /// Gets or sets the Pattern of the Cutting plan
        /// </summary>
        [Required]
        public string Pattern { get; set; }

        /// <summary>       
        /// Gets or sets the PatternIndex of the Cutting plan
        /// </summary>
        public int PatternIndex { get; set; }

        /// <summary>
        /// Gets or sets the Material of the Cutting plan 
        /// </summary>
        [Required]
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the BoardCode of the Cutting plan 
        /// </summary>
        [Required]
        public string BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the BoardType of the Cutting plan 
        /// </summary>
        [Required]
        public string BoardType { get; set; }

        /// <summary>       
        /// Gets or sets the number of Parts of the Cutting plan
        /// </summary>
        [Required]
        public int Parts { get; set; }

        /// <summary>
        /// Gets or sets the number of Offcuts of the Cutting plan
        /// </summary>
        [Required]
        public int Offcuts { get; set; }

        /// <summary>
        /// Gets or sets the PartArea of the Cutting plan 
        /// </summary>
        [Required]
        public double PartArea { get; set; }

        /// <summary>
        /// Gets or sets the OffcutArea of the Cutting plan 
        /// </summary>
        [Required]
        public double OffcutArea { get; set; }

    }
}
