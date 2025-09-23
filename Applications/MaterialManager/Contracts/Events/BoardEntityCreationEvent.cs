using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using System.ComponentModel.DataAnnotations;

namespace  HomagConnect.MaterialManager.Contracts.Events
{
    /// <summary>
    /// Gests or sets an event that occurs when a BoardEntity is created
    /// </summary>
    [AppEvent("BoardEntityCreation")]
    public class BoardEntityCreationEvent : AppEvent
    {
        /// <summary>
        /// The BoardEntity object
        /// </summary>
        public BoardEntity BoardEntity { get; set; }
    }
}
