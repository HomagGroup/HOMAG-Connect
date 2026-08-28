using System.Collections.ObjectModel;

using HomagConnect.OrderManager.Contracts.Extensions;
using HomagConnect.OrderManager.Contracts.OrderItems;

using OrderItemBase = HomagConnect.OrderManager.Contracts.OrderItems.Base;

namespace HomagConnect.OrderManager.Tests.Extensions
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Extensions")]
    public sealed class OrderItemsBaseExtensionsTests
    {
        private static Group CreateGroup(string id, params OrderItemBase[] children)
        {
            var group = new Group { Id = id };

            if (children.Length > 0)
            {
                group.Items = new Collection<OrderItemBase>(children);
            }

            return group;
        }

        [TestMethod]
        public void Find_MatchOnTopLevel_ReturnsItem()
        {
            var target = CreateGroup("A");
            var items = new[] { target, CreateGroup("B") };

            var result = items.Find(item => item.Id == "A");

            Assert.AreSame(target, result);
        }

        [TestMethod]
        public void Find_NoMatch_ReturnsNull()
        {
            var items = new[] { CreateGroup("A"), CreateGroup("B") };

            var result = items.Find(item => item.Id == "Z");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Find_Recursive_FindsNestedItem()
        {
            var nested = CreateGroup("nested");
            var root = CreateGroup("root", CreateGroup("child", nested));
            var items = new[] { root };

            var result = items.Find(item => item.Id == "nested", recursive: true);

            Assert.AreSame(nested, result);
        }

        [TestMethod]
        public void Find_NonRecursive_DoesNotFindNestedItem()
        {
            var nested = CreateGroup("nested");
            var root = CreateGroup("root", CreateGroup("child", nested));
            var items = new[] { root };

            var result = items.Find(item => item.Id == "nested", recursive: false);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Find_NonRecursive_FindsTopLevelItem()
        {
            var target = CreateGroup("root", CreateGroup("child"));
            var items = new[] { target };

            var result = items.Find(item => item.Id == "root", recursive: false);

            Assert.AreSame(target, result);
        }

        [TestMethod]
        public void Find_NullSource_ReturnsNull()
        {
            IEnumerable<OrderItemBase?>? items = null;

            var result = items.Find(item => true);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Find_SourceContainingNullEntries_IgnoresNullEntries()
        {
            var target = CreateGroup("A");
            var items = new OrderItemBase?[] { null, target };

            var result = items.Find(item => item.Id == "A");

            Assert.AreSame(target, result);
        }

        [TestMethod]
        public void Find_NullPredicate_ThrowsArgumentNullException()
        {
            var items = new[] { CreateGroup("A") };

            Assert.ThrowsExactly<ArgumentNullException>(() => items.Find(null!));
        }

        [TestMethod]
        public void Find_MultipleMatches_ReturnsFirstInDepthFirstOrder()
        {
            var first = CreateGroup("match");
            var second = CreateGroup("match");
            var root = CreateGroup("root", first, second);
            var items = new[] { root };

            var result = items.Find(item => item.Id == "match", recursive: true);

            Assert.AreSame(first, result);
        }

        [TestMethod]
        public void GetLibraryId_GroupWithConfigurationPosition_ReturnsLibraryId()
        {
            var group = new Group
            {
                Items = new Collection<OrderItemBase>
                {
                    new ConfigurationPosition { LibraryId = "lib-1" }
                }
            };

            var result = group.GetLibraryId();

            Assert.AreEqual("lib-1", result);
        }

        [TestMethod]
        public void GetLibraryId_ConfigurationPositionNested_ReturnsLibraryId()
        {
            var group = new Group
            {
                Items = new Collection<OrderItemBase>
                {
                    new Group
                    {
                        Items = new Collection<OrderItemBase>
                        {
                            new ConfigurationPosition { LibraryId = "lib-2" }
                        }
                    }
                }
            };

            var result = group.GetLibraryId();

            Assert.AreEqual("lib-2", result);
        }

        [TestMethod]
        public void GetLibraryId_GroupWithoutConfigurationPosition_ReturnsNull()
        {
            var group = CreateGroup("root", CreateGroup("child"));

            var result = group.GetLibraryId();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLibraryId_ConfigurationPositionWithoutLibraryId_ReturnsNull()
        {
            var group = new Group
            {
                Items = new Collection<OrderItemBase>
                {
                    new ConfigurationPosition { LibraryId = null }
                }
            };

            var result = group.GetLibraryId();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLibraryId_NullGroup_ReturnsNull()
        {
            Group? group = null;

            var result = group.GetLibraryId();

            Assert.IsNull(result);
        }
    }
}
