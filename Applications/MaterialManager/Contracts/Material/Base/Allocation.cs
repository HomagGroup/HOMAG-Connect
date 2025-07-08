using System;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    public abstract class Allocation
    {
        public AdditionalDataEntity AdditionalData { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; } = new Dictionary<string, object>();

        public string Comments { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string Workstation { get; set; }
    }
}