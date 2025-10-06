﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Events.Feedback
{
    /// <summary>
    /// Gets triggered when a ProductionItem has been completed on a Workstation.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Feedback) + "." + nameof(ProductionItemCompletedEvent))]
    public class ProductionItemCompletedEvent : WorkstationEvent
    {
        /// <summary>
        /// Gets or sets the Identifier of the ProductionItem that was completed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the Quantity of the ProductionItem that was completed.
        /// </summary>
        [Required]
        [DefaultValue(1)]
        [JsonProperty(Order = 21)]
        public int Quantity { get; set; } = 1;
    }
}