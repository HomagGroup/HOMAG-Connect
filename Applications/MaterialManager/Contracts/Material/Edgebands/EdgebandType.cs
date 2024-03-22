using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandType
    {
        [Key]
        public string? Code { get; set; }

        public string? MaterialCategory { get; set; }

        public string? GluingCategory { get; set; }

        public double? Lasertec { get; set; } // TODO: Bool?

        public double? Airtec { get; set; }  // TODO: Bool?

        public double? ProtectionFilmThickness { get; set; }

        public double? FunctionLayerThickness { get; set; }

        public string? MacroName { get; set; }

        public double? Height { get; set; }

        public double? Thickness { get; set; }

        public double? Length { get; set; }

        public string? ManufacturerName { get; set; }

        public string? ProductName { get; set; }

        public string? DecorName { get; set; }

        public string? DecorCode { get; set; }

        public string? DecorEmbossingCode { get; set; }

        public string? ArticleNumber { get; set; }

        public double? Costs { get; set; }

        public double? MinimumAmountAvailable { get; set; } // TODO: Align with <see cref="BoardType.TotalQuantityAvailableWarningLimit"/>.

        public double? MinimumLengthAvailable { get; set; }

        public string? AdditionalNotes { get; set; } // TODO: Comments?

        public double? TotalAmount { get; set; } // TODO: Total Quantity?

        public double? TotalLength { get; set; }

        public bool? InsufficientInventory { get; set; } // TODO: Align with <see cref="BoardType.TotalQuantityAvailableWarningLimitReached"/>. InsufficientInventory might be better.

        public DateTimeOffset? LastUsedDate { get; set; }

        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
    }
}