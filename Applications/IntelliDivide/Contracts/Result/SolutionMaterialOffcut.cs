#nullable enable

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the offcuts used in the solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcut : SolutionMaterialBoardBase
    {
        /// <inheritdoc />
        public override BoardTypeType Type { get; set; } = BoardTypeType.Offcut;
    }
}