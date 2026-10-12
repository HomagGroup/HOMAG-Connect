namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Texture reference with synchronized id and component properties.
    /// </summary>
    public class TextureReference
    {
        private string _catalog = string.Empty;
        private string _decorCode = string.Empty;
        private string? _embossing;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureReference" /> class.
        /// </summary>
        /// <param name="id">The texture identifier in format {Catalog}:DecorCode_Embossing.</param>
        public TextureReference(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureReference" /> class.
        /// </summary>
        /// <param name="catalog">The catalog.</param>
        /// <param name="decorCode">The decor code.</param>
        /// <param name="embossing">The embossing.</param>
        public TextureReference(string catalog, string decorCode, string? embossing = null)
        {
            Catalog = catalog;
            DecorCode = decorCode;
            Embossing = embossing;
        }

        /// <summary>
        /// Gets or sets the texture id in format {Catalog}:DecorCode_Embossing.
        /// Setting this property updates Catalog, DecorCode and Embossing.
        /// </summary>
        public string Id
        {
            get => string.IsNullOrWhiteSpace(Embossing)
                ? $"{Catalog}:{DecorCode}"
                : $"{Catalog}:{DecorCode}_{Embossing}";
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Texture id must not be null or whitespace.", nameof(value));
                }

                var textureIdParts = value.Split(new[] { ":" }, 2, StringSplitOptions.None);

                if (textureIdParts.Length != 2 || string.IsNullOrWhiteSpace(textureIdParts[0]) || string.IsNullOrWhiteSpace(textureIdParts[1]))
                {
                    throw new ArgumentException("Texture id must match format '{Catalog}:DecorCode_Embossing'.", nameof(value));
                }

                var decorAndEmbossingParts = textureIdParts[1].Split(new[] { "_" }, 2, StringSplitOptions.None);

                _catalog = textureIdParts[0];
                _decorCode = decorAndEmbossingParts[0];
                _embossing = decorAndEmbossingParts.Length > 1 && !string.IsNullOrWhiteSpace(decorAndEmbossingParts[1])
                    ? decorAndEmbossingParts[1]
                    : null;
            }
        }

        /// <summary>
        /// Gets or sets the texture catalog.
        /// </summary>
        public string Catalog
        {
            get => _catalog;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Catalog must not be null or whitespace.", nameof(value));
                }

                _catalog = value;
            }
        }

        /// <summary>
        /// Gets or sets the texture decor code.
        /// </summary>
        public string DecorCode
        {
            get => _decorCode;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Decor code must not be null or whitespace.", nameof(value));
                }

                _decorCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the texture embossing.
        /// </summary>
        public string? Embossing
        {
            get => _embossing;
            set => _embossing = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// Implicitly converts a texture reference to its identifier.
        /// </summary>
        /// <param name="textureReference">The texture reference.</param>
        public static implicit operator string?(TextureReference? textureReference)
        {
            return textureReference?.Id;
        }
    }

    /// <summary>
    /// Attribute to specify a texture reference for a board material category.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TextureReferenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureReferenceAttribute" /> class using a texture identifier.
        /// </summary>
        /// <param name="textureId">The texture identifier in format {Catalog}:DecorCode_Embossing.</param>
        public TextureReferenceAttribute(string textureId)
        {
            TextureReference = new TextureReference(textureId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureReferenceAttribute" /> class using catalog, decor code and optional embossing.
        /// </summary>
        /// <param name="catalog">The catalog.</param>
        /// <param name="decorCode">The decor code.</param>
        /// <param name="embossing">The embossing.</param>
        public TextureReferenceAttribute(string catalog, string decorCode, string? embossing = null)
        {
            TextureReference = new TextureReference(catalog, decorCode, embossing);
        }

        /// <summary>
        /// Gets the texture reference object.
        /// </summary>
        public TextureReference TextureReference { get; }

        /// <summary>
        /// Gets the texture catalog.
        /// </summary>
        public string Catalog => TextureReference.Catalog;

        /// <summary>
        /// Gets the texture decor code.
        /// </summary>
        public string DecorCode => TextureReference.DecorCode;

        /// <summary>
        /// Gets the texture embossing.
        /// </summary>
        public string? Embossing => TextureReference.Embossing;

        /// <summary>
        /// Gets the texture identifier.
        /// </summary>
        public string TextureId => TextureReference.Id;
    }
}
