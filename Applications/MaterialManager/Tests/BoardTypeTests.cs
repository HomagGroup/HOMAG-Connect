using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Tests
{
    [TestClass]
    public class BoardTypeTests
    {
        public virtual TestContext? TestContext { get; set; }

        [TestMethod]
        public void BoardType_SwitchUnitSystem_LengthWidthThicknessChanged()
        {
            var boardTypeMetric = new BoardType();

            boardTypeMetric.Length = 1000;
            boardTypeMetric.Width = 600;
            boardTypeMetric.Thickness = 19;

            var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial);

           Assert.AreEqual(UnitSystem.Imperial, boardTypeImperial.UnitSystem);

           Assert.AreNotEqual(boardTypeMetric.Length, boardTypeImperial.Length);
           Assert.AreNotEqual(boardTypeMetric.Width, boardTypeImperial.Width);
           Assert.AreNotEqual(boardTypeMetric.Thickness, boardTypeImperial.Thickness);
        }

        protected void Trace(string o)
        {
            if (TestContext == null)
            {
                Console.WriteLine(o);
            }
            else
            {
                TestContext.WriteLine(o);
            }
        }
    }
}