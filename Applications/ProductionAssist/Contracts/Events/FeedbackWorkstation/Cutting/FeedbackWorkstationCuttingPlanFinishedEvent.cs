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
    [AppEvent("WorkstationFeedbackCuttingPlanFinished")]
    public class FeedbackWorkstationCuttingPlanFinishedEvent : FeedbackWorkstationEvent
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
    }
}
