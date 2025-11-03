using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents the type of processed item, such as a part or a board.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProcessedItemType
    {
        /// <summary>
        /// Processed item is unknown or not specified.
        /// </summary>
        Unknown,

        /// <summary>
        /// Processed item is a part.
        /// </summary>
        Part,

        /// <summary>
        /// Processed item is a grouping of parts called Position.
        /// </summary>
        Position,

        /// <summary>
        /// Processed item is a grouping of parts called AssemblyGroup.
        /// </summary>
        AssemblyGroup,

        /// <summary>
        /// Processed item is a board or offcut.
        /// </summary>
        Board
    }
}