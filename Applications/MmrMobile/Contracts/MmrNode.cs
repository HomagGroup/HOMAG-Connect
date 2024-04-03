using System;

namespace HomagConnect.MmrMobile.Contracts
{
    public class MmrNode
    {
        /// <summary>
        /// Timestamp, when the value did change
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of the Node
        /// </summary>
        public string? Node { get; set; }

        /// <summary>
        /// Value of the node
        /// </summary>
        public string? Value { get; set; }
    }
}
