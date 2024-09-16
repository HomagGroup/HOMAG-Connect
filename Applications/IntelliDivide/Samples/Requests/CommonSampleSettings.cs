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
        public static TimeSpan TimeoutDuration = TimeSpan.FromMinutes(8);

        /// <summary>
        /// Material code to be used for testing with grain lengthwise.
        /// </summary>
        public const string SampleMaterialCodeGrainLengthwise = "P2_Gold_Craft_Oak_19.0";

        /// <summary>
        /// Material code to be used for testing with grain none.
        /// </summary>
        public const string SampleMaterialCodeGrainNone = "P2_White_19.0";

    }
}