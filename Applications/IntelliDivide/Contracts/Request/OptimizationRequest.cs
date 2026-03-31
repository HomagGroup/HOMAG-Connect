using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request;

/// <summary>
/// Represents an optimization request built using the object model. Combines a named set of
/// <see cref="Parts" /> with the machine, parameters, boards, and action inherited from <see cref="OptimizationRequestBase" />.
/// </summary>
/// <example>
/// {
///   "name": "Order_20260120",
///   "machine": "productionAssist Cutting",
///   "parameters": "Default",
///   "action": "Optimize",
///   "unitSystem": "Metric",
///   "boards": [  // optional, materialManager data is used if not provided
///     {
///       "materialCode": "P2_White_19.0",
///       "boardCode": "P2_White_19.0_2800_2070",
///       "length": 2800,
///       "width": 2070,
///       "thickness": 19.0,
///       "grain": "None",
///       "quantity": 20,
///       "costs": 10.5
///     }
///   ],
///   "parts": [
///     {
///       "description": "Left Side",
///       "materialCode": "P2_White_19.0",
///       "length": 600,
///       "width": 300,
///       "thickness": 19.0,
///       "quantity": 5,
///       "grain": "None"
///     },
///     {
///       "description": "Right Side",
///       "materialCode": "P2_White_19.0",
///       "length": 600,
///       "width": 300,
///       "thickness": 19.0,
///       "quantity": 5,
///       "grain": "None"
///     }
///   ]
/// }
/// </example>
[DebuggerDisplay("Name={Name}, Action={Action}")]
public class OptimizationRequest : OptimizationRequestBase
{
    private const int _NameMaxLength = 100;

    /// <summary>
    /// Gets or sets the name of the optimization. Must be between 3 and 100 characters.
    /// If not provided, the name is generated automatically.
    /// </summary>
    /// <example>Order_20250120</example>
    [Required]
    [StringLength(_NameMaxLength, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the parts to include in the optimization. At least one part is required.
    /// </summary>
    /// <example>
    /// [
    ///   { "description": "Part A", "materialCode": "P2_White_19.0", "length": 600, "width": 300, "quantity": 10 }
    /// ]
    /// </example>
    [MinLength(1)]
    public Collection<OptimizationRequestPart> Parts { get; set; } = [];
}