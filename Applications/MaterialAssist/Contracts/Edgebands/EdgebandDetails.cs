using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Edgebands
{
    public class EdgebandDetails : Edgeband
    {
        /// <summary>
        /// Gets or sets the images of the board.
        /// </summary>
        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
    }
}
