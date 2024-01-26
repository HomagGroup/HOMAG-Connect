using HomagConnect.IntelliDivide.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Base
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Grain
    {
        None = 0,
        Lengthwise = 1,
        Crosswise = 2
    }
}