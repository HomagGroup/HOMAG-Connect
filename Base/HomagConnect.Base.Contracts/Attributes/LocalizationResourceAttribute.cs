namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Declares the resource type used to resolve localized property display names for all properties of the decorated class.
    /// </summary>
    /// <remarks>
    /// Apply this attribute at the class level to avoid repeating <c>ResourceType</c> on every <c>DisplayAttribute</c>.
    /// Properties can then use <c>[Display(Name = nameof(Property))]</c> without specifying <c>ResourceType</c>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class LocalizationResourceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationResourceAttribute" /> class.
        /// </summary>
        public LocalizationResourceAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets the resource type used to resolve localized property display names.
        /// </summary>
        public Type ResourceType { get; }
    }
}
