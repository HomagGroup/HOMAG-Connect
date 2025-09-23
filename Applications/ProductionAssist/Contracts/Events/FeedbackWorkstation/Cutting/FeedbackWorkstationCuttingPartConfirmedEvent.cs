using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace  HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting
{
    /// <summary>
    /// Gests or sets an event that occurs when a manual feedback is registered on one workplace for a ProductionItem
    /// </summary>
    [AppEvent("FeedbackWorkstationCuttingPartConfirmed")]
    public class FeedbackWorkstationCuttingPartConfirmedEvent : FeedbackWorkstationPartConfirmedEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override WorkstationType WorkstationType => WorkstationType.Cutting;

        /// <summary>
        /// 
        /// </summary>
        public override PartProcessedAction PartProcessedAction => PartProcessedAction.PartConfirmed;


    }
}
