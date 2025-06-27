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
        [Display(Description = "Single")]
        Single,
        [Display(Description = "Stack")]
        Stack,
        [Display(Description = "Goods in stock")]
        GoodsInStock
    }
}