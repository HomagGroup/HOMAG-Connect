using HomagConnect.IntelliDivide.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OptimizationType
    {
        Cutting,
        Nesting
    }
}