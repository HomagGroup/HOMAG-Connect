﻿using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the overview figures for material.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOverviewFiguresMaterial : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the total number of offcuts.
        /// </summary>
        [JsonProperty(Order = 10)]
        public int OffcutsTotal { get; set; }

        /// <summary>
        /// Gets offcuts produced.
        /// </summary>
        [JsonProperty(Order = 11)]
        public int OffcutsProduced { get; set; }

        /// <summary>
        /// Gets offcuts required.
        /// </summary>
        [JsonProperty(Order = 12)]
        public int OffcutsRequired { get; set; }

        /// <summary>
        /// Gets the waste in percent.
        /// </summary>
        [JsonProperty(Order = 1)]
        public double Waste { get; set; }

        /// <summary>
        /// Gets the waste + offcuts in percent.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double WastePlusOffcuts { get; set; }

        /// <summary>
        /// Gets the number of whole boards.
        /// </summary>
        [JsonProperty(Order = 3)]
        public int WholeBoards { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}