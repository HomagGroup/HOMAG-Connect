using System.IO.Compression;

using HomagConnect.Base.Extensions;
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
        var zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        var project = ProjectPersistenceManager.Load(zipArchive);
        var projectWrapper = new ProjectWrapper(project);

        var groups = project.ConvertToGroups().ToList();

        Assert.IsNotNull(project.Orders);
        Assert.IsNotNull(groups);

        Assert.AreEqual(project.Orders.Count, groups.Count);

        for (var i = 0; i < projectWrapper.Orders.Count; i++)
        {
            var source = projectWrapper.Orders[i];
            var target = groups[i];

            TestContext.AddResultFile(source.TraceToFile("Source").FullName);
            TestContext.AddResultFile(target.TraceToFile("Target").FullName);

            if (target.AdditionalProperties is { Count: > 0 })
            {
                target.AdditionalProperties.Trace("Unknown properties");
            }

            Assert.AreEqual(source.OrderName, target.Name);
        }
    }

    /// <summary />
    private void ConvertProjectToOrder(FileInfo fileInfo)
    {
        var zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        var project = ProjectPersistenceManager.Load(zipArchive);
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
            Assert.AreEqual(source.OrderNumber, target.OrderNumber);
        }
    }
}