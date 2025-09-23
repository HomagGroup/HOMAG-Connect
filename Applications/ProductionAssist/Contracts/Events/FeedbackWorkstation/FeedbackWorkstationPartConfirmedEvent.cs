using HomagConnect.Base.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation
{
    /// <summary>
    /// 
    /// </summary>
    [AppEvent("WorkstationFeedback")]
    public class FeedbackWorkstationPartConfirmedEvent : FeedbackWorkstationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override PartProcessedAction PartProcessedAction => PartProcessedAction.PartConfirmed;

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

        #region Optional

        /// <summary>
        /// 
        /// </summary>
        public string Material { get; set; }
        #endregion
    }
}
