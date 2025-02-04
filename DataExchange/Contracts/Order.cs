using System.Diagnostics.CodeAnalysis;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange order definition.
    /// </summary>
    [SuppressMessage("Security", "S2094:Suppressing rule S2094", Justification = "This is a known issue and is being addressed.")]
    public class Order : Entity
    {
    }
}
