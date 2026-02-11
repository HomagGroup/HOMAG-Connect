using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
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
    // Conversion factors
    private const double _MillimeterToInch = 0.03937008;
    private const double _SquareMeterToSquareFeet = 10.7639104;
    private const double _MeterToFeet = 3.2808399;

    [TestMethod]
    public async Task Optimizations_Cutting_GetFirstOptimizationAndEvaluate()
    {
        var solutions = await GetSampleSolutionDetails(OptimizationType.Cutting);
        EvaluateFirstOptimization(solutions);
    }

    [TestMethod]
    public async Task Optimizations_Cutting_SwitchUnitSystem()
    {
        var solutions = await GetSampleSolutionDetails(OptimizationType.Cutting);

        if (solutions.Count == 0)
        {
            Assert.Inconclusive("No solutions found for Cutting optimization");
            return;
        }

        var metric = solutions.First();
        var imperial = metric.SwitchUnitSystem(UnitSystem.Imperial, false);

        AssertUnconvertedValues(metric, imperial);
        AssertConvertedKeyFigures(metric, imperial);
        AssertConvertedPartDimensions(metric, imperial);
        AssertConvertedBoardDimensions(metric, imperial);

        TestContext?.AddResultFile(metric.TraceToFile(nameof(metric)).FullName);
        TestContext?.AddResultFile(imperial.TraceToFile(nameof(imperial)).FullName);
    }

    [TestMethod]
    public async Task Optimizations_Nesting_GetFirstOptimizationAndEvaluate()
    {
        var solutions = await GetSampleSolutionDetails(OptimizationType.Nesting);
        EvaluateFirstOptimization(solutions);
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

    private static void AssertConvertedBoardDimensions(SolutionDetails metric, SolutionDetails imperial)
    {
        var metricBoard = metric.Material.Boards.FirstOrDefault();
        if (metricBoard == null)
        {
            Assert.Inconclusive("No boards found in the solution");
            return;
        }

        var imperialBoard = imperial.Material.Boards.First();

        imperialBoard.Length.ShouldBeEquivalentTo(metricBoard.Length * _MillimeterToInch);
        imperialBoard.Width.ShouldBeEquivalentTo(metricBoard.Width * _MillimeterToInch);
    }

    private static void AssertConvertedKeyFigures(SolutionDetails metric, SolutionDetails imperial)
    {
        // mm -> inch
        imperial.Overview.Figures.Production.AverageBookHeight
            .ShouldBeEquivalentTo(metric.Overview.Figures.Production.AverageBookHeight * _MillimeterToInch);

        // m² -> ft²
        imperial.KeyFigures.Production.Output.PartArea
            .ShouldBeEquivalentTo(metric.KeyFigures.Production.Output.PartArea * _SquareMeterToSquareFeet);

        // m -> ft
        imperial.KeyFigures.Production.Output.CuttingLength
            .ShouldBeEquivalentTo(metric.KeyFigures.Production.Output.CuttingLength * _MeterToFeet);
    }

    private static void AssertConvertedPartDimensions(SolutionDetails metric, SolutionDetails imperial)
    {
        var metricPart = metric.Parts.FirstOrDefault();
        if (metricPart == null)
        {
            Assert.Inconclusive("No parts found in the solution");
            return;
        }

        var imperialPart = imperial.Parts.First();

        imperialPart.Length.ShouldBeEquivalentTo(metricPart.Length * _MillimeterToInch);
        imperialPart.Width.ShouldBeEquivalentTo(metricPart.Width * _MillimeterToInch);
        imperialPart.UnitSystem.ShouldBe(UnitSystem.Imperial);
    }

    private static void AssertUnconvertedValues(SolutionDetails metric, SolutionDetails imperial)
    {
        imperial.Overview.Figures.Production.ProductionTime
            .ShouldBeEquivalentTo(metric.Overview.Figures.Production.ProductionTime);

        imperial.Overview.Figures.Production.Cycles
            .ShouldBeEquivalentTo(metric.Overview.Figures.Production.Cycles);
    }

    private void EvaluateFirstOptimization(List<SolutionDetails> solutionDetails)
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