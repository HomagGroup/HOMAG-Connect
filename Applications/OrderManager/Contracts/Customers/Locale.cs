using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// Locale
    /// </summary>
    /// <remarks>When deserializing from JSON, unknown values are mapped to <see cref="Unknown"/> due to the tolerant enum converter.</remarks>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Locale
    {
        /// <summary>
        /// Unknown locale (fallback if the locale is new or not known).
        /// Uses -1 to keep the numeric values and the default value <see cref="deDE"/> of the existing entries unchanged.
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// German (Germany)
        /// </summary>
        deDE,
        /// <summary>
        /// German (Austria)
        /// </summary>
        deAt,
        /// <summary>
        /// German (Switzerland)
        /// </summary>
        deCh,
        /// <summary>
        /// English (United Kingdom)
        /// </summary>
        enGB,
        /// <summary>
        /// English (United States)
        /// </summary>
        enUS,
        /// <summary>
        /// Spanish (Spain)
        /// </summary>
        esES,
        /// <summary>
        /// Dutch (Netherlands)
        /// </summary>
        nlNL,
        /// <summary>
        /// Dutch (Belgium)
        /// </summary>
        nlBE
    }
}
