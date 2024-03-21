using System;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public class BoardTypeInventory
    {
        public string? OrderNumber { get; set; }

        public string? Code { get; set; }

        public string? Location { get; set; }

        public string? Workstation { get; set; }

        public int Quantity { get; set; }

        public string? AdditionalCommentsBoards { get; set; }

        public DateTimeOffset? CreationDate { get; set; }
    }
}
