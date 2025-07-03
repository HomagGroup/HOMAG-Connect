using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Book height mode
    /// </summary>
    [ResourceManager(typeof(BookHeightModeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum BookHeightMode
    {
        /// <summary>
        /// Maximum saw blade projection deduction
        /// </summary>
        [Obsolete("This mode is obsolete and was replaced with MaximumBookHeightOfMachine.")]
        MaximumSawBladeProjectionDeduction,

        /// <summary>
        /// Maximum book height of the machine
        /// </summary>
        [Display(Description = "Maximum Book Height Of Machine")] 
        MaximumBookHeightOfMachine,

        /// <summary>
        /// Single board
        /// </summary>
        [Display(Description = "Single Board")] 
        SingleBoard,
        
        /// <summary>
        /// Specific value can be set
        /// </summary>
        [Obsolete("This mode is obsolete and was replaced with LimitedBookHeight.")]
        SpecificValue,

        /// <summary>
        /// Limit of the book height can be set
        /// </summary>
        [Display(Description = "Limited Book Height")] 
        LimitedBookHeight
    }
}