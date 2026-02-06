using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Boards
{
    /// <summary>
    /// Event that occurs when a board type has been created or updated.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Boards) + "." + nameof(BoardTypeUpsertedEvent))]
    public class BoardTypeUpsertedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the BoardType that has been created or updated.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public BoardType BoardType { get; set; }
    }
}