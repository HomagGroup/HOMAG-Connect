using HomagConnect.Base.Contracts.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using Shouldly;

namespace HomagConnect.Base.Tests
{
    [TestClass]
    public class TolerantEnumConverterTests
    {
        [Flags]
        [JsonConverter(typeof(TolerantEnumConverter))]
        public enum AddressType
        {
            Unknown,
            Home,
            Work
        }
        public class Test
        {
            public AddressType? Type { get; set; }
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(AddressType.Unknown)]
        [DataRow(AddressType.Home)]
        [DataRow(AddressType.Work)]
        [DataRow(AddressType.Work | AddressType.Home)]
        public void TestTolerantEnumConverter(AddressType? addressType)
        {
            // Arrange
            var o = new Test();
            o.Type = addressType;

            // Act
            var json = JsonConvert.SerializeObject(o, SerializerSettings.Default);
            var o2 = JsonConvert.DeserializeObject<Test>(json, SerializerSettings.Default);

            // Assert
            o2.ShouldNotBeNull();
            o2!.Type.ShouldBe(o.Type);
        }

        [DataTestMethod]
        [DataRow("Unknown", AddressType.Unknown)]
        [DataRow("Home", AddressType.Home)]
        [DataRow("Work", AddressType.Work)]
        [DataRow("Work, Home", AddressType.Work | AddressType.Home)]
        [DataRow("ABC", AddressType.Unknown)]
        [DataRow("ABC, Home", AddressType.Unknown)]
        public void TestTolerantEnumConverterJson(string enumJson, AddressType addressType)
        {
            // Arrange
            var json = "{'type': '" + enumJson + "' }";

            // Act
            var o2 = JsonConvert.DeserializeObject<Test>(json, SerializerSettings.Default);

            // Assert
            o2.ShouldNotBeNull();
            o2!.Type.ShouldBe(addressType);
        }

        [JsonConverter(typeof(TolerantEnumConverter))]
        public enum AddressType2
        {
            Home,
            Work,
            Test
        }
        public class Test2
        {
            public AddressType2 Type { get; set; }
        }

        [DataTestMethod]
        [DataRow("Unknown", AddressType2.Home)]
        [DataRow("Home", AddressType2.Home)]
        [DataRow("Work", AddressType2.Work)]
        [DataRow("Work, Test, Home", AddressType2.Work | AddressType2.Home | AddressType2.Test)]
        [DataRow("ABC", AddressType2.Home)]
        [DataRow("ABC, Home", AddressType2.Home)]
        public void TestTolerantEnumConverterJson2(string enumJson, AddressType2 addressType)
        {
            // Arrange
            var json = "{'type': '" + enumJson + "' }";

            // Act
            var o2 = JsonConvert.DeserializeObject<Test2>(json, SerializerSettings.Default);

            // Assert
            o2.Type.ShouldBe(addressType);
        }
    }
}
