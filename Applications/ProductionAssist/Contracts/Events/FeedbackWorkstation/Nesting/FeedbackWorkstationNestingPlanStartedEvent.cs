using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Nesting
{
    /// <summary>
    /// Gets or sets an event that occurs when a Nesting or nesting plan has been started or finished.
    /// </summary>
    [AppEvent("FeedbackWorkstationNestingPlanStarted")]
    public class FeedbackWorkstationNestingPlanStartedEvent : FeedbackWorkstationCuttingPlanStartedEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override WorkstationType WorkstationType => WorkstationType.Nesting;
    }
}
