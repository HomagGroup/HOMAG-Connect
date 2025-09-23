using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace  HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Nesting
{
    /// <summary>
    /// Gests or sets an event that occurs when a manual feedback is registered on one workplace for a ProductionItem
    /// </summary>
    [AppEvent("FeedbackWorkstationNestingPlanPartConfirmed")]
    public class FeedbackWorkstationNestingPlanPartConfirmedEvent : FeedbackWorkstationCuttingPlanPartConfirmedEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override WorkstationType WorkstationType => WorkstationType.Nesting;
    }
}
