namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Attribute to define the resource manager for an enumeration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class ResourceManagerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManagerAttribute" /> class.
        /// </summary>
        public ResourceManagerAttribute(Type resourceManagerType)
        {
            ResourceManagerType = resourceManagerType;
        }

        /// <summary>
        /// Gets the type of the resource manager.
        /// </summary>
        public Type ResourceManagerType { get; }
    }
}