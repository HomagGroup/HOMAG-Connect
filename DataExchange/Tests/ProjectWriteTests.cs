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

        project.Orders?.Add(order);

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