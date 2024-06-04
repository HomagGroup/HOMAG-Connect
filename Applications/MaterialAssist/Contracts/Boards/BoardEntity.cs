using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class BoardEntity
    {
        /// <summary>
        /// Gets or sets the code (#) of a board.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the comments for the boards
        /// </summary>
        public string CommentsBoard { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public StorageLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the last used date.
        /// </summary>
        public DateTimeOffset? LastUsed { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public StorageLocation Workstation { get; set; }

        /// <summary>
        /// Gets or sets the board type properties.
        /// </summary>
        public BoardType BoardType { get; set; }
    }
}