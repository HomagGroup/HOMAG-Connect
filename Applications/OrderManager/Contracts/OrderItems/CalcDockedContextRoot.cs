namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents the root context for a docked calculation module,
    /// containing its unique identifier and docking position information.
    /// </summary>
    public class CalcDockedContextRoot
    {
        /// <summary>
        /// Unique modules id
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Dock information
        /// Possible values: RightTop, BackTop, LeftTop, RightBottom, BackBottom, LeftBottom, LeftBackTop, RightBackTop, LeftBackBottom, RightBackBottom
        /// </summary>
        public string DockingVector { get; set; } = null!;
    }
}
