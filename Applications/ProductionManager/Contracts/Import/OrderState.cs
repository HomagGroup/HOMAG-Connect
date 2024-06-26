﻿using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// Order State Enumeration
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OrderState
    {
        /// <summary>
        ///  After a successful import of a customer order, it receives the state "New".
        /// </summary>
        New,

        /// <summary>
        /// ReadyForProduction
        /// </summary>
        ReadyForProduction,

        /// <summary>
        /// As soon as a production order of a sales order is in production.
        /// </summary>

        InProduction,

        /// <summary>
        /// As soon as all production orders of a sales order have been completed.
        /// </summary>
        Finished,
    }
}
