using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// The workstation group (Used for shared work load over workplaces)
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum WorkstationGroup
    { 
        /// <summary>
        /// None - this is the default
        /// </summary>
        None,

        /// <summary>
        /// Dividing
        /// </summary>
        Dividing,

        /// <summary>
        /// Edgebanding
        /// </summary>
        Edgebanding,

        /// <summary>
        /// CNC
        /// </summary>
        CNC,

        /// <summary>
        /// Laminating
        /// </summary>
        Laminating,

        /// <summary>
        /// Moulding
        /// </summary>
        Moulding,

        /// <summary>
        /// Sanding
        /// </summary>
        Sanding,

        /// <summary>
        /// Roller Coating
        /// </summary>
        RollerCoating,

        /// <summary>
        /// Spray Coating
        /// </summary>
        SprayCoating,

        /// <summary>
        /// Sorting
        /// </summary>
        Sorting,

        /// <summary>
        /// Mount Fittings
        /// </summary>
        MountFittings,

        /// <summary>
        /// Preassembly
        /// </summary>
        Preassembly,

        /// <summary>
        /// Assembly
        /// </summary>
        Assembly,

        /// <summary>
        /// Packaging
        /// </summary>
        Packaging,

        /// <summary>
        /// Shipping
        /// </summary>
        Shipping,

        /// <summary>
        /// Sorting After Cutting
        /// </summary>
        SortingAfterCutting,

        /// <summary>
        /// Rework
        /// </summary>
        Rework,

        /// <summary>
        /// Boards
        /// </summary>
        Boards,
    }
}
