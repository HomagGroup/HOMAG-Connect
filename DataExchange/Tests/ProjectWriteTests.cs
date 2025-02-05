using System.Runtime.CompilerServices;

using HomagConnect.DataExchange.Extensions.Wrapper;

namespace HomagConnect.DataExchange.Tests;

/// <summary />
[TestClass]
[TestCategory("DataExchange")]
[TestCategory("DataExchange.Project.Write")]
public class ProjectWriteTests
{
#pragma warning disable S1199
    /// <summary />
    public TestContext? TestContext { get; set; }

    [TestMethod]
    public void Project_WriteZip_TypicalProperties()
    {
        Assert.IsNotNull(TestContext);

        var testDataFolder = new DirectoryInfo(@"TestData\\project-02");

        var project = new ProjectWrapper();
        var projectFiles = new Dictionary<string, string>();

        project.Name = "Project 01";
        project.Source = "smartWOP";

        var order = new OrderWrapper();

        order.OrderNumber = "101";
        order.OrderName = "Sample order";
        order.OrderDescription = "Sample order description";
        order.CustomerNumber = "201";
        order.CustomerName = "Max Mustermann";
        order.Company = "Mustermann GmbH";
        order.OrderDate = DateTime.Today;
        order.DeliveryDatePlanned = DateTime.Today.AddDays(10);

        project.Orders.Add(order);

        {
            var image = new ImageWrapper();

            const string name = "Cabinet";
            const string fileName = name + ".png";
            const string reference = @"images\" + fileName;

            image.Category = "Scene";
            image.Description = name;
            image.OriginalFileName = fileName;
            image.ImageLinkPicture = reference;

            projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName).FullName);

            order.Images.Add(image);
        }

        {
            var image = new ImageWrapper();

            const string name = "Cabinet xray";
            const string fileName = name + ".jpg";
            const string reference = @"images\" + fileName;

            image.Category = "AssemblyXRayImage";
            image.Description = name;
            image.OriginalFileName = fileName;
            image.ImageLinkPicture = reference;

            projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName).FullName);

            order.Images.Add(image);
        }

        var zipFile = GetFileName("zip");

        project.Save(zipFile, projectFiles);

        TestContext.AddResultFile(zipFile.FullName);
    }

    private FileInfo GetFileName(string extension = ".zip", [CallerMemberName] string callerMemberName = "")
    {
        Assert.IsNotNull(TestContext);
        Assert.IsNotNull(TestContext.TestResultsDirectory);
        return new FileInfo(Path.Combine("", callerMemberName) + "." + extension.Trim('.'));
    }
#pragma warning restore S1199
}