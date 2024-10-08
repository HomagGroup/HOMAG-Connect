﻿using System;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// Describe an edgeband entity.
    /// </summary>
    public class EdgebandEntity
    {
        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2,3)]
        public double CurrentThickness { get; set; }

        /// <summary>
        /// Gets or sets the edgeband type.
        /// </summary>
        public EdgebandType EdgebandType { get; set; }

        /// <summary>
        /// Gets or sets the id (#)
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial:
        /// ft).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or set the location.
        /// </summary>
        public StorageLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the management type (single, stack, goods in stock).
        /// </summary>
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the edgeband entity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the total length of the edgeband. The unit depends on the settings of the subscription (metric: m,
        /// imperial:
        /// ft).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double TotalLength { get; set; }
    }
}