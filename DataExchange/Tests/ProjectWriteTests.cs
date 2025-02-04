using System.Runtime.CompilerServices;
using System.Xml;

using HomagConnect.Base.Extensions;
using HomagConnect.DataExchange.Contracts;

using Order = HomagConnect.ProductionManager.Contracts.Orders.Order;

namespace HomagConnect.DataExchange.Tests;

/// <summary />
[TestClass]
[TestCategory("DataExchange")]
[TestCategory("DataExchange.Project.Write")]
public class ProjectWriteTests
{
    /// <summary />
    public TestContext? TestContext { get; set; }

    [TestMethod]
    public void Project_WriteZip_TypicalProperties()
    {
        Assert.IsNotNull(TestContext);

        var project = new Project();

        project.Properties.Add(new Param { Name = nameof(Order.OrderNumber), Value = "101" });
        project.Properties.Add(new Param { Name = nameof(Order.OrderName), Value = "Sample order" });
        project.Properties.Add(new Param { Name = nameof(Order.OrderDescription), Value = "Sample order description" });

        project.Properties.Add(new Param { Name = nameof(Order.CustomerNumber), Value = "201" });
        project.Properties.Add(new Param { Name = nameof(Order.CustomerName), Value = "Sample customer name" });
        project.Properties.Add(new Param { Name = nameof(Order.Company), Value = "Sample company" });

        project.Properties.Add(new Param { Name = nameof(Order.OrderDate), Value = XmlConvert.ToString(DateTime.Today, XmlDateTimeSerializationMode.Utc) });
        project.Properties.Add(new Param { Name = nameof(Order.DeliveryDatePlanned), Value = XmlConvert.ToString(DateTime.Today.AddDays(10), XmlDateTimeSerializationMode.Utc) });

        project.Trace();

        var zipFile = GetFileName("xml");

        project.Save(zipFile);

        TestContext.AddResultFile(zipFile.FullName);
    }

    private FileInfo GetFileName(string extension = ".zip", [CallerMemberName] string callerMemberName = "")
    {
        Assert.IsNotNull(TestContext);
        Assert.IsNotNull(TestContext.DeploymentDirectory);

        return new FileInfo(Path.Combine(TestContext.DeploymentDirectory, callerMemberName) + "." + extension.Trim('.'));
    }
}