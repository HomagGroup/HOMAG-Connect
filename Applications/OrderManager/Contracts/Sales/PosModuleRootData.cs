using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a root module of an article, including its position and rotation.
    /// </summary>
    public class PosModuleRootData : PosModuleData
    {
        /// <summary>
        /// optional a position of this article; if not set it is always 0,0,0 (x/y/z)
        /// </summary>
        [JsonProperty("articlePos")]
        public IList<double>? ArticlePos { get; set; }

        /// <summary>
        /// The Y-Rotation of this root module; if it is not set, it is always 0
        /// </summary>
        [JsonProperty("rotationY")]
        public double? RotationY { get; set; }
    }
}
