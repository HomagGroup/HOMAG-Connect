using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework reason
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(ReworkReasonDisplayNames))]
    public enum ReworkReason
    {
        /// <summary>
        /// Other
        /// </summary>
        Other,

        /// <summary>
        /// Tearouts
        /// </summary>
        Tearouts,

        /// <summary>
        /// Burn marks
        /// </summary>
        BurnMarks,

        /// <summary>
        /// Scratches
        /// </summary>
        Scratches,

        /// <summary>
        /// Damaged board
        /// </summary>
        DamagedBoard,

        /// <summary>
        /// Wave cut
        /// </summary>
        WaveCut,

        /// <summary>
        /// Too little glue
        /// </summary>
        TooLittleGlue,

        /// <summary>
        /// Finishing not OK
        /// </summary>
        FinishingNotOK,

        /// <summary>
        /// Chatter marks
        /// </summary>
        ChatterMarks,

        /// <summary>
        /// Wrong edge material
        /// </summary>
        WrongEdgeMaterial,

        /// <summary>
        /// Wrong CNC program/ drilling pattern
        /// </summary>
        WrongCNCProgramOrDrillingPattern,

        /// <summary>
        /// Drilling pattern mirrored
        /// </summary>
        DrillingPatternMirrored,

        /// <summary>
        /// Tool measured incorrectly
        /// </summary>
        ToolMeasuredIncorrectly,

        /// <summary>
        /// Clamping not OK
        /// </summary>
        ClampingNotOK,

        /// <summary>
        /// Damages
        /// </summary>
        Damages,

        /// <summary>
        /// Inclusions
        /// </summary>
        Inclusions,

        /// <summary>
        /// Pollution
        /// </summary>
        Pollution,

        /// <summary>
        /// Dimensions not OK
        /// </summary>
        DimensionsNotOK,

        /// <summary>
        /// Part out of tolerance
        /// </summary>
        PartOutOfTolerance,

        /// <summary>
        /// Part damaged
        /// </summary>
        PartDamaged,

        /// <summary>
        /// Part lost
        /// </summary>
        PartLost,

        /// <summary>
        /// Wrong material
        /// </summary>
        WrongMaterial
    }
}