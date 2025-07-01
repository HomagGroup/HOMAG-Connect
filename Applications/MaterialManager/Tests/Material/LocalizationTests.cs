using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Base;
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
            }

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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
            }
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
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
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.ToLower(), displayNames[value].ToLower(), $"Mismatch for {value} in culture {cultureName}");
            }

        }

    }
}