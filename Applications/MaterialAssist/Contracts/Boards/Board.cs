using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class Board
    {
        /// <summary>
        /// Gets or sets the code (#) of a board.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        public string? BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the board type.
        /// </summary>
        public BoardTypeType BoardType { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the article number.
        /// </summary>
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the comments for the boards
        /// </summary>
        public string? CommentsBoard { get; set; }

        /// <summary>
        /// Gets or sets the comments for the master data
        /// </summary>
        public string? CommentsMasterData { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the Grain.
        /// </summary>
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the last used date.
        /// </summary>
        public DateTimeOffset? LastUsed { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public string? Workstation { get; set; }
    }
}