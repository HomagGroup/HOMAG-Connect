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
    private static readonly string[] TagsIntelliDivide = ["intelliDivide"];
    private static readonly string[] TagsServiceAssist = ["serviceAssist"];
    private static readonly string[] TagsMultiple = ["intelliDivide", "serviceAssist"];

    #region Culture isolation

    [TestMethod]
    public async Task GetLatest_EnAndDe_BothCulturesReturnArticles()
    {
        var client = CreateClient();

        var enArticles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsIntelliDivide, take: 10);
        var deArticles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsIntelliDivide, take: 10);

        enArticles.ShouldNotBeNull();
        deArticles.ShouldNotBeNull();

        enArticles.Count.ShouldBeGreaterThan(0, "because EN intelliDivide articles should exist");
        deArticles.Count.ShouldBeGreaterThan(0, "because DE intelliDivide articles should exist");

        // The feed serves the same article IDs across cultures (same content, localised per culture).
        enArticles.ShouldAllBe(a => a.Tags.Contains("intelliDivide", StringComparer.OrdinalIgnoreCase),
            "because every EN article must carry the requested tag");
        deArticles.ShouldAllBe(a => a.Tags.Contains("intelliDivide", StringComparer.OrdinalIgnoreCase),
            "because every DE article must carry the requested tag");
    }

    #endregion

    private static NewsClient CreateClient() => new();

    #region EN

    [TestMethod]
    public async Task GetLatest_En_ReturnsArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), [], take: 5);

        articles.ShouldNotBeNull("because the feed should return a result");
        articles.Count.ShouldBeGreaterThan(0, "because EN articles should exist");

        articles.Trace();
    }

    [TestMethod]
    public async Task GetLatest_En_WithIntelliDivideTag_ReturnsFilteredArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsIntelliDivide, take: 5);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because EN intelliDivide articles should exist");
        articles.ShouldAllBe(a => a.Tags.Contains("intelliDivide", StringComparer.OrdinalIgnoreCase),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_En_WithServiceAssistTag_ReturnsFilteredArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsServiceAssist, take: 5);

        articles.ShouldNotBeNull();

        // Only assert tag filtering when articles are returned; the EN feed may have no serviceAssist content.
        if (articles.Count > 0)
        {
            articles.ShouldAllBe(a => a.Tags.Contains("serviceAssist", StringComparer.OrdinalIgnoreCase),
                "because every returned article must carry the requested tag");
        }
    }

    [TestMethod]
    public async Task GetLatest_En_WithMultipleTags_ReturnsArticlesMatchingAnyTag()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsMultiple, take: 10);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because EN articles for at least one of the tags should exist");
        articles.ShouldAllBe(
            a => a.Tags.Any(t => TagsMultiple.Contains(t, StringComparer.OrdinalIgnoreCase)),
            "because every returned article must carry at least one of the requested tags");
    }

    [TestMethod]
    public async Task GetLatest_En_TakeReturnsArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsIntelliDivide, take: 3);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because EN intelliDivide articles should exist");
    }

    [TestMethod]
    public async Task GetLatest_En_ArticlesHaveRequiredFields()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("en"), TagsIntelliDivide, take: 5);

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

    #region DE

    [TestMethod]
    public async Task GetLatest_De_ReturnsArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), [], take: 5);

        articles.ShouldNotBeNull("because the feed should return a result");
        articles.Count.ShouldBeGreaterThan(0, "because DE articles should exist");
    }

    [TestMethod]
    public async Task GetLatest_De_WithIntelliDivideTag_ReturnsFilteredArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsIntelliDivide, take: 5);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE intelliDivide articles should exist");
        articles.ShouldAllBe(a => a.Tags.Contains("intelliDivide", StringComparer.OrdinalIgnoreCase),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_De_WithServiceAssistTag_ReturnsFilteredArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsServiceAssist, take: 5);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE serviceAssist articles should exist");
        articles.ShouldAllBe(a => a.Tags.Contains("serviceAssist", StringComparer.OrdinalIgnoreCase),
            "because every returned article must carry the requested tag");
    }

    [TestMethod]
    public async Task GetLatest_De_WithMultipleTags_ReturnsArticlesMatchingAnyTag()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsMultiple, take: 10);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE articles for at least one of the tags should exist");
        articles.ShouldAllBe(
            a => a.Tags.Any(t => TagsMultiple.Contains(t, StringComparer.OrdinalIgnoreCase)),
            "because every returned article must carry at least one of the requested tags");
    }

    [TestMethod]
    public async Task GetLatest_De_TakeReturnsArticles()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsIntelliDivide, take: 3);

        articles.ShouldNotBeNull();
        articles.Count.ShouldBeGreaterThan(0, "because DE intelliDivide articles should exist");

        articles.Trace();
    }

    [TestMethod]
    public async Task GetLatest_De_ArticlesHaveRequiredFields()
    {
        var client = CreateClient();

        var articles = await client.GetLatest(CultureInfo.GetCultureInfo("de"), TagsIntelliDivide, take: 5);

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