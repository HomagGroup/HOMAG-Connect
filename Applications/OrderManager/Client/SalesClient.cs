using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Sales;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Client
{
    /// <inheritdoc cref="ISalesClient" />
    public class SalesClient : ClientBase, ISalesClient
    {
        private static readonly string _BaseRoute = "api/orderManager/sales";
        private static readonly string _ArticlesRoute = $"{_BaseRoute}/articles";
        private static readonly string _MasterDataRoute = $"{_BaseRoute}/masterData";

        /// <inheritdoc />
        public SalesClient(HttpClient client) : base(client) { }

        #region PosArticle CRUD

        /// <inheritdoc />
        public async Task<PosArticle> CreatePosArticle(PosArticle article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            var uri = new Uri(_ArticlesRoute, UriKind.Relative);

            using var response = await PostObject(uri, article);

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PosArticle>(content, SerializerSettings.Default);
            if (responseObject == null)
            {
                throw new InvalidOperationException("Response content could not be deserialized to PosArticle.");
            }

            return responseObject;
        }

        /// <inheritdoc />
        public async Task<PosArticle?> GetPosArticle(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var uri = $"{_ArticlesRoute}/{Uri.EscapeDataString(articleId)}";
            return await RequestObject<PosArticle>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<PosArticle>> GetPosArticles(int take, int skip = 0)
        {
            var uri = $"{_ArticlesRoute}?take={take}&skip={skip}";
            return await RequestEnumerable<PosArticle>(new Uri(uri, UriKind.Relative)) ?? Enumerable.Empty<PosArticle>();
        }

        /// <inheritdoc />
        public async Task<PosArticle> UpdatePosArticle(PosArticle article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (string.IsNullOrWhiteSpace(article.ArticleId))
            {
                throw new ArgumentException($"{nameof(PosArticle.ArticleId)} must be set.", nameof(article));
            }

            var uri = $"{_ArticlesRoute}/{Uri.EscapeDataString(article.ArticleId!)}";

            var json = JsonConvert.SerializeObject(article, SerializerSettings.Default);
            using var requestContent = new StringContent(json, Encoding.UTF8);
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            using var request = new HttpRequestMessage(HttpMethod.Put, new Uri(uri, UriKind.Relative))
            {
                Content = requestContent
            };

            using var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PosArticle>(content, SerializerSettings.Default);

            return responseObject ?? article;
        }

        /// <inheritdoc />
        public async Task DeletePosArticle(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            await DeletePosArticles([articleId]);
        }

        /// <inheritdoc />
        public async Task DeletePosArticles(IEnumerable<string> articleIds)
        {
            if (articleIds == null)
            {
                throw new ArgumentNullException(nameof(articleIds));
            }

            var distinctArticleIds = articleIds
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct()
                .OrderBy(id => id).ToList();

            if (!distinctArticleIds.Any())
            {
                throw new ArgumentNullException(nameof(articleIds), "At least one article id must be passed.");
            }

            var uris = distinctArticleIds
                .Select(id => $"&articleId={Uri.EscapeDataString(id)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_ArticlesRoute}" + parameter)
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri);
            }
        }

        #endregion

        #region MasterData

        /// <inheritdoc />
        public async Task<MasterData> GetMasterData(string libraryId)
        {
            if (string.IsNullOrWhiteSpace(libraryId))
            {
                throw new ArgumentNullException(nameof(libraryId));
            }

            var uri = $"{_MasterDataRoute}?libraryId={Uri.EscapeDataString(libraryId)}";
            var result = await RequestObject<MasterData>(new Uri(uri, UriKind.Relative));
            return result ?? new MasterData();
        }

        #endregion
    }
}
