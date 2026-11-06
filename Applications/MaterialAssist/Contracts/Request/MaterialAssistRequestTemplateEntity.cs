namespace HomagConnect.MaterialAssist.Contracts.Request;

/// <summary>
/// Inventory values for creating a template with its own dimensions from an existing board type.
/// </summary>
/// <remarks>
/// Supply a Quantity of one. The service enforces this rule in addition to the local property constraints.
/// </remarks>
/// <example>
/// {
/// "id": "template-001",
/// "boardCode": "BOARD-19",
/// "quantity": 1,
/// "length": 1000,
/// "width": 500
/// }
/// </example>
public class MaterialAssistRequestTemplateEntity : MaterialAssistRequestOffcutEntity { }