using HomagConnect.IntelliDivide.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Provides fixed rotation angles
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum RotationAngle
    {
        /// <summary>
        /// 0 degrees
        /// </summary>
        Angle0,

        /// <summary>
        /// 90 degrees
        /// </summary>
        Angle90,

        /// <summary>
        /// Free
        /// </summary>
        Free,

        /// <summary>
        /// no rotation because grain is set
        /// </summary>
        Grain
    }
}