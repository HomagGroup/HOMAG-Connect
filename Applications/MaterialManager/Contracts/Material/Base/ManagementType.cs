using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Entity management type.
    /// </summary>
    [ResourceManager(typeof(ManagementTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ManagementType
    {
        /// <summary>
        /// Single
        /// </summary>
        [Display(Description = "Single")]
        Single,
        /// <summary>
        /// Stack
        /// </summary>
        [Display(Description = "Stack")]
        Stack,
        /// <summary>
        /// GoodsInStock
        /// </summary>
        [Display(Description = "Goods in stock")]
        GoodsInStock
    }
}