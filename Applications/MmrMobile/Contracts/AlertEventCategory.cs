using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Grouping to categorize an AlertEvent
    /// </summary>
    [ResourceManager(typeof(AlertEventCategoryDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum AlertEventCategory
    {
        /// <summary>
        /// Invalid category
        /// </summary>
        [Display(Description = nameof(None))]
        None = 0,

        /// <summary>
        /// Report category
        /// </summary>
        [Display(Description = nameof(Report))]
        Report = 2,

        /// <summary>
        /// Information category
        /// </summary>
        [Display(Description = nameof(Information))]
        Information = 3,

        /// <summary>
        /// Warning category
        /// </summary>
        [Display(Description = nameof(Warning))]
        Warning = 4,

        /// <summary>
        /// Error category
        /// </summary>
        [Display(Description = nameof(Fault))]
        Fault = 5,

        /// <summary>
        /// Alarm category
        /// </summary>
        [Display(Description = nameof(Alarm))]
        Alarm = 6,

        /// <summary>
        /// Danger category
        /// </summary>
        [Display(Description = nameof(Danger))]
        Danger = 7,

        /// <summary>
        /// UserInstruction category
        /// </summary>
        [Display(Description = nameof(UserInstruction))]
        UserInstruction = 8
    }
}