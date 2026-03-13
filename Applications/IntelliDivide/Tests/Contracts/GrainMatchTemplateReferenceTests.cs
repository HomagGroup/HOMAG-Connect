using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Contracts
{
    [TestClass]
    [UnitTest("IntelliDivide.Contracts")]
    public class GrainMatchTemplateReferenceTests
    {
        public TestContext? TestContext { get; set; }

        [TestMethod]
        public void FromString_Should_Parse_TemplateReference_RoundTrip_DefaultTrim()
        {
            var inputTemplate = CreateTemplateReference();

            var parsedTemplate = RoundTrip(inputTemplate);

            inputTemplate.Trim.ShouldBe(GrainMatchTemplateReference.DefaultTrim);
            parsedTemplate.Trim.ShouldBe(GrainMatchTemplateReference.DefaultTrim);
            parsedTemplate.ToString().ShouldBe(inputTemplate.ToString());
        }

        [TestMethod]
        public void FromString_Should_Parse_TemplateReference_RoundTrip_TrimSet()
        {
            const double trim = 300;
            var inputTemplate = CreateTemplateReference(trim);

            var parsedTemplate = RoundTrip(inputTemplate);

            inputTemplate.Trim.ShouldBe(trim);
            parsedTemplate.Trim.ShouldBe(trim);
            parsedTemplate.ToString().ShouldBe(inputTemplate.ToString());
        }

        [TestMethod]
        public void Trim_Should_Be_Zero_When_Trims_None_And_Restore_When_Reenabled()
        {
            var inputTemplate = CreateTemplateReference(30);

            inputTemplate.Trim.ShouldBe(30);
            inputTemplate.Trims = GrainMatchingTemplateOptionsTrims.None;

            var parsedTemplate = RoundTrip(inputTemplate);

            inputTemplate.Trim.ShouldBe(0);
            parsedTemplate.Trim.ShouldBe(0);
            parsedTemplate.Trims.ShouldBe(GrainMatchingTemplateOptionsTrims.None);

            inputTemplate.Trims = GrainMatchingTemplateOptionsTrims.AllSides;
            inputTemplate.Trim.ShouldBe(30);
        }

        private GrainMatchTemplateReference RoundTrip(GrainMatchTemplateReference inputTemplate)
        {
            inputTemplate.Trace();

            var templateString = inputTemplate.ToString();
            TestContext?.WriteLine(templateString);

            var parsedTemplate = GrainMatchTemplateReference.FromString(templateString);

            inputTemplate.Template.ShouldBe(parsedTemplate.Template);
            inputTemplate.Dividing.ShouldBe(parsedTemplate.Dividing);
            inputTemplate.Grain.ShouldBe(parsedTemplate.Grain);
            inputTemplate.Instance.ShouldBe(parsedTemplate.Instance);
            inputTemplate.Trims.ShouldBe(parsedTemplate.Trims);
            inputTemplate.Positions.Length.ShouldBe(parsedTemplate.Positions.Length);

            for (int i = 0; i < inputTemplate.Positions.Length; i++)
            {
                inputTemplate.Positions[i].Column.ShouldBe(parsedTemplate.Positions[i].Column);
                inputTemplate.Positions[i].Row.ShouldBe(parsedTemplate.Positions[i].Row);
            }

            templateString.Trace();
            parsedTemplate.Trace();

            return parsedTemplate;
        }

        private static GrainMatchTemplateReference CreateTemplateReference(double trim = GrainMatchTemplateReference.DefaultTrim)
        {
            return new GrainMatchTemplateReference
            {
                Template = "2 Parts (2 x 1)",
                Positions =
                [
                    new GrainMatchTemplatePosition
                    {
                        Column = 2,
                        Row = 1
                    }
                ],
                Trims = GrainMatchingTemplateOptionsTrims.AllSides,
                Dividing = GrainMatchingTemplateOptionsDividing.SeparatePattern,
                Grain = Grain.Lengthwise,
                Instance = 1,
                Trim = trim
            };
        }
    }
}