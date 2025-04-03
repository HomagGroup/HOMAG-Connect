namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// A configuration group.
    /// </summary>
    public class ConfigurationGroup : Base
    {
        /// <inheritdoc />
        public override Type Type
        {
            get
            {
                return Type.ConfigurationGroup;
            }
            set
            {
                // Ignore
            }
        }

        /// <summary>
        /// Gets or sets the contour information for this group
        /// which is the surrounding contour of the articles in this group.
        /// </summary>
        public string? ContourInformation { get; set; }
    }
}
