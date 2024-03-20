using System;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public class BoardTypeAllocation
    {
        public string? AllocationComments { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public int? Quantity { get; set; }

        public string? Type { get; set; }

        public string? Workstation { get; set; }
    }
}