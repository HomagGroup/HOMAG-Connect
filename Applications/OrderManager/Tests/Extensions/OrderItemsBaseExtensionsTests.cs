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

        [TestMethod]
        public void FindAll_Recursive_FindsNestedItems()
        {
            var nested = CreateGroup("match");
            var root = CreateGroup("root", CreateGroup("match"), CreateGroup("child", nested));
            var items = new[] { root };

            var result = items.FindAll(item => item.Id == "match", recursive: true).ToList();

            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, nested);
        }

        [TestMethod]
        public void FindAll_NonRecursive_DoesNotFindNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("match"));
            var items = new[] { root };

            var result = items.FindAll(item => item.Id == "match", recursive: false);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Count_MatchOnTopLevel_ReturnsMatchCount()
        {
            var items = new[] { CreateGroup("A"), CreateGroup("A"), CreateGroup("B") };

            var result = items.Count(item => item.Id == "A");

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Count_NonRecursive_DoesNotCountNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("match"));
            var items = new[] { root };

            var result = items.Count(item => item.Id == "match");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Count_Recursive_CountsNestedItems()
        {
            var root = CreateGroup("root", CreateGroup("match"), CreateGroup("child", CreateGroup("match")));
            var items = new[] { root };

            var result = items.Count(item => item.Id == "match", recursive: true);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Count_NullSource_ReturnsZero()
        {
            IEnumerable<OrderItemBase?>? items = null;

            var result = items.Count(item => true, recursive: false);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Count_NullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<OrderItemBase?> items = new[] { CreateGroup("A") };
            Func<OrderItemBase, bool>? predicate = null;

            Assert.ThrowsExactly<ArgumentNullException>(() => items.Count(predicate!));
        }

        [TestMethod]
        public void Count_SourceContainingNullEntries_IgnoresNullEntries()
        {
            var items = new OrderItemBase?[] { null, CreateGroup("A") };

            var result = items.Count(item => item.Id == "A", recursive: false);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void FindParent_MatchOnTopLevel_ReturnsParent()
        {
            var target = CreateGroup("match");
            var root = CreateGroup("root", target);
            var items = new[] { root };

            var result = items.FindParent(item => item.Id == "match");

            Assert.AreSame(root, result);
        }

        [TestMethod]
        public void FindParent_MatchNested_ReturnsDirectParent()
        {
            var target = CreateGroup("match");
            var directParent = CreateGroup("directParent", target);
            var root = CreateGroup("root", directParent);
            var items = new[] { root };

            var result = items.FindParent(item => item.Id == "match");

            Assert.AreSame(directParent, result);
        }

        [TestMethod]
        public void FindParent_NoMatch_ReturnsNull()
        {
            var items = new[] { CreateGroup("root", CreateGroup("child")) };

            var result = items.FindParent(item => item.Id == "Z");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindParent_MatchIsTopLevelWithoutParent_ReturnsNull()
        {
            var items = new[] { CreateGroup("root") };

            var result = items.FindParent(item => item.Id == "root");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindParent_NullSource_ReturnsNull()
        {
            IEnumerable<OrderItemBase?>? items = null;

            var result = items.FindParent(item => true);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindParent_NullPredicate_ThrowsArgumentNullException()
        {
            var items = new[] { CreateGroup("A") };

            Assert.ThrowsExactly<ArgumentNullException>(() => items.FindParent(null!));
        }

        [TestMethod]
        public void HasErrors_ItemWithErrorCategory_ReturnsTrue()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Error" });

            Assert.IsTrue(group.HasErrors());
        }

        [TestMethod]
        public void HasErrors_CategoryIsCaseInsensitive_ReturnsTrue()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "eRRor" });

            Assert.IsTrue(group.HasErrors());
        }

        [TestMethod]
        public void HasErrors_OnlyWarning_ReturnsFalse()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Warning" });

            Assert.IsFalse(group.HasErrors());
        }

        [TestMethod]
        public void HasErrors_NonRecursive_DoesNotFindNestedError()
        {
            var group = CreateGroup("root", CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.IsFalse(group.HasErrors());
        }

        [TestMethod]
        public void HasErrors_Recursive_FindsNestedError()
        {
            var group = CreateGroup("root", CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.IsTrue(group.HasErrors(recursive: true));
        }

        [TestMethod]
        public void HasErrors_NullItem_ReturnsFalse()
        {
            OrderItemBase? item = null;

            Assert.IsFalse(item.HasErrors());
        }

        [TestMethod]
        public void HasWarnings_ItemWithWarningCategory_ReturnsTrue()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Warning" });

            Assert.IsTrue(group.HasWarnings());
        }

        [TestMethod]
        public void HasWarnings_OnlyError_ReturnsFalse()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Error" });

            Assert.IsFalse(group.HasWarnings());
        }

        [TestMethod]
        public void HasWarnings_Recursive_FindsNestedWarning()
        {
            var group = CreateGroup("root", CreateGroup("child", new ErrorInfo { Category = "Warning" }));

            Assert.IsTrue(group.HasWarnings(recursive: true));
        }

        [TestMethod]
        public void HasWarnings_NullItem_ReturnsFalse()
        {
            OrderItemBase? item = null;

            Assert.IsFalse(item.HasWarnings());
        }

        [TestMethod]
        public void CountErrors_CountsOnlyErrorCategory()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Error" }, new ErrorInfo { Category = "Warning" }, new ErrorInfo { Category = "Error" });

            Assert.AreEqual(2, group.CountErrors());
        }

        [TestMethod]
        public void CountErrors_NonRecursive_DoesNotCountNestedErrors()
        {
            var group = CreateGroup("root", CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.AreEqual(0, group.CountErrors());
        }

        [TestMethod]
        public void CountErrors_Recursive_CountsNestedErrors()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Error" }, CreateGroup("child", new ErrorInfo { Category = "Error" }));

            Assert.AreEqual(2, group.CountErrors(recursive: true));
        }

        [TestMethod]
        public void CountErrors_NullItem_ReturnsZero()
        {
            OrderItemBase? item = null;

            Assert.AreEqual(0, item.CountErrors());
        }

        [TestMethod]
        public void CountWarnings_CountsOnlyWarningCategory()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Error" }, new ErrorInfo { Category = "Warning" }, new ErrorInfo { Category = "Warning" });

            Assert.AreEqual(2, group.CountWarnings());
        }

        [TestMethod]
        public void CountWarnings_Recursive_CountsNestedWarnings()
        {
            var group = CreateGroup("root", new ErrorInfo { Category = "Warning" }, CreateGroup("child", new ErrorInfo { Category = "Warning" }));

            Assert.AreEqual(2, group.CountWarnings(recursive: true));
        }

        [TestMethod]
        public void CountWarnings_NullItem_ReturnsZero()
        {
            OrderItemBase? item = null;

            Assert.AreEqual(0, item.CountWarnings());
        }
    }
}
