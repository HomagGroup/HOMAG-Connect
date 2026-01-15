using System.Globalization;

using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Extensions;

public static class ISupportsLocalizedSerializationExtensions
{
    /// <summary>   
    /// Serialize the object into JSON using localized property names and enum values
    /// for the specified culture. This ensures the output is user-friendly for reporting tools
    /// like Excel and Power BI, where localized column headers improve readability and usability.
    /// </summary>
    public static string SerializeLocalized(this ISupportsLocalizedSerialization supportLocalized, CultureInfo cultureInfo)
    {
        return JsonConvert.SerializeObject(supportLocalized, SerializerSettings.Localized(cultureInfo));
    }

    /// <summary>
    /// Serialize a sequence of objects into JSON using localized property names and enum values
    /// for the specified culture.
    /// </summary>
    public static string SerializeLocalized<T>(this IEnumerable<T> items, CultureInfo cultureInfo)
        where T : ISupportsLocalizedSerialization
    {
        return JsonConvert.SerializeObject(items, SerializerSettings.Localized(cultureInfo));
    }
}