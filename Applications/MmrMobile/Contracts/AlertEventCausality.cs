using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// What process/part of the production is to blame for the Event
    /// </summary>
    [ResourceManager(typeof(AlertEventCausalityDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum AlertEventCausality
    {
        /// <summary>
        /// Machine is the causality />
        /// </summary>
        [Display(Description = nameof(Machine))]
        Machine,

        /// <summary>
        /// User is the causality />
        /// </summary>
        [Display(Description = nameof(User))]
        User,

        /// <summary>
        /// Supply is the causality />
        /// </summary>
        [Display(Description = nameof(Supply))]
        Supply,

        /// <summary>
        /// PlantMode is the causality />
        /// </summary>
        [Display(Description = nameof(PlantMode))]
        PlantMode,

        /// <summary>
        /// unknown
        /// </summary>
        [Display(Description = nameof(Unknown))]
        Unknown,

        /// <summary>
        /// MachineElectric is the causality />
        /// </summary>
        [Display(Description = nameof(MachineElectric))]
        MachineElectric,

        /// <summary>
        /// MachineMechanic is the causality />
        /// </summary>
        [Display(Description = nameof(MachineMechanic))]
        MachineMechanic,

        /// <summary>
        /// MachineControl is the causality />
        /// </summary>
        [Display(Description = nameof(MachineControl))]
        MachineControl,

        /// <summary>
        /// UserOperational is the causality />
        /// </summary>
        [Display(Description = nameof(UserOperational))]
        UserOperational,

        /// <summary>
        /// LackAuxilarySupplies is the causality />
        /// </summary>
        [Display(Description = nameof(UserLackAuxilarySupplies))]
        UserLackAuxilarySupplies,

        /// <summary>
        /// UserMaintenance is the causality />
        /// </summary>
        [Display(Description = nameof(UserMaintenance))]
        UserMaintenance,

        /// <summary>
        /// SupplyElectric is the causality />
        /// </summary>
        [Display(Description = nameof(SupplyElectric))]
        SupplyElectric,

        /// <summary>
        /// SupplyCompressedAir is the causality />
        /// </summary>
        [Display(Description = nameof(SupplyCompressedAir))]
        SupplyCompressedAir,

        /// <summary>
        /// SupplyAirSuction is the causality />
        /// </summary>
        [Display(Description = nameof(SupplyAirSuction))]
        SupplyAirSuction,

        /// <summary>
        /// SupplyData is the causality />
        /// </summary>
        [Display(Description = nameof(SupplyData))]
        SupplyData,

        /// <summary>
        /// SupplyOffcut is the causality />
        /// </summary>
        [Display(Description = nameof(SupplyOffcut))]
        SupplyOffcut

    }
}