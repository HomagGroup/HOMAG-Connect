namespace HomagConnect.IntelliDivide.Samples.Requests
{
    /// <summary>
    /// Defines common settings for the samples.
    /// </summary>
    public static class CommonSampleSettings
    {
        /// <summary>
        /// Gets or sets the timeout duration for the samples.
        /// </summary>
        public static TimeSpan TimeoutDuration { get; } = TimeSpan.FromMinutes(8);

        /// <summary>
        /// Material code to be used for testing with grain lengthwise.
        /// </summary>
        public const string SampleMaterialCodeGrainLengthwise = "P2_Gold_Craft_Oak_19.0";

        /// <summary>
        /// Material code to be used for testing with grain none.
        /// </summary>
        public const string SampleMaterialCodeGrainNone = "P2_White_19.0";

        /// <summary>
        /// Nesting import template name to use in the samples.
        /// </summary>
        public const string NestingImportTemplateName = "HOMAG Connect";

        /// <summary>
        /// Cutting import template name to use in the samples.
        /// </summary>
        public const string CuttingImportTemplateName = "HOMAG Connect";

        /// <summary>
        /// Stacking optimization parameters name to use in the samples.
        /// </summary>
        public const string CuttingStackingOptimizationParameters = "HOMAG Connect stacking groups";

        /// <summary>
        /// Stacking optimization parameters name to use in the samples.
        /// </summary>
        public const string CuttingStackingZipFile = "Project_w_stacking.zip";
    }
}