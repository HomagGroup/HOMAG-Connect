namespace HomagConnect.Base.Contracts.Extensions
{
    /// <summary>
    /// String helper extensions.
    /// </summary>
    public static class StringExtensions
    {
        extension(string? input)
        {
            /// <summary>
            /// Returns the input string with its first letter capitalized.
            /// - Returns an empty string when input is null or whitespace.
            /// - Uses invariant casing for determinism across cultures.
            /// </summary>
            public string CapitalizeFirstLetter()
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return string.Empty;
                }

                var characters = input?.ToCharArray();

                if (characters != null && char.IsLower(characters[0]))
                {
                    characters[0] = char.ToUpperInvariant(characters[0]);
                }

                return new string(characters);
            }

            /// <summary>
            /// Trims the input and converts empty strings to null; otherwise returns the trimmed value.
            /// </summary>
            public string? NormalizeEmptyStringToNullOrTrimmed()
            {
                var trimmedValue = input.Trimmed();
                return string.IsNullOrEmpty(trimmedValue) ? null : trimmedValue;
            }

            /// <summary>
            /// Returns the trimmed input or an empty string if input is null.
            /// </summary>
            public string Trimmed()
            {
                return input?.Trim() ?? string.Empty;
            }
        }
    }
}