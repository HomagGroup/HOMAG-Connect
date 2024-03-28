namespace HomagConnect.Applications.MmrMobile.Samples
{
    /// <summary>
    /// 
    /// </summary>
    public static class StateGroupCodes
    {
        /// <summary>
        /// 
        /// </summary>
        public static string MainUsage
        {
            get => "s_mainusage";
        }
        /// <summary>
        /// 
        /// </summary>
        public static string MainUsageExeedance
        {
            get => "s_mainusage_exeeded";
        }

        public static string SecondaryUsage
        {
            get => "s_secondaryusage";
        }

        public static string SecondaryUsageExeedance
        {
            get => "s_secondaryusage_exeedance";
        }

        public static string IdleTime
        {
            get => "s_idletime";
        }

        public static string Malfunction
        {
            get => "s_malfunction";
        }

        public static string Maintenance
        {
            get => "s_maintenance";
        }

        public static string OutOfOperation
        {
            get => "s_outofoperation";
        }
    }
}