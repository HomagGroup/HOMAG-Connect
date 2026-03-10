using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents an optimization request that uses a structured project zip file. The zip file contains
    /// parts, boards, and optional MPR programs. It is submitted alongside this request.
    /// Machine, parameters, boards, and action are inherited from <see cref="OptimizationRequestBase" />.
    /// </summary>
    /// <example>
    /// {
    ///   "machine": "productionAssist Cutting",
    ///   "parameters": "Default",
    ///   "action": "ImportOnly"
    /// }
    /// </example>
    [DebuggerDisplay("Action={Action}")]
    public class OptimizationRequestUsingProject : OptimizationRequestBase
    {

    }
}