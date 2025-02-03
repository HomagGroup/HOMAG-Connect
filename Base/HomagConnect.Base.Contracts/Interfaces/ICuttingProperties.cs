namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Cutting / Nesting related properties
    /// </summary>
    public interface ICuttingProperties
    {
        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        public double? FinishLength { get; set; }

        /// <summary>
        /// Gets or sets the finish width.
        /// </summary>
        public double? FinishWidth { get; set; }

        /// <summary>
        /// Gets or sets the label layout.
        /// </summary>
        public double? LabelLayout { get; set; }

        /// <summary>
        /// Gets or set the grain matching template.
        /// </summary>
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets the 2. Cut size length.
        /// </summary>
        public double? SecondCutLength { get; set; }

        /// <summary>
        /// Gets or sets the 2. Cut size width.
        /// </summary>
        public double? SecondCutWidth { get; set; }
    }
}