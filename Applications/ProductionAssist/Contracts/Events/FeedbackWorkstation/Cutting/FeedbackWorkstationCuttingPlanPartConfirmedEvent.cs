using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace  HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting
{
    /// <summary>
    /// Gests or sets an event that occurs when a manual feedback is registered on one workplace for a ProductionItem
    /// </summary>
    [AppEvent("FeedbackWorkstationCuttingPlanPartConfirmed")]
    public class FeedbackWorkstationCuttingPlanPartConfirmedEvent : FeedbackWorkstationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override WorkstationType WorkstationType => WorkstationType.Cutting;

        /// <summary>
        /// Gets or sets the JobName 
        /// </summary>
        [Required]
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the JobId 
        /// </summary>
        [Required]
        public Guid JobId { get; set; }

        /// <summary>        
        /// Gets or sets the PatternName 
        /// </summary>
        [Required]
        public string PatternName { get; set; }

        /// <summary>       
        /// Gets or sets the PatternCycle 
        /// </summary>
        public int PatternCycle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Part Part { get; set; }
    }
}
