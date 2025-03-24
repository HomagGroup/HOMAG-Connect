using Newtonsoft.Json;
using System;

namespace HomagConnect.MmrMobile.Contracts
{
    public class MachineInformation: MmrMachine
    {

        /// <summary>
        /// Timestamp of the taken data
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Shows the requested granularity
        /// </summary>
        public Granularity? Granularity { get; set; }
    }
}