using System.Collections.ObjectModel;

using HomagConnect.OrderManager.Contracts.Extensions;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

using OrderItemBase = HomagConnect.OrderManager.Contracts.OrderItems.Base;

namespace HomagConnect.OrderManager.Tests.Extensions
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Extensions")]
    public sealed class OrderDetailsExtensionsTests
    {
        private static Group CreateGroup(string id, params OrderItemBase[] children)
        {
            var group = new Group { Id = id };

            if (children.Length > 0)
            {
                group.Items = new Collection<OrderItemBase>([.. children]);
            }

            return group;
        }

        private static OrderDetails CreateOrderDetails(params OrderItemBase[] items)
        {
            return new OrderDetails
            {
                Items = items.Length > 0 ? new Collection<OrderItemBase>([.. items]) : null
            };
        }

        [TestMethod]
        public void HasErrors_TopLevelError_ReturnsTrue()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Error" });

            Assert.IsTrue(orderDetails.HasErrors());
        }

        [TestMethod]
        public void HasErrors_NestedError_ReturnsTrue()
        {
            var orderDetails = CreateOrderDetails(CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.IsTrue(orderDetails.HasErrors());
        }

        [TestMethod]
        public void HasErrors_OnlyWarning_ReturnsFalse()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Warning" });

            Assert.IsFalse(orderDetails.HasErrors());
        }

        [TestMethod]
        public void HasErrors_NoItems_ReturnsFalse()
        {
            var orderDetails = CreateOrderDetails();

            Assert.IsFalse(orderDetails.HasErrors());
        }

        [TestMethod]
        public void HasErrors_NullOrderDetails_ReturnsFalse()
        {
            OrderDetails? orderDetails = null;

            Assert.IsFalse(orderDetails.HasErrors());
        }

        [TestMethod]
        public void HasWarnings_TopLevelWarning_ReturnsTrue()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Warning" });

            Assert.IsTrue(orderDetails.HasWarnings());
        }

        [TestMethod]
        public void HasWarnings_NestedWarning_ReturnsTrue()
        {
            var orderDetails = CreateOrderDetails(CreateGroup("child", new ErrorInfo { Category = "Warning" }));

            Assert.IsTrue(orderDetails.HasWarnings());
        }

        [TestMethod]
        public void HasWarnings_OnlyError_ReturnsFalse()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Error" });

            Assert.IsFalse(orderDetails.HasWarnings());
        }

        [TestMethod]
        public void HasWarnings_NullOrderDetails_ReturnsFalse()
        {
            OrderDetails? orderDetails = null;

            Assert.IsFalse(orderDetails.HasWarnings());
        }

        [TestMethod]
        public void CountErrors_CountsTopLevelAndNestedErrors()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Error" }, CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.AreEqual(2, orderDetails.CountErrors());
        }

        [TestMethod]
        public void CountErrors_NoItems_ReturnsZero()
        {
            var orderDetails = CreateOrderDetails();

            Assert.AreEqual(0, orderDetails.CountErrors());
        }

        [TestMethod]
        public void CountErrors_NullOrderDetails_ReturnsZero()
        {
            OrderDetails? orderDetails = null;

            Assert.AreEqual(0, orderDetails.CountErrors());
        }

        [TestMethod]
        public void CountWarnings_CountsTopLevelAndNestedWarnings()
        {
            var orderDetails = CreateOrderDetails(new ErrorInfo { Category = "Warning" }, CreateGroup("child", new ErrorInfo { Category = "Warning" }));

            Assert.AreEqual(2, orderDetails.CountWarnings());
        }

        [TestMethod]
        public void CountWarnings_NoItems_ReturnsZero()
        {
            var orderDetails = CreateOrderDetails();

            Assert.AreEqual(0, orderDetails.CountWarnings());
        }

        [TestMethod]
        public void CountWarnings_NullOrderDetails_ReturnsZero()
        {
            OrderDetails? orderDetails = null;

            Assert.AreEqual(0, orderDetails.CountWarnings());
        }
    }
}
