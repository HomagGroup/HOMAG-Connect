using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomagConnect.ProductionAssist.Contracts.Events.FeedbackWorkstation
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum PartProcessedAction
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        PartConfirmed,

        /// <summary>
        /// 
        /// </summary>
        PartLabeled,

    }
}
