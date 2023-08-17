using System;

namespace HomagConnect.Base
{
    /// <summary>
    /// One version
    /// </summary>
    public class VersionEntry
    {
        /// <summary>
        /// The date of this version
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Defined if this entry is deprecated and will be removed in the future
        /// </summary>
        public bool IsDeprecated { get; set; }

        /// <summary>
        /// Defines that this version is not supported anymore; the server should return "410 GONE"
        /// </summary>
        public bool Gone { get; set; }
    }
}