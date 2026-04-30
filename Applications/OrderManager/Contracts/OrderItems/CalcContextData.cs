namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents contextual data used for calculations, including the set of
    /// root modules that contain other docked modules.
    /// </summary>
    public class CalcContextData
    {
        /// <summary>
        /// A list of all root modules that have other docked modules (roots)
        /// </summary>
        public CalcDockedContext[]? DockedRoots { get; set; }
    }
}
