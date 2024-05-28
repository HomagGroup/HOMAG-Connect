using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class BoardDetails : Board
    {
        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
    }
}