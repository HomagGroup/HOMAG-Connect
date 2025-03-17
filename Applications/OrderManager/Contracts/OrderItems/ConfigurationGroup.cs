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
    }
}
