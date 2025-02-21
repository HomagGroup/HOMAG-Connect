using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Tests.Material.Boards
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Boards")]
    public class BoardTypeTests : TestBase
    {
        /// <summary />
        [TestMethod]
        public void BoardType_CheckConfiguration_ConfigValid()
        {
            BaseUrl.Should().NotBeNull();
            SubscriptionId.Should().NotBeEmpty();
            AuthorizationKey.Should().NotBeNullOrEmpty();
        }

        /// <summary />
        [TestMethod]
        public void BoardType_SwitchUnitSystem_LengthWidthThicknessChanged()
        {
            var boardTypeMetric = new BoardType
            {
                Length = 2800,
                Width = 2070,
                Thickness = 19,
                TotalAreaAvailableWarningLimit = 60
            };

            boardTypeMetric.Trace();

            var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

            boardTypeImperial.Trace();

            Assert.AreEqual(UnitSystem.Imperial, boardTypeImperial.UnitSystem);

            Assert.AreNotEqual(boardTypeMetric.Length, boardTypeImperial.Length);
            Assert.AreNotEqual(boardTypeMetric.Width, boardTypeImperial.Width);
            Assert.AreNotEqual(boardTypeMetric.Thickness, boardTypeImperial.Thickness);
            Assert.AreNotEqual(boardTypeMetric.TotalAreaAvailableWarningLimit, boardTypeImperial.TotalAreaAvailableWarningLimit);
        }
    }
}