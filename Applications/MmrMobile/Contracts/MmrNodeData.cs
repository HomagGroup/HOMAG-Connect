namespace HomagConnect.MmrMobile.Contracts
{

    /// <summary>
    /// list of nodeData
    /// returned by the api
    /// </summary>
    public class MmrNodeData
    {
        /// <summary>
        /// machinenumber of the nodedata
        /// </summary>
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        public string? MachineName { get; set; }

        /// <summary>
        /// List of nodes
        /// </summary>
        public MmrNode[]? Nodes { get; set; }
    }
}
