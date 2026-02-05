namespace HomagConnect.Base.Contracts.Extensions;

/// <summary>
/// String helper extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns the input string with its first letter capitalized.
    /// - Returns an empty string when input is null or whitespace.
    /// - Uses invariant casing for determinism across cultures.
    /// </summary>
    public static string CapitalizeFirstLetter(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input) || input == null)
        {
            return string.Empty;
        }

        var chars = input.ToCharArray();

        if (char.IsLower(chars[0]))
        {
            chars[0] = char.ToUpperInvariant(chars[0]);
        }

        return new string(chars);
    }
}