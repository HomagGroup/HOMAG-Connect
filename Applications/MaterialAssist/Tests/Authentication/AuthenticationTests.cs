using FluentAssertions;

using HomagConnect.MaterialAssist.Samples.Authentication;

namespace HomagConnect.MaterialAssist.Tests.Authentication;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Authentication")]
public class AuthenticationTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task Authentication_UsingHttpClient()
    {
        // Act
        var act = async () => await AuthenticationSamples.CreateMaterialAssistClientUsingHttpClient(SubscriptionId, AuthorizationKey);

        // Assert
        await act.Should().NotThrowAsync(
            "because authentication using HttpClient with valid subscription ID and authorization key should succeed and retrieve board entities");
    }

    [TestMethod]
    public async Task Authentication_UsingSubscriptionIdAndToken()
    {
        // Act
        var act = async () => await AuthenticationSamples.CreateMaterialAssistClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);

        // Assert
        await act.Should().NotThrowAsync(
            "because authentication using subscription ID and authorization key should succeed and retrieve board entities");
    }
}