namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents a board that is classified as an offcut, typically a leftover or remnant piece from a larger board.
    /// </summary>
    /// <remarks>Use this type to model offcut boards within inventory or material tracking systems. The type
    /// is fixed to indicate an offcut and cannot be changed.</remarks>
    public class Offcut : Board
    {
        #region ComponentBase Members

        /// <inheritdoc cref="Base" />
        public override Type Type
        {
            get
            {
                return Type.Offcut;
            }
            set
            {
                // Ignore
            }
        }

        #endregion
    }
}
