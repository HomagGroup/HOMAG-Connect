using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Joins the values to strings with a maximum length
        /// </summary>
        public static IEnumerable<string> Join(this IEnumerable<string> strings, int maxLength)
        {
            var chunk = new StringBuilder();

            foreach (var value in strings.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                if (chunk.Length + value.Length <= maxLength)
                {
                    chunk.Append(value);
                }
                else
                {
                    yield return chunk.ToString();
                    chunk = new StringBuilder(value);
                }
            }

            if (chunk.Length > 0)
            {
                yield return chunk.ToString();
            }
        }
    }
}