using HomagConnect.Base.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation
{
    /// <summary>
    /// 
    /// </summary>
    [AppEvent("FeedbackWorkstationPartConfirmed")]
    public class FeedbackWorkstationPartConfirmedEvent : FeedbackWorkstationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override PartProcessedAction PartProcessedAction => PartProcessedAction.PartConfirmed;

        /// <summary>
        /// Gets or sets the Part Identification that was confirmed
        /// </summary>
        [Required]
        public string? Identification { get; set; }

        /// <summary>
        /// Gets or sets the account which truggered the processing
        /// </summary>
        public string GeneratedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal QuantityRemaining { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}
