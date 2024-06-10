namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// What process/part of the production is to blame for the Event
    /// </summary>
    public enum AlertEventCausality
    {
        /// <summary>
        /// Machine is the causality />
        /// </summary>
        CausalityMachine,

        /// <summary>
        /// User is the causality />
        /// </summary>
        CausalityUser,

        /// <summary>
        /// Supply is the causality />
        /// </summary>
        CausalitySupply,

        /// <summary>
        /// PlantMode is the causality />
        /// </summary>
        CausalityPlantMode,

        /// <summary>
        /// The causality /> is unknown
        /// </summary>
        CausalityUnknown,

        /// <summary>
        /// MachineElectric is the causality />
        /// </summary>
        CausalityMachineElectric,

        /// <summary>
        /// MachineMechanic is the causality />
        /// </summary>
        CausalityMachineMechanic,

        /// <summary>
        /// MachineControl is the causality />
        /// </summary>
        CausalityMachineControl,

        /// <summary>
        /// UserOperational is the causality />
        /// </summary>
        CausalityUserOperational,

        /// <summary>
        /// LackAuxilarySupplies is the causality />
        /// </summary>
        CausalityUserLackAuxilarySupplies,

        /// <summary>
        /// UserMaintenance is the causality />
        /// </summary>
        CausalityUserMaintenance,

        /// <summary>
        /// SupplyElectric is the causality />
        /// </summary>
        CausalitySupplyElectric,

        /// <summary>
        /// SupplyCompressedAir is the causality />
        /// </summary>
        CausalitySupplyCompressedAir,

        /// <summary>
        /// SupplyAirSuction is the causality />
        /// </summary>
        CausalitySupplyAirSuction,

        /// <summary>
        /// SupplyData is the causality />
        /// </summary>
        CausalitySupplyData,

        /// <summary>
        /// SupplyOffcut is the causality />
        /// </summary>
        CausalitySupplyOffcut

    }
}