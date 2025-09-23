using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation
{
    /// <summary>
    /// Gets or sets an event that occurs when a part has been confirmed
    /// </summary>
    [AppEvent("FeedbackWorkstationPartConfirmed")]
    public class FeedbackWorkstationPartConfirmedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the WorkstationId
        /// </summary>
        [Required]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the Part Identification that was confirmed
        /// </summary>
        [Required]
        public string Identification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
