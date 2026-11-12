using System;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// Represents one edgeband inventory entry, including its associated edgeband type, stock quantity, and storage location.
    /// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
    /// </summary>
    /// <example>
    /// {
    ///   "comments": "Finished coil",
    ///   "creationDate": "2026-11-12T10:00:00+02:00",
    ///   "currentThickness": 2.0,
    ///   "edgebandType": { "unitSystem": "Metric", "edgebandCode": "ABS_Hunton_Oak_dark_2.00_23.0_HM", "height": 23.0, "thickness": 2.0 },
    ///   "id": "edge-001",
    ///   "length": 50.0,
    ///   "location": { "locationId": "LOC-01-02-03" },
    ///   "managementType": "Single",
    ///   "quantity": 1,
    ///   "totalLength": 50.0
    /// }
    /// </example>
    public class EdgebandEntity
    {
        /// <summary>
        /// Notes associated with this inventory entry.
        /// </summary>
        /// <example>Finished coil</example>
        public string? Comments { get; set; }

        /// <summary>
        /// Date and time when this inventory entry was created, when known.
        /// </summary>
        /// <example>2026-11-12T10:00:00+02:00</example>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Thickness recorded for this edgeband inventory entry.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>2.0</example>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2,3)]
        public double CurrentThickness { get; set; }

        /// <summary>
        /// Edgeband type associated with this inventory entry.
        /// </summary>
        /// <example>{ "edgebandCode": "ABS_Hunton_Oak_dark_2.00_23.0_HM", "height": 23.0, "thickness": 2.0 }</example>
        public EdgebandType EdgebandType { get; set; }

        /// <summary>
        /// Unique identifier for this inventory entry.
        /// </summary>
        /// <example>edge-001</example>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Length associated with this edgeband inventory entry.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
        /// </summary>
        /// <example>50.0</example>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double Length { get; set; }

        /// <summary>
        /// Storage location associated with this inventory entry.
        /// </summary>
        /// <example>{ "locationId": "LOC-01-02-03" }</example>
        public StorageLocation Location { get; set; }

        /// <summary>
        /// How this inventory entry is tracked: individually, as a stack, or as goods in stock.
        /// </summary>
        /// <example>Single</example>
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Number of edgeband items represented by this inventory entry.
        /// </summary>
        /// <example>1</example>
        public int Quantity { get; set; }

        /// <summary>
        /// Total edgeband length represented by this inventory entry.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
        /// </summary>
        /// <example>50.0</example>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double TotalLength { get; set; }
    }
}