﻿namespace HomagConnect.Base.Contracts
{
    /// <summary>
    /// Defines a storage location.
    /// </summary>
    public class StorageLocation
    {
        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public string Workstation { get; set; }

        /// <summary>
        /// Gets or sets the barcode of the compartment.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the name of the compartment.
        /// </summary>
        public string Name { get; set; }
    }
}