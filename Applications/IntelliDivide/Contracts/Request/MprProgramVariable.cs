using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a variable passed to the MPR program referenced by <see cref="OptimizationRequestPart.MprFileName" />.
    /// Used to parameterize nesting contours (e.g. part dimensions).
    /// </summary>
    /// <example>{ "name": "L", "value": "980", "comment": "Length" }</example>
    [DebuggerDisplay("{Name}={Value} ({Comment})")]
    public class MprProgramVariable
    {
        /// <summary>
        /// Gets or sets the variable name as defined in the MPR program.
        /// </summary>
        /// <example>L</example>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value assigned to the variable.
        /// </summary>
        /// <example>980</example>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets an optional comment describing the purpose of the variable.
        /// </summary>
        /// <example>Length</example>
        public string Comment { get; set; }
    }
}