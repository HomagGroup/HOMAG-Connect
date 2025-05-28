using System;

using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    public class PartHistory
    {
        public DateTimeOffset DividedAt { get; set; }

        public Guid JobId { get; set; }

        public string JobName { get; set; }

        public Guid WorkstationId { get; set; }

        public string WorkstationName { get; set; }

        public Part Part { get; set; }

        public BoardType BoardType { get; set; }

        public BoardEntity BoardEntity { get; set; }


    }
}
