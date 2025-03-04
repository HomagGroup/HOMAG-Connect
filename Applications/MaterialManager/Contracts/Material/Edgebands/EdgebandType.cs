using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// A edgeband type.
    /// </summary>
    [DebuggerDisplay("{EdgebandCode}")]
    public class EdgebandType : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the timestamp when board type has been used last.
        /// </summary>
        public DateTimeOffset? LastUsed { get; set; }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region EdgebandType

        /// <summary>
        /// Gets or sets the edgeband code
        /// </summary>
        [Key]
        public string? EdgebandCode { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Height { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial:
        /// ft).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double? DefaultLength { get; set; }

        /// <summary>
        /// Gets or sets the costs of the edgeband. The unit depends on the settings of the subscription.
        /// </summary>
        public double? Costs { get; set; }

        #endregion

        #region Material

        /// <summary>
        /// Gets or sets the material category.
        /// </summary>
        public EdgebandMaterialCategory? MaterialCategory { get; set; }

        /// <summary>
        /// Gets or sets the gluing category.
        /// </summary>
        public GluingCategory? GluingCategory { get; set; }

        /// <summary>
        /// Gets or sets the lasertec (J/cm^2).
        /// </summary>
        public double? Lasertec { get; set; } // TODO: Bool? No bool, here the pressure (J/cm^2) for the values is set

        /// <summary>
        /// Gets or sets the airtec. The unit depends on the settings of the subscription (metric: bar, imperial: psi).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Bar)]
        public double? Airtec { get; set; } // TODO: Bool? No bool, here the pressure (bar) for the values is set

        /// <summary>
        /// Gets or sets the protection film thickness.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
        public double? ProtectionFilmThickness { get; set; }

        /// <summary>
        /// Gets or sets the protection layer thickness.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
        public double? FunctionLayerThickness { get; set; }

        /// <summary>
        /// Gets or sets the macro name.
        /// </summary>
        public string? MacroName { get; set; }

        #endregion

        #region Manufacturer

        /// <summary>
        /// Gets or sets the name of the manufacturer.
        /// </summary>
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        public string? DecorName { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor embossing code.
        /// </summary>
        public string? DecorEmbossingCode { get; set; }

        /// <summary>
        /// Gets or sets the article number.
        /// </summary>
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the gtin.
        /// </summary>
        public string? Gtin { get; set; }

        /// <summary>
        /// Gets or sets the id from an external system.
        /// </summary>
        public string? ExternalId { get; set; }

        #endregion

        #region Inventory

        /// <summary>
        /// Gets or sets the total quantity available warning limit.
        /// </summary>
        public int? TotalQuantityAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets the total length available warning limit. The unit depends on the settings of the subscription (metric: m,
        /// imperial: ft).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double? TotalLengthAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which are available in the inventory.
        /// </summary>
        public int? TotalQuantityAvailable { get; set; }

        /// <summary>
        /// Gets or sets the total length of boards of this type which are available in the inventory.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double? TotalLengthAvailable { get; set; }

        /// <summary>
        /// Gets or sets an indication whether the <see cref="TotalQuantityAvailable" /> or <see cref="TotalLengthAvailable" /> is
        /// below the defined limit
        /// <see cref="TotalQuantityAvailableWarningLimit" />.
        /// <see cref="TotalLengthAvailableWarningLimit" />.
        /// </summary>
        public bool? InsufficientInventory { get; set; }

        #endregion

        #region Additional data

        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or set the thumbnail uri.
        /// </summary>
        public Uri? Thumbnail { get; set; }

        #endregion
    }
}