using System.Globalization;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Extensions;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Optimizations;

[TestClass]
[IntegrationTest("IntelliDivide.Optimizations.Evaluation")]
public class OptimizationsCandidateEvaluationTests : IntelliDivideTestBase
{
    [TestMethod]
    public async Task Optimizations_Cutting_GetFirstOptimizationAndEvaluate()
    {
        var solutions = await GetSampleSolutionDetails(OptimizationType.Cutting);
        GetFirstOptimizationAndEvaluate(solutions);
    }

    [TestMethod]
    public async Task Optimizations_Nesting_GetFirstOptimizationAndEvaluate()
    {
        var solutions = await GetSampleSolutionDetails(OptimizationType.Nesting);
        GetFirstOptimizationAndEvaluate(solutions);
    }

    [TestMethod]
    public async Task SolutionCharacteristics_EnsureAllIconsExists()
    {
        var icons = EnumExtensions.GetDisplayIconUris<SolutionCharacteristic>();
        var downloadRoot = Path.Combine(Path.GetTempPath(), "IntelliDivideIconCache");

        Directory.CreateDirectory(downloadRoot);

        using var httpClient = new HttpClient();

        foreach (var icon in icons)
        {
            var characteristic = icon.Key;
            var iconUri = icon.Value;

            if (iconUri == null)
            {
                Assert.Inconclusive($"No icon assigned for characteristic: {characteristic}");
                continue;
            }

            var uriText = iconUri.ToString()!;
            var uri = new Uri(uriText, UriKind.RelativeOrAbsolute);

            if (uri.IsFile)
            {
                var filePath = uri.LocalPath;
                File.Exists(filePath).ShouldBeTrue($"Icon file does not exist: {filePath}");
                new FileInfo(filePath).Length.ShouldBeGreaterThan(0, $"Icon file is empty: {filePath}");
            }
            else
            {
                // Create a stable local filename per characteristic
                var baseName = Path.GetFileName(uri.AbsolutePath);
                var safeName = string.IsNullOrWhiteSpace(baseName) ? $"{characteristic}.png" : $"{characteristic}_{baseName}";
                var localPath = Path.Combine(downloadRoot, safeName);

                try
                {
                    if (!File.Exists(localPath))
                    {
                        using var response = await httpClient.GetAsync(uri);
                        response.EnsureSuccessStatusCode();
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        await File.WriteAllBytesAsync(localPath, bytes);
                    }

                    File.Exists(localPath).ShouldBeTrue($"Downloaded icon file does not exist: {localPath}");
                    new FileInfo(localPath).Length.ShouldBeGreaterThan(0, $"Downloaded icon file is empty: {localPath}");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Failed to download icon '{uriText}' for {characteristic}: {ex.Message}");
                }
            }
        }
    }

    private void GetFirstOptimizationAndEvaluate(List<SolutionDetails> solutionDetails)
    {
        solutionDetails.ShouldNotBeNull();
        solutionDetails.ShouldNotBeEmpty();

        var cultureInfo = new CultureInfo("de-DE");

        var evaluationResults = solutionDetails.DetermineCharacteristicsAndDisplayOrder();

        evaluationResults.ShouldNotBeEmpty();
        evaluationResults.Length.ShouldBe(solutionDetails.Count);

        // Project localized name and description for reporting
        evaluationResults
            .Select(r => new
            {
                r.Id,
                r.Characteristic,
                DisplayName = r.Characteristic.GetLocalizedName(cultureInfo),
                Description = r.Characteristic.GetLocalizedDescription(r.CharacteristicsInAddition, cultureInfo),
                Icon = r.Characteristic.GetDisplayIconUri()
            })
            .Trace("LocalizedCharacteristics");

        evaluationResults.Trace(nameof(evaluationResults));

        var resultFile = evaluationResults.TraceToFile(nameof(evaluationResults));

        if (resultFile != null)
        {
            TestContext?.AddResultFile(resultFile.FullName);
        }
    }

    private async Task<List<SolutionDetails>> GetSampleSolutionDetails(OptimizationType optimizationType)
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        var optimization = await intelliDivide
            .GetOptimizations(optimizationType, OptimizationStatus.Optimized, 1)!
            .FirstOrDefaultAsync();

        if (optimization == null)
        {
            Assert.Inconclusive("No optimization found");
        }

        optimization.Link.Trace(nameof(optimization.Link));

        var solutionDetails = new List<SolutionDetails>();
        var solutions = await intelliDivide.GetSolutions(optimization.Id);

        Assert.IsNotNull(solutions);

        foreach (var solution in solutions)
        {
            var details = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);
            Assert.IsNotNull(details);
            solutionDetails.Add(details);
        }

        return solutionDetails;
    }
}