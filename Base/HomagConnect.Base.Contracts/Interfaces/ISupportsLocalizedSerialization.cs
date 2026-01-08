namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Marker interface indicating a type supports localized JSON serialization.
/// </summary>
/// <remarks>
/// Implement this interface on DTOs that should be serialized with localized property display names
/// and/or enum display values when used together with a localized contract resolver and enum converter.
/// For example, use <c>LocalizedContractResolver</c> and <c>LocalizedEnumConverter</c> with a specific
/// <c>CultureInfo</c> to emit translated names.
/// </remarks>
/// <example>
/// var settings = new JsonSerializerSettings
/// {
///     ContractResolver = new LocalizedContractResolver(culture),
///     Converters = { new LocalizedEnumConverter(culture) }
/// };
/// var json = JsonConvert.SerializeObject(myDto, settings);
/// </example>
public interface ISupportsLocalizedSerialization;