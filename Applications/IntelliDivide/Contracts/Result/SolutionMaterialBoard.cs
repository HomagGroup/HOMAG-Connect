using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the material used in the solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialBoard : SolutionMaterialBoardBase
    {
        /// <inheritdoc />
        public override BoardTypeType Type { get; set; } = BoardTypeType.Board;
    }
}