using System.Text;

namespace HomagConnect.MaterialManager.Client
{
    internal static class CatalogHelpers
    {
        public static Uri BuildCatalogQuery(string baseRoute, int take, int skip, string[]? manufacturers,
            string? decorCode, string? embossingTop, string? embossingBottom)
        {
            var query = new StringBuilder($"{baseRoute}/catalog?take={take}&skip={skip}");

            if (manufacturers != null)
            {
                foreach (var manufacturer in manufacturers.Where(m => !string.IsNullOrWhiteSpace(m)))
                {
                    query.Append($"&manufacturers={Uri.EscapeDataString(manufacturer)}");
                }
            }

            if (!string.IsNullOrWhiteSpace(decorCode))
            {
                query.Append($"&decorCode={Uri.EscapeDataString(decorCode)}");
            }

            if (!string.IsNullOrWhiteSpace(embossingTop))
            {
                query.Append($"&embossingTop={Uri.EscapeDataString(embossingTop)}");
            }

            if (!string.IsNullOrWhiteSpace(embossingBottom))
            {
                query.Append($"&embossingBottom={Uri.EscapeDataString(embossingBottom)}");
            }

            return new Uri(query.ToString(), UriKind.Relative);
        }
    }
}
