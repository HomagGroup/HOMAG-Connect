namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// The type of the board to use in the optimzation.
    /// </summary>
    public enum OptimizationBoardType
    {
        /// <summary>
        /// The board is a whole board.
        /// </summary>
        Board,

        /// <summary>
        /// The board is an offcut. Discounts on costs will get applied.
        /// </summary>
        Offcut
    }
}