using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.News.Client;

using Shouldly;

namespace HomagConnect.News.Tests;

[TestClass]
[DeploymentTest("Common.News", TestPriority.Low)]
public class NewsClientTests
{
    public TestContext TestContext { get; set; } = null!;

    private static readonly string[] _TagsIntelliDivide = ["intelliDivide"];
    private static readonly string[] _TagsServiceAssist = ["serviceAssist"];
    private static readonly string[] _TagsMultiple = ["intelliDivide", "serviceAssist"];

    #region Culture isolation

    [TestMethod]
    public async Task GetLatest_EnAndDe_BothCulturesReturnArticles()
    {
        var newsClient = CreateClient();

        var enArticles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsIntelliDivide, take: 10, cancellationToken: TestContext.CancellationToken);
        var deArticles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsIntelliDivide, take: 10, cancellationToken: TestContext.CancellationToken);

        enArticles.ShouldNotBeNull();
        deArticles.ShouldNotBeNull();

        enArticles.Count.ShouldBeGreaterThan(0, "because EN intelliDivide articles should exist");
        deArticles.Count.ShouldBeGreaterThan(0, "because DE intelliDivide articles should exist");

        // The feed serves the same article IDs across cultures (same content, localised per culture).
        enArticles.ShouldAllBe(a => HasTag(a.Tags, "intelliDivide"),
            "because every EN article must carry the requested tag");
        deArticles.ShouldAllBe(a => HasTag(a.Tags, "intelliDivide"),
            "because every DE article must carry the requested tag");
    }

    #endregion

    private static NewsClient CreateClient() => new();

    private static bool HasTag(IEnumerable<string> tags, string expectedTag)
    {
        return tags.Any(tag => string.Equals(tag, expectedTag, StringComparison.OrdinalIgnoreCase));
    }

    private static bool HasAnyTag(IEnumerable<string> tags, IEnumerable<string> expectedTags)
    {
        var expectedTagSet = new HashSet<string>(expectedTags, StringComparer.OrdinalIgnoreCase);
        return tags.Any(expectedTagSet.Contains);
    }

    #region EN

    [TestMethod]
    public async Task GetLatest_En_ReturnsArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), [], take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull("because the feed should return a result");
        articles.Count.ShouldBeGreaterThan(0, "because EN articles should exist");

        articles.Trace();
    }

    [TestMethod]
    public async Task GetLatest_En_WithIntelliDivideTag_ReturnsFilteredArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsIntelliDivide, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because EN intelliDivide articles should exist");
        articles.ShouldAllBe(a => HasTag(a.Tags, "intelliDivide"),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_En_WithServiceAssistTag_ReturnsFilteredArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsServiceAssist, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();

        // Only assert tag filtering when articles are returned; the EN feed may have no serviceAssist content.
        if (articles.Count > 0)
        {
            articles.ShouldAllBe(a => HasTag(a.Tags, "serviceAssist"),
                "because every returned article must carry the requested tag");
        }
    }

    [TestMethod]
    public async Task GetLatest_En_WithMultipleTags_ReturnsArticlesMatchingAnyTag()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsMultiple, take: 10, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because EN articles for at least one of the tags should exist");
        articles.ShouldAllBe(
            a => HasAnyTag(a.Tags, _TagsMultiple),
            "because every returned article must carry at least one of the requested tags");
    }

    [TestMethod]
    public async Task GetLatest_En_TakeLimitsArticleCount()
    {
        const int take = 3;
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsIntelliDivide, take: take, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeLessThanOrEqualTo(take, "because the number of returned EN articles must be less than or equal to take");
    }

    [TestMethod]
    public async Task GetLatest_En_ArticlesHaveRequiredFields()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsIntelliDivide, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        foreach (var article in articles)
        {
            article.Id.ShouldNotBeNullOrWhiteSpace("because every article must have an Id");
            article.Title.ShouldNotBeNullOrWhiteSpace("because every article must have a Title");
            article.Url.ShouldNotBeNull("because every article must have a Url");
            article.Tags.ShouldNotBeEmpty("because every article must have at least one tag");
            article.Date.ShouldNotBe(default, "because every article must have a Date");
        }
    }

    [TestMethod]
    public async Task GetLatest_WithTakeLessThanOne_ThrowsArgumentOutOfRangeException()
    {
        var newsClient = CreateClient();

        var exception = await Should.ThrowAsync<ArgumentOutOfRangeException>(
            async () => await newsClient.GetLatest(CultureInfo.GetCultureInfo("en"), _TagsIntelliDivide, take: 0, cancellationToken: TestContext.CancellationToken));

        exception.ParamName.ShouldBe("take");
    }

    #endregion

    #region DE

    [TestMethod]
    public async Task GetLatest_De_ReturnsArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), [], take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull("because the feed should return a result");
        articles.Count.ShouldBeGreaterThan(0, "because DE articles should exist");
    }

    [TestMethod]
    public async Task GetLatest_De_WithIntelliDivideTag_ReturnsFilteredArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsIntelliDivide, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE intelliDivide articles should exist");
        articles.ShouldAllBe(a => HasTag(a.Tags, "intelliDivide"),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_De_WithServiceAssistTag_ReturnsFilteredArticles()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsServiceAssist, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE serviceAssist articles should exist");
        articles.ShouldAllBe(a => HasTag(a.Tags, "serviceAssist"),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_De_WithMultipleTags_ReturnsArticlesMatchingAnyTag()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsMultiple, take: 10, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE articles for at least one of the tags should exist");
        articles.ShouldAllBe(
            a => HasAnyTag(a.Tags, _TagsMultiple),
            "because every returned article must carry at least one of the requested tags");
    }

    [TestMethod]
    public async Task GetLatest_De_TakeLimitsArticleCount()
    {
        const int take = 3;
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsIntelliDivide, take: take, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeLessThanOrEqualTo(take, "because the number of returned DE articles must be less than or equal to take");

        articles.Trace();
    }

    [TestMethod]
    public async Task GetLatest_De_ArticlesHaveRequiredFields()
    {
        var newsClient = CreateClient();

        var articles = await newsClient.GetLatest(CultureInfo.GetCultureInfo("de"), _TagsIntelliDivide, take: 5, cancellationToken: TestContext.CancellationToken);

        articles.ShouldNotBeNull();
        foreach (var article in articles)
        {
            article.Id.ShouldNotBeNullOrWhiteSpace("because every article must have an Id");
            article.Title.ShouldNotBeNullOrWhiteSpace("because every article must have a Title");
            article.Url.ShouldNotBeNull("because every article must have a Url");
            article.Tags.ShouldNotBeEmpty("because every article must have at least one tag");
            article.Date.ShouldNotBe(default, "because every article must have a Date");
        }
    }

    #endregion
}