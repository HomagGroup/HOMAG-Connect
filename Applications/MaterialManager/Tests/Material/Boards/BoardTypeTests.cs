using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Tests;
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
        protected override Guid UserSecretsFolder { get; set; } = new("7a028258-94b9-4d79-822a-1005e4558b74");

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
                Length = 1000,
                Width = 600,
                Thickness = 19
            };

            Trace(boardTypeMetric);

            var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial);

            Trace(boardTypeImperial);

            Assert.AreEqual(UnitSystem.Imperial, boardTypeImperial.UnitSystem);

            Assert.AreNotEqual(boardTypeMetric.Length, boardTypeImperial.Length);
            Assert.AreNotEqual(boardTypeMetric.Width, boardTypeImperial.Width);
            Assert.AreNotEqual(boardTypeMetric.Thickness, boardTypeImperial.Thickness);
        }
    }
}