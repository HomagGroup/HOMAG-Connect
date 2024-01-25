using System.Diagnostics;
using System;

namespace HomagConnect.IntelliDivide.Contracts
{
    [DebuggerDisplay("Name={Name}")]
    public class Optimization
    {
        public Guid Id { get; set; }

        public Uri Link { get; set; }

        public string Machine { get; set; }

        public string Name { get; set; }

        public string ParameterName { get; set; }

        public OptimizationStatus Status { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int PartsCount { get; set; }

        public double Scrap { get; set; }

        public TimeSpan ProductionTime { get; set; }
    }
}