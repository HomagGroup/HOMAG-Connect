namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// A configuration group.
    /// </summary>
    public class ConfigurationGroup : Group
    {
        /// <inheritdoc />
        public override Type Type
        {
            get
            {
                return Type.ConfigurationGroup;
            }
        }
    }
}
