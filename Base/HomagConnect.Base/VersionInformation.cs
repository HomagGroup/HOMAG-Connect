using System.Collections.Generic;
using System.Linq;

namespace HomagConnect.Base
{
    /// <summary>
    /// Contains all supported version
    /// </summary>
    public class VersionInformation
    {
        /// <summary>
        /// The list of all version
        /// </summary>
        public IList<VersionEntry> Versions { get; } = new List<VersionEntry>();

        /// <summary>
        /// Returns the latest version
        /// </summary>
        public VersionEntry Default => Versions.Last();

        /// <summary>
        /// The key which should be used in the http header
        /// </summary>
        public string HeaderKey { get; set; } = "connect-version";
    }
}