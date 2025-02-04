using System.Runtime.CompilerServices;
using System.Xml;

using HomagConnect.DataExchange.Contracts;

using Order = HomagConnect.ProductionManager.Contracts.Orders.Order;

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

        var project = new Project();
        var projectFiles = new Dictionary<string, string>();

        project.Properties.Add(new Param { Name = "Name", Value = "Project 01" });
        project.Properties.Add(new Param { Name = "Source", Value = "smartWOP" });

        var order = new Contracts.Order();

        order.Properties.Add(new Param { Name = nameof(Order.OrderNumber), Value = "101" });
        order.Properties.Add(new Param { Name = nameof(Order.OrderName), Value = "Sample order" });
        order.Properties.Add(new Param { Name = nameof(Order.OrderDescription), Value = "Sample order description" });

        order.Properties.Add(new Param { Name = nameof(Order.CustomerNumber), Value = "201" });
        order.Properties.Add(new Param { Name = nameof(Order.CustomerName), Value = "Sample customer name" });
        order.Properties.Add(new Param { Name = nameof(Order.Company), Value = "Sample company" });

        order.Properties.Add(new Param { Name = nameof(Order.OrderDate), Value = XmlConvert.ToString(DateTime.Today, XmlDateTimeSerializationMode.Utc) });
        order.Properties.Add(new Param { Name = nameof(Order.DeliveryDatePlanned), Value = XmlConvert.ToString(DateTime.Today.AddDays(10), XmlDateTimeSerializationMode.Utc) });

        {
            var orderImage = new Image();

            const string name = "Cabinet";
            const string fileName = name + ".png";
            const string reference = @"images\" + fileName;

            orderImage.Properties.Add(new Param { Name = "Category", Value = "Scene" });
            orderImage.Properties.Add(new Param { Name = "Description", Value = name });
            orderImage.Properties.Add(new Param { Name = "OriginalFileName", Value = fileName });
            orderImage.Properties.Add(new Param { Name = "ImageLinkPicture", Value = reference });

            projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName).FullName);

            order.Images.Add(orderImage);
        }

        {
            var orderImage = new Image();

            const string name = "Cabinet xray";
            const string fileName = name + ".jpg";
            const string reference = @"images\" + fileName;

            orderImage.Properties.Add(new Param { Name = "Category", Value = "AssemblyXRayImage" });
            orderImage.Properties.Add(new Param { Name = "Description", Value = name });
            orderImage.Properties.Add(new Param { Name = "OriginalFileName", Value = fileName });
            orderImage.Properties.Add(new Param { Name = "ImageLinkPicture", Value = reference });

            projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName).FullName);

            order.Images.Add(orderImage);
        }

        project.Orders?.Add(order);

        var zipFile = GetFileName("zip");

        project.Save(zipFile, projectFiles);

        TestContext.AddResultFile(zipFile.FullName);
    }

    private FileInfo GetFileName(string extension = ".zip", [CallerMemberName] string callerMemberName = "")
    {
        Assert.IsNotNull(TestContext);
        Assert.IsNotNull(TestContext.TestResultsDirectory);
        return new FileInfo(Path.Combine(TestContext.TestResultsDirectory, callerMemberName) + "." + extension.Trim('.'));
    }
#pragma warning restore S1199
}