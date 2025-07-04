using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// The type of the board type.
    /// </summary>
    [ResourceManager(typeof(AllocationTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum AllocationType
    {
        Automatic,
        Manual
    }
}
