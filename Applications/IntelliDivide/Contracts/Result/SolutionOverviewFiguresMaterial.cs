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
        /// Gets large offcuts produced.
        /// </summary>
        [JsonProperty(Order = 12)]
        public int LargeOffcutsProduced { get; set; }

        /// <summary>
        /// Gets large offcuts required.
        /// </summary>
        [JsonProperty(Order = 11)]
        public int LargeOffcutsRequired { get; set; }

        /// <summary>
        /// Gets large offcuts in total.
        /// </summary>
        [JsonProperty(Order = 10)]
        public int LargeOffcutsTotal { get; set; }

        /// <summary>
        /// Gets small offcuts produced.
        /// </summary>
        [JsonProperty(Order = 22)]
        public int SmallOffcutsProduced { get; set; }

        /// <summary>
        /// Gets small offcuts required.
        /// </summary>
        [JsonProperty(Order = 21)]
        public int SmallOffcutsRequired { get; set; }

        /// <summary>
        /// Gets small offcuts in total.
        /// </summary>
        [JsonProperty(Order = 20)]
        public int SmallOffcutsTotal { get; set; }

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