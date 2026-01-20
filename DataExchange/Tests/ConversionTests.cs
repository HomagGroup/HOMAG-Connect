using System.IO.Compression;

using HomagConnect.Base.Extensions;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions;
using HomagConnect.DataExchange.Extensions.Wrapper;
using HomagConnect.DataExchange.Samples;

using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExplicitCallerInfoArgument

namespace HomagConnect.DataExchange.Tests;

/// <summary />
[TestClass]
[TestCategory("DataExchange")]
[TestCategory("DataExchange.Conversion")]
public class ConversionTests
{
    /// <summary />
    public required TestContext TestContext { get; set; }

    /// <summary />
    [TestMethod]
    public void Convert_ProjectToGroup_LargeProject()
    {
        var fileInfo = new FileInfo("TestData\\Kitchen.zip");

        ConvertProjectToGroup(fileInfo);
    }

    /// <summary />
    [TestMethod]
    public void Convert_ProjectToGroup_ProjectHavingTypicalProperties()
    {
        var fileInfo = DataExchangeSamples.GetProjectHavingTypicalProperties();

        ConvertProjectToGroup(fileInfo);
    }

    /// <summary />
    [TestMethod]
    public void Convert_ProjectToGroup_ProjectHavingOldPropertyTypes()
    {
        var quantityParam = new Param
        {
            Name = "Quantity",
            Value = "1.0000" // quantity type changed from double to int
        };

        var project = new Project();
        var order = new Order();
        project.Orders?.Add(order);

        var orderItemEntity = new Entity();
        orderItemEntity.Properties.Add(new Param { Name = "Type", Value = "OrderItem" });
        orderItemEntity.Properties.Add(quantityParam);
        order.Entities.Add(orderItemEntity);

        var componentEntity = new Entity();
        componentEntity.Properties.Add(new Param { Name = "Type", Value = "Component" });
        componentEntity.Properties.Add(quantityParam);
        orderItemEntity.Entities.Add(componentEntity);

        var boardEntity = new Entity();
        boardEntity.Properties.Add(new Param { Name = "Type", Value = "ProductionOrder" });
        boardEntity.Properties.Add(quantityParam);
        componentEntity.Entities.Add(boardEntity);

        var grps = project.ConvertToGroups();
        Assert.IsNotNull(grps);
        Assert.AreEqual(1, grps.Count());
    }

    /// <summary />
    [TestMethod]
    public void Convert_ProjectToOrder_LargeProject()
    {
        var fileInfo = new FileInfo("TestData\\Kitchen.zip");

        ConvertProjectToOrder(fileInfo);
    }

    /// <summary />
    [TestMethod]
    public void Convert_ProjectToOrder_ProjectHavingTypicalProperties()
    {
        var fileInfo = DataExchangeSamples.GetProjectHavingTypicalProperties();

        ConvertProjectToOrder(fileInfo);
    }

    private void ConvertProjectToGroup(FileInfo fileInfo)
    {
        using var tempDirectory = DisposableTempDirectory.Create();

        var zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        var (project, projectFiles) = ProjectPersistenceManager.Load(zipArchive, tempDirectory.DirectoryInfo);
        var projectWrapper = new ProjectWrapper(project);

        var groups = project.ConvertToGroups(projectFiles).ToList();

        Assert.IsNotNull(project.Orders);
        Assert.IsNotNull(groups);

        Assert.AreEqual(project.Orders.Count, groups.Count);

        for (var i = 0; i < projectWrapper.Orders.Count; i++)
        {
            var source = projectWrapper.Orders[i];
            var target = groups[i].group;
            var groupImages = groups[i].images;

            TestContext.AddResultFile(source.TraceToFile("Source").FullName);
            TestContext.AddResultFile(target.TraceToFile("Target").FullName);

            if (target.AdditionalProperties is { Count: > 0 })
            {
                target.AdditionalProperties.Trace("Unknown properties");
            }

            Assert.AreEqual(source.OrderName, target.Name);
            Assert.IsNotNull(groupImages);
        }
    }

    /// <summary />
    private void ConvertProjectToOrder(FileInfo fileInfo)
    {
        using var tempDirectory = DisposableTempDirectory.Create();
        var zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        var (project, projectFiles) = ProjectPersistenceManager.Load(zipArchive, tempDirectory.DirectoryInfo);
        var projectWrapper = new ProjectWrapper(project);

        var orders = project.ConvertToOrders().ToList();

        Assert.IsNotNull(project.Orders);
        Assert.IsNotNull(orders);

        Assert.AreEqual(project.Orders.Count, orders.Count);

        for (var i = 0; i < projectWrapper.Orders.Count; i++)
        {
            var source = projectWrapper.Orders[i];
            var target = orders[i];

            TestContext.AddResultFile(source.TraceToFile("Source").FullName);
            TestContext.AddResultFile(target.TraceToFile("Target").FullName);

            if (target.AdditionalProperties is { Count: > 0 })
            {
                target.AdditionalProperties.Trace("Unknown properties");
            }

            Assert.AreEqual(source.OrderName, target.OrderName);
            Assert.AreEqual(source.OrderNumber, target.OrderNumberExternal);
            Assert.IsNull(target.OrderNumber);
        }
    }
}