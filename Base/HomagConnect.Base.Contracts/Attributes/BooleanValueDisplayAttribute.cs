namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Declares a localized display value for a boolean property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class BooleanValueDisplayAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanValueDisplayAttribute" /> class.
        /// </summary>
        /// <param name="value">The boolean value represented by this display mapping.</param>
        /// <param name="resourceType">The resource class that contains the localized display value.</param>
        /// <param name="resourceName">The resource key for the localized display value.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="resourceType" /> or <paramref name="resourceName" /> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="resourceName" /> is empty or whitespace.</exception>
        public BooleanValueDisplayAttribute(bool value, Type resourceType, string resourceName)
        {
            Value = value;
            ResourceType = resourceType ?? throw new ArgumentNullException(nameof(resourceType));

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("Resource name must not be null or whitespace.", nameof(resourceName));
            }

            ResourceName = resourceName;
        }

        /// <summary>
        /// Gets the boolean value represented by this display mapping.
        /// </summary>
        public bool Value { get; }

        /// <summary>
        /// Gets the resource class that contains the localized display value.
        /// </summary>
        public Type ResourceType { get; }

        /// <summary>
        /// Gets the resource key for the localized display value.
        /// </summary>
        public string ResourceName { get; }
    }
}
