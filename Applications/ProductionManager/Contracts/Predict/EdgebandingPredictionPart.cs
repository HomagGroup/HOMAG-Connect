using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Part for edgebanding prediction.
    /// </summary>
    public class EdgebandingPredictionPart
    {
        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
        /// </summary>
        [JsonProperty(Order = 32)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeBack { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
        /// </summary>
        [JsonProperty(Order = 31)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeFront { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
        /// </summary>
        [JsonProperty(Order = 33)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeLeft { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
        /// </summary>
        [JsonProperty(Order = 34)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeRight { get; set; }

        /// <summary>
        /// Gets or sets the id of the part.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the length of the part.
        /// </summary>
        [JsonProperty(Order = 20)]
        [Range(0.1, 9999.9)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the quantity how often the part is needed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        [Range(1, 10000)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the thickness of the part.
        /// </summary>
        [JsonProperty(Order = 23)]
        [Range(0.1, 500)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the width of the part.
        /// </summary>
        [JsonProperty(Order = 21)]
        [Range(0.1, 9999.9)]
        public double? Width { get; set; }
    }
}