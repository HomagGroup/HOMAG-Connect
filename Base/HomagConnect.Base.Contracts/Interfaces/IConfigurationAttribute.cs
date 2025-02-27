namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Configuration attribute.
    /// </summary>
    public interface IConfigurationAttribute
    {
        /// <summary>
        /// The name of the attribute.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Defines if the attribute is an input value or an output value.
        /// </summary>
        bool? IsInput { get; set; }

        /// <summary>
        /// The value of the attribute.
        /// </summary>
        object Value { get; set; }
    }
}
