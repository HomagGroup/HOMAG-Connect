using HomagConnect.ProductionAssist.Contracts.Feedback.Enumerations;

namespace HomagConnect.ProductionAssist.Contracts.Feedback
{
    /// <summary>
    /// Feedback Workstation
    /// </summary>
    public class FeedbackWorkstation
    {
        /// <summary>
        /// Gets or sets the Workstation Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the tapio machine ID
        /// </summary>
        public string? AssignedTapioMachineId { get; set; }

        /// <summary>
        /// Gets or sets the tapio machine ID
        /// </summary>
        public WorkstationGroup Type { get; set; } = WorkstationGroup.None;

        /// <summary>
        /// Gets or sets the tapio machine ID
        /// </summary>
        public WorkstationCategory Category { get; set; } = WorkstationCategory.None;
    }
}
