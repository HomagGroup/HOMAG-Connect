using HomagConnect.OrderManager.Contracts.Customers;
using System.Globalization;

namespace HomagConnect.OrderManager.Contracts.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Locale" />.
    /// </summary>
    public static class LocaleExtensions
    {
        /// <summary>
        /// Returns the <see cref="CultureInfo" /> matching the locale, e.g. "de-AT" for <see cref="Locale.deAt" />.
        /// Returns <see cref="CultureInfo.InvariantCulture" /> for <see cref="Locale.Unknown" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value is not a defined <see cref="Locale" />.</exception>
        public static CultureInfo ToCultureInfo(this Locale locale)
        {
            if (!Enum.IsDefined(typeof(Locale), locale))
            {
                throw new ArgumentOutOfRangeException(nameof(locale), locale, "Unknown locale.");
            }

            if (locale == Locale.Unknown)
            {
                return CultureInfo.InvariantCulture;
            }

            return ToCultureInfo(locale.ToString());
        }

        private static CultureInfo ToCultureInfo(string locale)
        {
            return CultureInfo.GetCultureInfo(ToCultureName(locale));
        }

        /// <summary>
        /// Converts a locale enum name into a BCP-47 culture name, e.g. "deAt" to "de-AT", "zhHans" to "zh-Hans",
        /// "zhHansCN" to "zh-Hans-CN" or "es419" to "es-419".
        /// </summary>
        internal static string ToCultureName(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale))
            {
                throw new ArgumentException("Locale must not be empty.", nameof(locale));
            }

            var languageLength = 0;
            while (languageLength < locale.Length && char.IsLower(locale[languageLength]))
            {
                languageLength++;
            }

            if (languageLength < 2 || languageLength > 3)
            {
                throw new ArgumentException($"Locale '{locale}' must start with a two- or three-letter language code.", nameof(locale));
            }

            var subtags = new List<string> { locale.Substring(0, languageLength) };
            var index = languageLength;

            while (index < locale.Length)
            {
                var start = index;
                var current = locale[index];

                if (char.IsDigit(current))
                {
                    while (index < locale.Length && char.IsDigit(locale[index]))
                    {
                        index++;
                    }
                }
                else if (char.IsUpper(current))
                {
                    index++;

                    if (index < locale.Length && char.IsLower(locale[index]))
                    {
                        // Title case subtag, e.g. "Hans" or "At"
                        while (index < locale.Length && char.IsLower(locale[index]))
                        {
                            index++;
                        }
                    }
                    else
                    {
                        // Upper case subtag, e.g. "CN"
                        while (index < locale.Length && char.IsUpper(locale[index]))
                        {
                            index++;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Locale '{locale}' contains the invalid character '{current}'.", nameof(locale));
                }

                subtags.Add(NormalizeSubtag(locale, locale.Substring(start, index - start)));
            }

            if (subtags.Count < 2)
            {
                throw new ArgumentException($"Locale '{locale}' must contain a region or script code.", nameof(locale));
            }

            return string.Join("-", subtags);
        }

        private static string NormalizeSubtag(string locale, string subtag)
        {
            if (subtag.All(char.IsDigit))
            {
                if (subtag.Length != 3)
                {
                    throw new ArgumentException($"Locale '{locale}' contains the invalid numeric region code '{subtag}'.", nameof(locale));
                }

                return subtag;
            }

            switch (subtag.Length)
            {
                case 2:
                    return subtag.ToUpperInvariant();
                case 4:
                    return string.Concat(subtag.Substring(0, 1).ToUpperInvariant(), subtag.Substring(1).ToLowerInvariant());
                default:
                    throw new ArgumentException($"Locale '{locale}' contains the unsupported subtag '{subtag}'.", nameof(locale));
            }
        }
    }
}
