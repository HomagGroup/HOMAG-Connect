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
    [AppEvent("FeedbackWorkstationCuttingPlanStarted")]
    public class FeedbackWorkstationCuttingPlanStartedEvent : FeedbackWorkstationEvent
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
        /// Gets or sets the PatternName of the Cutting plan
        /// </summary>
        [Required]
        public string PatternName { get; set; }

        /// <summary>       
        /// Gets or sets the PatternCycle of the Cutting plan
        /// </summary>
        public int PatternCycle { get; set; }

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
