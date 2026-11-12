using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

using HomagConnect.Base.Contracts;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Allocations;

/// <summary>
/// Verifies partial allocation payloads without relying on service-side PATCH support.
/// </summary>
[TestClass]
public class EdgebandAllocationPatchPayloadTests
{
    /// <summary />
    [TestMethod]
    public async Task UpdateComments_OmitsAllUnspecifiedValues()
    {
        var json = await CaptureUpdate(new EdgebandTypeAllocationUpdate
        {
            EdgebandCode = "EDGE-001", Order = "ORDER-001", Comments = "Updated notes"
        });

        json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(
            new[] { "comments", "edgebandCode", "order" });
        json["comments"]!.Value<string>().ShouldBe("Updated notes");
        json["edgebandCode"]!.Value<string>().ShouldBe("EDGE-001");
        json["order"]!.Value<string>().ShouldBe("ORDER-001");
    }

    /// <summary />
    [TestMethod]
    public async Task UpdateLengths_PreservesExplicitZeroAndOmitsNulls()
    {
        var json = await CaptureUpdate(new EdgebandTypeAllocationUpdate
        {
            EdgebandCode = "EDGE-001", Order = "ORDER-001",
            AllocatedLength = 0, UsedLength = 0,
            Customer = null, Project = null, Comments = null, Workstation = null
        });

        json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(
            new[] { "allocatedLength", "edgebandCode", "order", "usedLength" });
        json["allocatedLength"]!.Value<double>().ShouldBe(0);
        json["usedLength"]!.Value<double>().ShouldBe(0);
    }

    /// <summary />
    [TestMethod]
    public async Task UpdateOptionalStrings_PreservesExplicitEmptyValues()
    {
        var json = await CaptureUpdate(new EdgebandTypeAllocationUpdate
        {
            EdgebandCode = "EDGE-001", Order = "ORDER-001",
            Customer = "", Project = "", Comments = "", Workstation = ""
        });

        foreach (var name in new[] { "customer", "project", "comments", "workstation" })
        {
            json[name].ShouldNotBeNull();
            json[name]!.Value<string>().ShouldBe(string.Empty);
        }
        json.ContainsKey("allocatedLength").ShouldBeFalse();
        json.ContainsKey("usedLength").ShouldBeFalse();
    }

    /// <summary />
    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public async Task UpdateOneLength_OmitsTheOther(bool updateAllocatedLength)
    {
        var update = new EdgebandTypeAllocationUpdate
        {
            EdgebandCode = "EDGE-001", Order = "ORDER-001"
        };
        if (updateAllocatedLength)
        {
            update.AllocatedLength = 12.5;
        }
        else
        {
            update.UsedLength = 2.5;
        }

        var json = await CaptureUpdate(update);

        json.ContainsKey(updateAllocatedLength ? "usedLength" : "allocatedLength").ShouldBeFalse();
        json[updateAllocatedLength ? "allocatedLength" : "usedLength"]!.Value<double>()
            .ShouldBe(updateAllocatedLength ? 12.5 : 2.5);
    }

    /// <summary />
    [TestMethod]
    [DataRow("", "ORDER-001")]
    [DataRow("EDGE-001", "")]
    public async Task UpdateWithoutIdentifier_FailsBeforeSending(string edgebandCode, string order)
    {
        using var handler = new CaptureHandler();
        using var http = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialManagerClientMaterialEdgebands(http);

        await Should.ThrowAsync<ValidationException>(() => client.UpdateEdgebandTypeAllocation(
            new EdgebandTypeAllocationUpdate { EdgebandCode = edgebandCode, Order = order }));

        handler.Payload.ShouldBeNull();
    }

    /// <summary />
    [TestMethod]
    public void CreationContract_RetainsOriginalDefaultsAndRequiredLength()
    {
        var request = new EdgebandTypeAllocationRequest();
        var json = JObject.Parse(JsonConvert.SerializeObject(request, SerializerSettings.Default));

        json["customer"]!.Value<string>().ShouldBe(string.Empty);
        json["project"]!.Value<string>().ShouldBe(string.Empty);
        typeof(EdgebandTypeAllocationRequest).GetProperty(nameof(request.AllocatedLength))!
            .PropertyType.ShouldBe(typeof(double));
        typeof(EdgebandTypeAllocationRequest).GetProperty(nameof(request.UsedLength))!
            .PropertyType.ShouldBe(typeof(double));
    }

    private static async Task<JObject> CaptureUpdate(EdgebandTypeAllocationUpdate update)
    {
        using var handler = new CaptureHandler();
        using var http = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialManagerClientMaterialEdgebands(http);

        await client.UpdateEdgebandTypeAllocation(update);

        handler.Method.ShouldBe(new HttpMethod("PATCH"));
        handler.Path.ShouldBe("/api/materialManager/materials/edgebands/allocations");
        handler.Payload.ShouldNotBeNull();
        return JObject.Parse(handler.Payload);
    }

    private sealed class CaptureHandler : HttpMessageHandler
    {
        public string? Payload { get; private set; }
        public HttpMethod? Method { get; private set; }
        public string? Path { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Method = request.Method;
            Path = request.RequestUri!.AbsolutePath;
            Payload = await request.Content!.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };
        }
    }
}
