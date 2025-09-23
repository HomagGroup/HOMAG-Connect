using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using System.ComponentModel.DataAnnotations;

namespace  HomagConnect.MaterialManager.Contracts.Events
{
    /// <summary>
    /// Gests or sets an event that occurs when a Board is confirmed to be used in productionAssist
    /// </summary>
    [AppEvent("BoardTypeCreation")]
    public class BoardTypeCreationEvent : AppEvent
    {
        /// <summary>
        /// The BoardType object
        /// </summary>
        public BoardType BoardType { get; set; }
    }
}
