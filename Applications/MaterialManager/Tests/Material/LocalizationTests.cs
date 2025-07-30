using System.Globalization;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Tests.Material
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    public class LocalizationTests
    {
        /// <summary />
        [TestMethod]
        public void MaterialManager_Localization_BoardMaterialCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            Assert.IsTrue(displayNames.Count > 0);
            Assert.AreEqual("Spanplatte", displayNames[BoardMaterialCategory.Chipboard]);

            displayNames.Trace();
        }

        /// <summary>
        /// </summary>
        /// <param name="cultureName"></param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_BoardType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);

            var boardType = new BoardType();
            var propertyDisplayNames = boardType.GetPropertyDisplayNames(culture);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ArticleNumber), culture), propertyDisplayNames[nameof(BoardType.ArticleNumber)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_LastUsed), culture), propertyDisplayNames[nameof(BoardType.LastUsed)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_UnitSystem), culture), propertyDisplayNames[nameof(BoardType.UnitSystem)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ExtensionData), culture), propertyDisplayNames[nameof(BoardType.ExtensionData)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialCode), culture), propertyDisplayNames[nameof(BoardType.MaterialCode)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Thickness), culture), propertyDisplayNames[nameof(BoardType.Thickness)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialCategory), culture), propertyDisplayNames[nameof(BoardType.MaterialCategory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_CoatingCategory), culture), propertyDisplayNames[nameof(BoardType.CoatingCategory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_StandardQuality), culture), propertyDisplayNames[nameof(BoardType.StandardQuality)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialLastUsed), culture), propertyDisplayNames[nameof(BoardType.MaterialLastUsed)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_BoardCode), culture), propertyDisplayNames[nameof(BoardType.BoardCode)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Width), culture), propertyDisplayNames[nameof(BoardType.Width)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Length), culture), propertyDisplayNames[nameof(BoardType.Length)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Grain), culture), propertyDisplayNames[nameof(BoardType.Grain)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Costs), culture), propertyDisplayNames[nameof(BoardType.Costs)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Density), culture), propertyDisplayNames[nameof(BoardType.Density)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_BoardTypeType), culture), propertyDisplayNames[nameof(BoardType.BoardTypeType)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ManufacturerName), culture), propertyDisplayNames[nameof(BoardType.ManufacturerName)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ProductName), culture), propertyDisplayNames[nameof(BoardType.ProductName)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ArticleNumber), culture), propertyDisplayNames[nameof(BoardType.ArticleNumber)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_DecorCode), culture), propertyDisplayNames[nameof(BoardType.DecorCode)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_DecorName), culture), propertyDisplayNames[nameof(BoardType.DecorName)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Gtin), culture), propertyDisplayNames[nameof(BoardType.Gtin)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_EmbossingTop), culture), propertyDisplayNames[nameof(BoardType.EmbossingTop)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_EmbossingBottom), culture), propertyDisplayNames[nameof(BoardType.EmbossingBottom)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ExternalId), culture), propertyDisplayNames[nameof(BoardType.ExternalId)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAvailableWarningLimit), culture),
                propertyDisplayNames[nameof(BoardType.TotalQuantityAvailableWarningLimit)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAvailableWarningLimit), culture),
                propertyDisplayNames[nameof(BoardType.TotalAreaAvailableWarningLimit)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_OptimizeAgainstInfinite), culture),
                propertyDisplayNames[nameof(BoardType.OptimizeAgainstInfinite)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_LockedForOptimization), culture), propertyDisplayNames[nameof(BoardType.LockedForOptimization)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityInInventory), culture),
                propertyDisplayNames[nameof(BoardType.TotalQuantityInInventory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAllocated), culture), propertyDisplayNames[nameof(BoardType.TotalQuantityAllocated)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAvailable), culture), propertyDisplayNames[nameof(BoardType.TotalQuantityAvailable)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalValueInInventory), culture), propertyDisplayNames[nameof(BoardType.TotalValueInInventory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaInInventory), culture), propertyDisplayNames[nameof(BoardType.TotalAreaInInventory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAllocated), culture), propertyDisplayNames[nameof(BoardType.TotalAreaAllocated)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAvailable), culture), propertyDisplayNames[nameof(BoardType.TotalAreaAvailable)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_InsufficientInventory), culture), propertyDisplayNames[nameof(BoardType.InsufficientInventory)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Comments), culture), propertyDisplayNames[nameof(BoardType.Comments)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Thumbnail), culture), propertyDisplayNames[nameof(BoardType.Thumbnail)]);
        }


        /// <summary>
        /// </summary>
        /// <param name="cultureName"></param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_BoardEntity(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);

            var boardEntity = new BoardEntity();
            var propertyDisplayNames = boardEntity.GetPropertyDisplayNames(culture);

            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Id), culture), propertyDisplayNames[nameof(BoardEntity.Id)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Length), culture), propertyDisplayNames[nameof(BoardEntity.Length)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Width), culture), propertyDisplayNames[nameof(BoardEntity.Width)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Quantity), culture), propertyDisplayNames[nameof(BoardEntity.Quantity)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_ManagementType), culture), propertyDisplayNames[nameof(BoardEntity.ManagementType)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Comments), culture), propertyDisplayNames[nameof(BoardEntity.Comments)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_CreationDate), culture), propertyDisplayNames[nameof(BoardEntity.CreationDate)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Location), culture), propertyDisplayNames[nameof(BoardEntity.Location)]);
            Assert.AreEqual(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_BoardType), culture), propertyDisplayNames[nameof(BoardEntity.BoardType)]);

        }

        /// <summary>
        /// Tests the localization of the BoardTypeType enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_BoardTypeType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<BoardTypeType>(culture);

            foreach (BoardTypeType value in Enum.GetValues(typeof(BoardTypeType)))
            {
                var expected = BoardTypeTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the BookHeightMode enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_BookHeightMode(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<BookHeightMode>(culture);

            foreach (BookHeightMode value in Enum.GetValues(typeof(BookHeightMode)))
            {
                var expected = BookHeightModeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the CoatingCategory enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_CoatingCategory(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<CoatingCategory>(culture);

            foreach (CoatingCategory value in Enum.GetValues(typeof(CoatingCategory)))
            {
                var expected = CoatingCategoryDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the EdgebandingProcess enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_EdgebandingProcess(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<EdgebandingProcess>(culture);

            foreach (EdgebandingProcess value in Enum.GetValues(typeof(EdgebandingProcess)))
            {
                var expected = EdgebandingProcessDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the EdgebandMaterialCategory enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_EdgebandMaterialCategory(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<EdgebandMaterialCategory>(culture);

            foreach (EdgebandMaterialCategory value in Enum.GetValues(typeof(EdgebandMaterialCategory)))
            {
                var expected = EdgebandMaterialCategoryDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary />
        [TestMethod]
        public void MaterialManager_Localization_Grain()
        {
            var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("Keine", displayNames[Grain.None]);

            displayNames.Trace();
        }

        /// <summary>
        /// Tests the localization of the ImageSize enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_ImageSize(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<ImageSize>(culture);

            foreach (ImageSize value in Enum.GetValues(typeof(ImageSize)))
            {
                var expected = ImageSizeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the ImageType enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_ImageType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<ImageType>(culture);

            foreach (ImageType value in Enum.GetValues(typeof(ImageType)))
            {
                var expected = ImageTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the ManagementType enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_ManagementType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<ManagementType>(culture);

            foreach (ManagementType value in Enum.GetValues(typeof(ManagementType)))
            {
                var expected = ManagementTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the StandardQuality enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_StandardQuality(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<StandardQuality>(culture);

            foreach (StandardQuality value in Enum.GetValues(typeof(StandardQuality)))
            {
                var expected = StandardQualityDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the TensionTrimType enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_TensionTrimType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<TensionTrimType>(culture);

            foreach (TensionTrimType value in Enum.GetValues(typeof(TensionTrimType)))
            {
                var expected = TensionTrimTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        /// <summary>
        /// Tests the localization of the WorkstationType enumeration for a given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
        [DataRow("de")]
        [DataRow("en")]
        [TestMethod]
        public void MaterialManager_Localization_WorkstationType(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            var displayNames = EnumExtensions.GetDisplayNames<WorkstationType>(culture);

            foreach (WorkstationType value in Enum.GetValues(typeof(WorkstationType)))
            {
                var expected = WorkstationTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
                Assert.AreEqual(CapitalizeFirstLetter(expected), displayNames[value], $"Mismatch for {value} in culture {cultureName}");
            }
        }

        private static string CapitalizeFirstLetter(string? input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (input.Length == 1) return input.ToUpper();
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}