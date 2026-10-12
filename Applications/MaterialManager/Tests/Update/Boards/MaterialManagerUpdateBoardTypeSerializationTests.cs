using HomagConnect.Base.Contracts;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Update;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Boards;

/// <summary>
/// Verifies JSON serialization of board type updates.
/// </summary>
[TestClass]
[UnitTest("MaterialManager.Boards")]
public class MaterialManagerUpdateBoardTypeSerializationTests
{
    /// <summary>
    /// Verifies that explicitly clearing costs is represented as JSON null.
    /// </summary>
    [TestMethod]
    public void MaterialManagerUpdateBoardType_SerializesExplicitNullCosts()
    {
        var update = new MaterialManagerUpdateBoardType
        {
            Costs = null
        };

        var serialized = JsonConvert.SerializeObject(update, SerializerSettings.Default);
        var json = JObject.Parse(serialized);

        json.ContainsKey("costs").ShouldBeTrue("because an explicitly cleared value must be included in the PATCH payload");
        json["costs"]!.Type.ShouldBe(JTokenType.Null);
    }

    /// <summary>
    /// Verifies that an unspecified costs value is omitted from the JSON payload.
    /// </summary>
    [TestMethod]
    public void MaterialManagerUpdateBoardType_OmitsUnspecifiedCosts()
    {
        var update = new MaterialManagerUpdateBoardType();

        var serialized = JsonConvert.SerializeObject(update, SerializerSettings.Default);
        var json = JObject.Parse(serialized);

        json.ContainsKey("costs").ShouldBeFalse("because an unspecified value must not clear the existing costs");
    }
}
