using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.Base.TestBase.Helpers;

/// <summary>
/// Provides reusable validation for roundtrip serialization of contract types that expose an <c>AdditionalProperties</c> extension-data property.
/// </summary>
public static class RoundtripTestSerializationValidator
{
    private const string _AdditionalPropertiesPropertyName = "AdditionalProperties";

    /// <summary>
    /// Serializes and deserializes all matching contract types and verifies that no unexpected extension-data entries remain.
    /// </summary>
    public static void SerializeDeserialize_RoundtripDoesNotAddAdditionalProperties_ForAllContracts(
        Assembly assembly,
        string contractsNamespacePrefix,
        TestContext? testContext,
        JsonSerializerSettings? serializerSettings = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentException.ThrowIfNullOrWhiteSpace(contractsNamespacePrefix);
        ArgumentNullException.ThrowIfNull(testContext);

        var effectiveSerializerSettings = serializerSettings ?? SerializerSettings.Default;

        foreach (var contractType in GetContractTypesWithAdditionalProperties(assembly, contractsNamespacePrefix))
        {
            ValidateRoundtrip(contractType, testContext, effectiveSerializerSettings);
        }
    }

    private static void ValidateRoundtrip(Type contractType, TestContext testContext, JsonSerializerSettings serializerSettings)
    {
        var contract = PublicPropertyValueInitializer.CreateInstance(contractType);
        PublicPropertyValueInitializer.PopulatePublicProperties(contract);

        var additionalPropertiesProperty = GetAdditionalPropertiesProperty(contractType);
        additionalPropertiesProperty.SetValue(contract, null);

        var serializedContract = JsonConvert.SerializeObject(contract, serializerSettings);
        var deserializedContract = JsonConvert.DeserializeObject(serializedContract, contractType, serializerSettings);

        Assert.IsNotNull(deserializedContract);

        testContext.AddResultFile(deserializedContract.TraceToFile(contractType.Name + "-deserialized").FullName);

        serializedContract.ShouldNotBeNullOrWhiteSpace(
            $"because '{contractType.FullName}' should serialize to JSON");
        deserializedContract.ShouldNotBeNull(
            $"because '{contractType.FullName}' should deserialize from its serialized JSON");

        var deserializedAdditionalProperties = (IDictionary<string, object>?)additionalPropertiesProperty.GetValue(deserializedContract);
        deserializedAdditionalProperties.ShouldBeNullOrEmpty(
            $"because '{contractType.FullName}' should not contain any extension-data entries after roundtrip serialization. Contained keys: {string.Join(", ", deserializedAdditionalProperties?.Keys ?? [])}");
    }

    private static PropertyInfo GetAdditionalPropertiesProperty(Type contractType)
    {
        var additionalPropertiesProperty = contractType.GetProperty(_AdditionalPropertiesPropertyName, BindingFlags.Instance | BindingFlags.Public);
        additionalPropertiesProperty.ShouldNotBeNull(
            $"because '{contractType.FullName}' is expected to expose an '{_AdditionalPropertiesPropertyName}' property");

        return additionalPropertiesProperty!;
    }

    private static IEnumerable<Type> GetContractTypesWithAdditionalProperties(Assembly assembly, string contractsNamespacePrefix)
    {
        return assembly
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false, Namespace: not null } &&
                type.Namespace.StartsWith(contractsNamespacePrefix, StringComparison.Ordinal) &&
                type.GetProperty(_AdditionalPropertiesPropertyName, BindingFlags.Instance | BindingFlags.Public) != null)
            .OrderBy(type => type.FullName, StringComparer.Ordinal);
    }
}
