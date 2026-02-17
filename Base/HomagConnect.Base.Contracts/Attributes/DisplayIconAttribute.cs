namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Declares an icon for an enum member.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DisplayIconAttribute : Attribute
    {
        /// <summary>
        /// Creates the attribute from a URI string.
        /// </summary>
        /// <param name="uri">Absolute or relative URI string identifying the icon.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="uri"/> is null or whitespace.</exception>
        /// <exception cref="UriFormatException">Thrown when <paramref name="uri"/> is not a valid URI.</exception>
        public DisplayIconAttribute(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException(nameof(uri));

            Icon = new Uri(uri, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Creates the attribute from a <see cref="Uri"/> instance.
        /// </summary>
        /// <param name="iconUri">Icon URI.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="iconUri"/> is null.</exception>
        public DisplayIconAttribute(Uri iconUri)
        {
            Icon = iconUri ?? throw new ArgumentNullException(nameof(iconUri));
        }

        /// <summary>
        /// Gets the icon URI.
        /// </summary>
        public Uri Icon { get; }
    }
}