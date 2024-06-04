using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class BoardEntityDetails : BoardEntity
    {
        /// <summary>
        /// Gets or sets the images of the board.
        /// </summary>
        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
    }
}