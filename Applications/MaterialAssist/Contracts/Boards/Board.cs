using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class Board
    {
        public int Code { get; set; }

        public string? BoardCode { get; set; }

        public string? MaterialCode { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public BoardTypeType BoardType { get; set; }

        public ManagementType ManagementType { get; set; }

        public int Quantity { get; set; }

        public string? ArticleNumber { get; set; }

        public string? CommentsBoard { get; set; }

        public string? CommentsMasterData { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public Grain Grain { get; set; }

        public string? Location { get; set; }

        public DateTimeOffset? LastUsed { get; set; }

        public string? Workstation { get; set; }
    }
}