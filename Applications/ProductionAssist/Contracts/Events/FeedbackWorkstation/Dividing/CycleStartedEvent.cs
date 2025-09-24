using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation.Cutting
{
#pragma warning disable CS8618
    /// <summary>
    /// Gets or sets an event that occurs when a Cutting or nesting plan has been started or finished.
    /// </summary>
    [AppEvent("CycleStarted")]
    public class CycleStartedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the WorkstationId
        /// </summary>
        [Required]
        public Guid WorkstationId { get; set; }


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
        /// Gets or sets the BoardEntityId used in this cycle
        /// </summary>
        public string? BoardEntityId { get; set; }
    }
#pragma warning restore  CS8618
}
