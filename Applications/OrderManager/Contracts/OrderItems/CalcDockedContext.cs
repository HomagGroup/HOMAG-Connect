namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents the docking context of a calculation element, describing its own docking position
    /// and the collection of docked root contexts associated with it.
    /// </summary>
    public class CalcDockedContext
    {
        /// <summary>
        /// Dock information
        /// Possible values: RightTop, BackTop, LeftTop, RightBottom, BackBottom, LeftBottom, LeftBackTop, RightBackTop, LeftBackBottom, RightBackBottom
        /// </summary>
        public string OwnDockingVector { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public CalcDockedContextRoot[] DockedRoots { get; set; } = null!;
    }
}
