﻿using System;
using System.ComponentModel;
using System.Diagnostics;

using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts
{
    /// <summary />
    [DebuggerDisplay("Name={Name}")]
    public class Optimization
    {
        /// <summary>
        /// Gets or sets the optimization id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the last datetime the optimization was modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a link to open the optimization in the app.
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the machine the optimization is done for.
        /// </summary>
        public string Machine { get; set; }

        /// <summary>
        /// Gets or sets the optimization name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="OptimizationType" />
        /// </summary>
        public OptimizationType OptimizationType { get; set; }

        /// <summary>
        /// Gets or sets the optimization parameter set name.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary />
        [Obsolete("Replaced with QuantityOfParts")]
        public int PartsCount
        {
            get
            {
                return QuantityOfParts;
            }
            set
            {
                QuantityOfParts = value;
            }
        }

        /// <summary>
        /// Gets or sets the estimated production time.
        /// </summary>
        public TimeSpan ProductionTime { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts.
        /// </summary>
        public int QuantityOfParts { get; set; }

        /// <summary />
        [Obsolete("Replaced with Waste")]
        public double Scrap
        {
            get
            {
                return Waste;
            }
            set
            {
                Waste = value;
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public OptimizationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the waste.
        /// </summary>
        [DefaultValue(0.0)]
        public double Waste { get; set; }
    }
}