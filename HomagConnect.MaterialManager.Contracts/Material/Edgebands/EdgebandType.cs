using System;
using System.Collections.Generic;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandType
    {
        public string Code { get; set; }

        public string MaterialCategory { get; set; }

        public string GluingCategory { get; set; }

        public double? Lasertec { get; set; }

        public double? Airtec { get; set; }

        public double? ProtectionFilmThickness { get; set; }

        public double? FunctionLayerThickness { get; set; }

        public string MacroName { get; set; }

        public double? Height { get; set; }

        public double? Thickness { get; set; }

        public double? Length { get; set; }

        public string ManufacturerName { get; set; }

        public string ProductName { get; set; }

        public string DecorName { get; set; }

        public string DecorCode { get; set; }

        public string DecorEmbossingCode { get; set; }

        public string ArticleNumber { get; set; }

        public double? Costs { get; set; }

        public double? MinimumAmountAvailable { get; set; }

        public double? MinimumLengthAvailable { get; set; }

        public string AdditionalNotes { get; set; }

        public double? TotalAmount { get; set; }

        public double? TotalLength { get; set; }

        public bool? InsufficientInventory { get; set; }

        public DateTimeOffset? LastUsedDate { get; set; }

        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();

    }
}
