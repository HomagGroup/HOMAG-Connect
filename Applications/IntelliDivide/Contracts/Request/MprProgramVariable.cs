using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a variable stored in nesting optimizing parameter sets
    /// </summary>
    [DebuggerDisplay("{Name}={Value} ({Comment})")]
    public class MprProgramVariable
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }
    }
}