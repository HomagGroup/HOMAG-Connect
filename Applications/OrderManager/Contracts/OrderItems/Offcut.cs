namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Board that is a leftover piece of a larger board.
    /// </summary>
    /// <remarks>The type is fixed to <c>Offcut</c> and cannot be changed.</remarks>
    /// <example>
    /// { "type": "Offcut", "id": "O1", "material": "P2_Gold_Craft_Oak_19.0", "thickness": 19, "grain": "Lengthwise" }
    /// </example>
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
