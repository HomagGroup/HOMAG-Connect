using System;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Book height mode
    /// </summary>
    public enum BookHeightMode
    {
        /// <summary>
        /// Maximum saw blade projection deduction
        /// </summary>
        [Obsolete]
        MaximumSawBladeProjectionDeduction,

        /// <summary>
        /// Maximum book height
        /// </summary>
        MaximumBookHeight,

        /// <summary>
        /// Single board
        /// </summary>
        SingleBoard,
        
        /// <summary>
        /// Specific value can be set
        /// </summary>
        SpecificValue
    }
}