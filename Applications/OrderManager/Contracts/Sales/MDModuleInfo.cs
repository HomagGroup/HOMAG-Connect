using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Master data describing a module that can be used to compose an article.
    /// </summary>
    public class MDModuleInfo
    {
        /// <summary>
        /// The module id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The localized short name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// A localized description of the module.
        /// </summary>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// The localized group this module belongs to (used to group modules in the UI).
        /// </summary>
        [JsonProperty("group")]
        public string? Group { get; set; }

        /// <summary>
        /// An optional URL pointing to a preview image for this module.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// The ids of the attributes that are assigned to this module.
        /// See also <see cref="MDAttributeInfo"/> for more info about attributes.
        /// </summary>
        [JsonProperty("assignedAttributes")]
        public IList<string>? AssignedAttributes { get; set; }

        /// <summary>
        /// The ids of the modules that are allowed as direct children of this module.
        /// See also <see cref="MDModuleInfo"/> for more info about modules.
        /// </summary>
        [JsonProperty("allowedChildModules")]
        public IList<string>? AllowedChildModules { get; set; }

        /// <summary>
        /// Contains the initial attribute data when we create a new module
        /// </summary>
        [JsonProperty("initialAttributes")]
        public IDictionary<string, string>? InitialAttributes { get; set; }

        /// <summary>
        /// Indicates whether this module can be used as a root module of an article.
        /// </summary>
        [JsonProperty("isRoot")]
        public bool? IsRoot { get; set; }

        /// <summary>
        /// Indicates if the module is a dummy module
        /// </summary>
        [JsonProperty("isConfigDummy")]
        public bool? IsConfigDummy { get; set; }

        /// <summary>
        /// Indicates if the module is drop container
        /// </summary>
        [JsonProperty("isDropContainer")]
        public bool? IsDropContainer { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
