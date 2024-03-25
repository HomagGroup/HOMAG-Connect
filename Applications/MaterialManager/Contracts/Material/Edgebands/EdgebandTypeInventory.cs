using System;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandTypeInventory
    {
        public string? Code { get; set; }

        public string? Location { get; set; }

        public string? Workstation { get; set; }

        public double? Length { get; set; }

        public double? TotalLength { get; set; }

        public double? CurrentThickness { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public double? Quantity { get; set; }

        public string? AdditionalCommentsEdgeband { get; set; }
    }
}