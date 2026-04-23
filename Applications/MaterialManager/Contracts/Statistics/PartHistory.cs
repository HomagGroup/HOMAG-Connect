using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Resources = HomagConnect.Base.Contracts.Resources;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    /// <summary>
    /// Represents a history entry for a divided part together with the related board and workstation context.
    /// </summary>
    /// <example>
    /// { "dividedAt": "2025-04-09T08:15:00+00:00", "optimizationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "optimizationName": "Kitchen optimization", "boardEntityId": "B-1001", "boardCode": "P2_Gold_Craft_Oak_19.0", "boardTypeType": "Board", "comments": "Reserved for production", "workstationId": "5b53bce7-6677-4b52-a5cf-c0ab8d6a9b77", "workstationName": "Saw-01", "part": { "id": "PART-1001", "identifier": "4711-01" } }
    /// </example>
    public class PartHistory : ISupportsLocalizedSerialization, ISupportsAdditionalProperties
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
        
        /// <summary>
        /// Gets or sets the identifier of the board entity from which the part was divided.
        /// </summary>
        /// <example>B-1001</example>
        public string? BoardEntityId { get; set; }

        /// <summary>
        /// Gets or sets comments related to the source board.
        /// </summary>
        /// <example>Reserved for production</example>
        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.BoardEntityProperties_Comments))]
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the board code of the source board type.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.BoardTypeProperties_BoardCode))]
        public string? BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the source board type classification, for example board, offcut, or template.
        /// </summary>
        /// <example>Board</example>
        public BoardTypeType BoardTypeType { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the part was divided.
        /// </summary>
        /// <example>2025-04-09T08:15:00+00:00</example>
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.DividedAt))]
        public DateTimeOffset DividedAt { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the related optimization.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [Display(AutoGenerateField = false)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        /// <example>Kitchen optimization</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(OptimizationName))]
        public string OptimizationName { get; set; }

        /// <summary>
        /// Gets or sets the part details associated with this history entry.
        /// </summary>
        /// <example>{ "id": "PART-1001", "identifier": "4711-01" }</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Part))]
        public PartHistoryPart Part { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the workstation where the dividing occurred.
        /// </summary>
        /// <example>5b53bce7-6677-4b52-a5cf-c0ab8d6a9b77</example>
        [Display(AutoGenerateField = false)]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the workstation name where the dividing occurred.
        /// </summary>
        /// <example>Saw-01</example>
        [Display(ResourceType = typeof(Material.Boards.Resources), Name = nameof(Material.Boards.Resources.AllocationProperties_Workstation))]
        public string WorkstationName { get; set; }

        /// <inheritdoc />
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}