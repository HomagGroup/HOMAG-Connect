using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a root module of an article, including its position and rotation.
    /// </summary>
    public class PosModuleRootData : PosModuleData
    {
        /// <summary>
        /// Position (x, y, z) of the article. Optional. Defaults to 0, 0, 0.
        /// </summary>
        /// <example>[0, 0, 0]</example>
        [JsonProperty("articlePos")]
        public IList<double>? ArticlePos { get; set; }

        /// <summary>
        /// Rotation of the root module around the Y axis. Optional. Defaults to 0.
        /// </summary>
        /// <example>90</example>
        [JsonProperty("rotationY")]
        public double? RotationY { get; set; }
    }
}
