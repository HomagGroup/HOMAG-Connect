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
    [TestMethod]
    public void Project_ConvertToOrder_AllPropertiesSet()
    {
        var fileInfo = DataExchangeSamples.GetProjectHavingTypicalProperties();
        var zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        var project = Project.Load(zipArchive);
        var projectWrapper = new ProjectWrapper(project);

        var orders = project.ConvertToOrders().ToList();

        Assert.IsNotNull(project.Orders);
        Assert.IsNotNull(orders);

        Assert.AreEqual(project.Orders.Count, orders.Count);

        for (var i = 0; i < projectWrapper.Orders.Count; i++)
        {
            var source = projectWrapper.Orders[i];
            var target = orders[i];

            source.Trace("Source");
            target.Trace("Target");

            Assert.AreEqual(source.OrderName, target.OrderName);
            Assert.AreEqual(source.OrderNumber, target.OrderNumber);
        }
    }
}