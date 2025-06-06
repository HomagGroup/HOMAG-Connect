﻿using HomagConnect.MmrMobile.Contracts;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;

namespace HomagConnect.Applications.MmrMobile.Samples
{
    /// <summary>
    /// Sample for customers
    /// </summary>
    internal static class StatesAndCountesSamples
    {
        private const int percent = 100;
        private const int Digits = 2;

        /// <summary>
        /// get all counters (last 14 days)
        /// group and summarize by counterid
        /// Write the content of the first couter to console
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetMmrMachines(IMmrMobileClient mmrMobileClient)
        {
            var machines = await mmrMobileClient.GetMmrMachines();
            if (machines != null &&  machines.Any())
            {
               
                System.Console.WriteLine($"You mave {machines.Count()} machines having mmr data");
            }
            else
            {
                System.Console.WriteLine("No data has been found related to this subscription.");
            }
        }
        /// <summary>
        /// get all counters (last 14 days)
        /// group and summarize by counterid
        /// Write the content of the first couter to console
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetCounterData(IMmrMobileClient mmrMobileClient)
        {
            var counters = await mmrMobileClient.GetCounterData();
            if (counters != null && counters.Any())
            {
                var groupedCounter = counters.GroupBy(c => new { c.CounterId }).Select(s => new
                {
                    s.Key.CounterId,
                    TotalCounter = s.Sum(x => x.Value)
                });
                System.Console.WriteLine($"You produced {groupedCounter.First().TotalCounter} pieces in the last 14 days.");
            }
            else
            {
                System.Console.WriteLine("No data has been found related to this subscription.");
            }
        }

        /// <summary>
        /// get all states (by default 14 days)
        /// group by state and aggregate the duration in hours
        /// get sum over all states
        /// print out every single state and its duration in hours
        /// calculate roughly the mainusage-percentage by getting the sum of states EXCEPT OutOfOperation
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetStateData(IMmrMobileClient mmrMobileClient)
        {
            var states = await mmrMobileClient.GetStateData(from: DateTime.Now.AddDays(-3), to: DateTime.Now );
            if (states != null && states.Any())
            {
                var groupedStates = states.GroupBy(c => new { c.StateId, c.StateTranslation }).Select(s => new
                {
                    s.Key.StateId,
                    s.Key.StateTranslation,
                    StateTime = s.Sum(x => x.DurationInHours),
                }).ToArray();

                var maximumWorkingHours = groupedStates.Sum(c => c.StateTime);
                System.Console.WriteLine("\nYour machines have been in the following stages for this many hours:");

                foreach (var state in groupedStates)
                {
                    System.Console.WriteLine($"{state.StateTranslation}: {Math.Round(state.StateTime, Digits)}");
                }

                System.Console.WriteLine("\nSeen in percentage it would be: (out of operation excluded)");
                foreach (var state in groupedStates.Where(y => y.StateId != StateGroupCodes.OutOfOperation))
                {
                    System.Console.WriteLine($"{state.StateTranslation}: {Math.Round(state.StateTime / maximumWorkingHours * percent, Digits)}%");
                }
            }
            else
            {
                System.Console.WriteLine("No data has been found related to this subscription.");
            }
        }
    }
}