using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class AlertEvent : IExtensibleDataObject, ISupportsLocalizedSerialization
    {
        public AlertEvent(Dictionary<string, string> localizedSource, Dictionary<string, string> localizedMessage)
        {
            LocalizedSource = localizedSource;
            LocalizedMessage = localizedMessage;
        }

        /// <summary>
        /// Timestamp, when the Event started
        /// </summary>
        [JsonProperty(Order = 1)]
        [Display(Name = nameof(MmrPropertyDisplayNames.StartTime))]
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Timestamp for the end of the event
        /// If the event is still active, the current timestamp is returned
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(Name = nameof(MmrPropertyDisplayNames.EndTime))]
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// Unique identifier for one event
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(Name = nameof(MmrPropertyDisplayNames.InstanceId))]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// Machine-Number, Format "x-yyy-zz-nnnn"
        /// </summary>
        [JsonProperty(Order = 3)]
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineNumber))]
        public string MachineNumber { get; set; }

        /// <summary>
        /// value = 1 - 1000
        /// e.g.
        /// 600 = pre warning on maintenance over 70%
        /// 900 = maintenance is due (100%)
        /// </summary>
        [JsonProperty(Order = 4)]
        [Display(Name = nameof(MmrPropertyDisplayNames.Severity))]
        public short Severity { get; set; }

        /// <summary>
        /// JSON-String containing the Source of the event in all available languages
        /// - german and english is mandatory for service
        /// - optional : customer language
        /// </summary>
        [JsonProperty(Order = 5)]
        [Display(Name = nameof(MmrPropertyDisplayNames.LocalizedSource))]
        public Dictionary<string, string> LocalizedSource { get; set; }

        /// <summary>
        /// JSON-String containing the shown error-message of the event in all available languages
        /// - german and english is mandatory for service
        /// - optional : customer language
        /// </summary>
        [JsonProperty(Order = 6)]
        [Display(Name = nameof(MmrPropertyDisplayNames.LocalizedMessage))]
        public Dictionary<string, string> LocalizedMessage { get; set; }

        /// <summary>
        /// Category of the event
        /// </summary>
        [JsonProperty(Order = 7)]
        [Display(Name = nameof(MmrPropertyDisplayNames.Category))]
        public AlertEventCategory? Category { get; set; }

        /// <summary>
        /// Specification of the origin of the AlertEvent. Level 1 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 8)]
        [Display(Name = nameof(MmrPropertyDisplayNames.SourceClass))]
        public string? SourceClass { get; set; }

        /// <summary>
        /// Detail of the origin of the AlertEvent. Level 2 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 9)]
        [Display(Name = nameof(MmrPropertyDisplayNames.SourceInstance))]
        public string? SourceInstance { get; set; }

        /// <summary>
        /// Id of the AlertEvent. Level 3 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 10)]
        [Display(Name = nameof(MmrPropertyDisplayNames.SourceMessageId))]
        public int SourceMessageId { get; set; }


        [JsonProperty(Order = 11)]
        [Display(Name = nameof(MmrPropertyDisplayNames.Causality))]
        public AlertEventCausality? Causality { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}