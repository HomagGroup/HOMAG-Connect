using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomagConnect.MmrMobile.Contracts
{
    public class AlertEvent : IExtensibleDataObject
    {
        public AlertEvent(IEnumerable<KeyValuePair<string, string>> localizedSource, IEnumerable<KeyValuePair<string, string>> localizedMessage)
        {
            LocalizedSource = localizedSource;
            LocalizedMessage = localizedMessage;
        }

        /// <summary>
        /// Timestamp, when the Event started
        /// </summary>
        [JsonProperty(Order = 1)]
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Timestamp for the end of the event
        /// If the event is still active, the current timestamp is returned
        /// </summary>
        [JsonProperty(Order = 2)]
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// Unique identifier for one event
        /// </summary>
        [JsonProperty(Order = 2)]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// Machine-Number, Format "x-yyy-zz-nnnn"
        /// </summary>
        [JsonProperty(Order = 3)]
        public string MachineNumber { get; set; }

        /// <summary>
        /// value = 1 - 1000
        /// e.g.
        /// 600 = pre warning on maintenance over 70%
        /// 900 = maintenance is due (100%)
        /// </summary>
        [JsonProperty(Order = 4)]
        public short Severity { get; set; }

        /// <summary>
        /// JSON-String containing the Source of the event in all available languages
        /// - german and english is mandatory for service
        /// - optional : customer language
        /// </summary>
        [JsonProperty(Order = 5)]
        public IEnumerable<KeyValuePair<string,string>> LocalizedSource { get; set; }

        /// <summary>
        /// JSON-String containing the shown error-message of the event in all available languages
        /// - german and english is mandatory for service
        /// - optional : customer language
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<KeyValuePair<string, string>> LocalizedMessage { get; set; }

        /// <summary>
        /// Category of the event
        /// </summary>
        [JsonProperty(Order = 7)]
        public AlertEventCategory? Category { get; set; }

        /// <summary>
        /// Specification of the origin of the AlertEvent. Level 1 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 8)]
        public string? SourceClass { get; set; }

        /// <summary>
        /// Detail of the origin of the AlertEvent. Level 2 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 9)]
        public string? SourceInstance { get; set; }

        /// <summary>
        /// Id of the AlertEvent. Level 3 of technical diaginfo
        /// </summary>
        [JsonProperty(Order = 10)]
        public int SourceMessageId { get; set; }


        [JsonProperty(Order = 11)]
        public AlertEventCausality? Causality { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}