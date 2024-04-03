namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// repsonse for requesting all active nodes of a machine
    /// </summary>
    public class MmrNodeList
    {
        /// <summary>
        /// Number of the machine in Homag-Format
        /// </summary>
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        public string? MachineName { get; set; } 

        /// <summary>
        /// List of strings / nodenames
        /// </summary>
        public string[]? Nodes { get; set; } 
    }
}