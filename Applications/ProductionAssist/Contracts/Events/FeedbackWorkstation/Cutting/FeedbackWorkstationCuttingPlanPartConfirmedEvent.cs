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
        /// The OptimizationId
        /// </summary>
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// The OptimizationName 
        /// </summary>
        public string OptimizationName { get; set; }

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
