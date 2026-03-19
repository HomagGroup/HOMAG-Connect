using System.IO.Compression;

using HomagConnect.Base.Extensions;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions;
using HomagConnect.DataExchange.Extensions.Wrapper;
using HomagConnect.DataExchange.Samples;
using HomagConnect.OrderManager.Contracts.OrderItems;

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
        var fileInfo = DataExchangeSamples.GetProjectHavingTypicalProperties(true, true);

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

    [TestMethod]
    public void Convert_ProjectToGroup_ProjectHavingUnknwonPropertyTypes()
    {
        var newProprtyParam = new Param
        {
            Name = "NewProperty",
            Value = "anyValue"
        };

        var project = new Project();
        var order = new Order();
        project.Orders?.Add(order);

        var orderItemEntity = new Entity();
        orderItemEntity.Properties.Add(new Param { Name = "Type", Value = "OrderItem" });
        order.Entities.Add(orderItemEntity);

        var componentEntity = new Entity();
        componentEntity.Properties.Add(new Param { Name = "Type", Value = "Component" });
        componentEntity.Properties.Add(newProprtyParam);
        orderItemEntity.Entities.Add(componentEntity);

        var boardEntity = new Entity();
        boardEntity.Properties.Add(new Param { Name = "Type", Value = "ProductionOrder" });
        boardEntity.Properties.Add(newProprtyParam);
        componentEntity.Entities.Add(boardEntity);

        // Act
        var grps = project.ConvertToGroups();

        // Assert
        Assert.IsNotNull(grps);
        Assert.AreEqual(1, grps.Count());
        var grp = grps.First();
        Assert.IsNotNull(grp.Items);
        Assert.AreEqual(1, grp.Items.Count());

        var pos = grp.Items.FirstOrDefault(p => p is Position);
        Assert.IsNotNull(pos);
        Assert.IsNotNull(pos.Items);
        Assert.AreEqual(1, pos.Items.Count());

        var comp = pos.Items.FirstOrDefault(p => p is Component);
        Assert.IsNotNull(comp);
        Assert.IsNotNull(comp.AdditionalProperties);
        Assert.IsNotNull(comp.AdditionalProperties.FirstOrDefault(p => p.Key == newProprtyParam.Name && p.Value?.ToString() == newProprtyParam.Value));
        Assert.IsNotNull(comp.Items);
        Assert.AreEqual(1, comp.Items.Count());

        var part = comp.Items.FirstOrDefault(p => p is Part);
        Assert.IsNotNull(part);
        Assert.IsNotNull(part.AdditionalProperties);
        Assert.IsNotNull(part.AdditionalProperties.FirstOrDefault(p => p.Key == newProprtyParam.Name && p.Value?.ToString() == newProprtyParam.Value));
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
    public void Convert_ProjectWithGroupsToOrder_LargeProject()
    {
        var fileInfo = new FileInfo("TestData\\PositionGroups.zip");

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