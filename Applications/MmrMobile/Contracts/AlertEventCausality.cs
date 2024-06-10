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
        Machine,

        /// <summary>
        /// User is the causality />
        /// </summary>
        User,

        /// <summary>
        /// Supply is the causality />
        /// </summary>
        Supply,

        /// <summary>
        /// PlantMode is the causality />
        /// </summary>
        PlantMode,

        /// <summary>
        /// unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// MachineElectric is the causality />
        /// </summary>
        MachineElectric,

        /// <summary>
        /// MachineMechanic is the causality />
        /// </summary>
        MachineMechanic,

        /// <summary>
        /// MachineControl is the causality />
        /// </summary>
        MachineControl,

        /// <summary>
        /// UserOperational is the causality />
        /// </summary>
        UserOperational,

        /// <summary>
        /// LackAuxilarySupplies is the causality />
        /// </summary>
        UserLackAuxilarySupplies,

        /// <summary>
        /// UserMaintenance is the causality />
        /// </summary>
        UserMaintenance,

        /// <summary>
        /// SupplyElectric is the causality />
        /// </summary>
        SupplyElectric,

        /// <summary>
        /// SupplyCompressedAir is the causality />
        /// </summary>
        SupplyCompressedAir,

        /// <summary>
        /// SupplyAirSuction is the causality />
        /// </summary>
        SupplyAirSuction,

        /// <summary>
        /// SupplyData is the causality />
        /// </summary>
        SupplyData,

        /// <summary>
        /// SupplyOffcut is the causality />
        /// </summary>
        SupplyOffcut

    }
}