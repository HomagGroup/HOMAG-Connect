using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Grain
    {
        None = 0,
        Lengthwise = 1,
        Crosswise = 2
    }
}