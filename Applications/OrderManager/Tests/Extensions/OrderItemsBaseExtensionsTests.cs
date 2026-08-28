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
                group.Items = new Collection<OrderItemBase>([.. children]);
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

        [TestMethod]
        public void ClearItems_RemovesMatchingTopLevelItems()
        {
            var keep = CreateGroup("keep");
            var items = new Collection<OrderItemBase> { CreateGroup("remove"), keep };

            items.ClearItems(item => item.Id == "remove");

            Assert.AreEqual(1, items.Count);
            Assert.AreSame(keep, items[0]);
        }

        [TestMethod]
        public void ClearItems_NonRecursive_DoesNotRemoveNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("remove"));
            var items = new Collection<OrderItemBase> { root };

            items.ClearItems(item => item.Id == "remove");

            Assert.AreEqual(1, root.Items!.Count);
        }

        [TestMethod]
        public void ClearItems_Recursive_RemovesNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("remove"), CreateGroup("keep"));
            var items = new Collection<OrderItemBase> { root };

            items.ClearItems(item => item.Id == "remove", recursive: true);

            Assert.AreEqual(1, root.Items!.Count);
            Assert.AreEqual("keep", root.Items![0].Id);
        }

        [TestMethod]
        public void ClearItems_NullPredicate_ThrowsArgumentNullException()
        {
            var items = new Collection<OrderItemBase> { CreateGroup("A") };

            Assert.ThrowsExactly<ArgumentNullException>(() => items.ClearItems(null!));
        }

        [TestMethod]
        public void ClearItems_NullSource_DoesNotThrow()
        {
            Collection<OrderItemBase>? items = null;

            items.ClearItems(item => true);
        }

        [TestMethod]
        public void FindAll_ReturnsAllMatchingItems()
        {
            var first = CreateGroup("match");
            var second = CreateGroup("match");
            var items = new[] { first, CreateGroup("other"), second };

            var result = items.FindAll(item => item.Id == "match").ToList();

            CollectionAssert.AreEqual(new[] { first, second }, result);
        }

        [TestMethod]
        public void FindAll_NoMatch_ReturnsEmpty()
        {
            var items = new[] { CreateGroup("A"), CreateGroup("B") };

            var result = items.FindAll(item => item.Id == "Z");

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void FindAll_DoesNotSearchNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("match"));
            var items = new[] { root };

            var result = items.FindAll(item => item.Id == "match");

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void FindAll_IgnoresNullEntries()
        {
            var target = CreateGroup("A");
            var items = new OrderItemBase?[] { null, target };

            var result = items.FindAll(item => item.Id == "A").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(target, result[0]);
        }

        [TestMethod]
        public void FindAll_NullSource_ReturnsEmpty()
        {
            IEnumerable<OrderItemBase?>? items = null;

            var result = items.FindAll(item => true);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void FindAll_NullPredicate_ThrowsArgumentNullException()
        {
            var items = new[] { CreateGroup("A") };

            Assert.ThrowsExactly<ArgumentNullException>(() => items.FindAll(null!));
        }
    }
}
