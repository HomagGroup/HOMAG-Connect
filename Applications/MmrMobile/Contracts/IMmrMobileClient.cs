using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMmrMobileClient
    {

        #region machinedata
        /// <summary>
        /// request for one machine and a (comma separated) list of nodes the last know value
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        Task<MmrNodeData> GetCurrentValuesFromMachine(string machineNumber, string node);

        /// <summary>
        /// get all available machines with a HOMAG CONNECT MMR Mobile license
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MmrMachine>> GetMachines();

        /// <summary>
        /// get all available nodes of a machine
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        Task<MmrNodeList> GetNodesOfMachine(string machineNumber);

        /// <summary>
        /// request for one machine and a (comma separated) list of nodes the last know value
        /// for a given point in time
        /// </summary>
        /// <returns></returns>
        Task<MmrNodeData> GetPointInTimeValuesFromMachine(string machineNumber, string node, DateTime timestamp);

        /// <summary>
        /// Get for a given duration and a given list of nodes all values reported from the machine
        /// </summary>
        /// <returns></returns>
        Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, DateTime from, DateTime to, int take , int skip = 0);
        #endregion

        #region eventdata

        /// <summary>
        /// get all  events from the requested timespan
        /// we show also events, that overlap the requested duration 
        /// - start was before from-value
        /// - end is after to-value
        /// </summary>
        /// <returns cref="AlertEvent">A list of AlertEvents</returns>
        Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, DateTime from, DateTime to, int take, int skip = 0);

        /// <summary>
        /// gets recent alerts . To-date is fix to "now", From date is calculated automatically
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, int daysBack, int take, int skip = 0);

        #endregion

        #region mmr states and counters
        /// <summary>
        /// generic approarch for state data with much filters
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="machineNumber"></param>
        /// <param name="instanceId"></param>
        /// <param name="machineType"></param>
        /// <param name="stateId"></param>
        /// <param name="detailedStateId"></param>
        /// <param name="granularity"></param>
        /// <returns></returns>
        Task<IEnumerable<MachineState>> GetStateData(DateTime? from = null, DateTime? to = null,
            string? machineNumber = null,
            string? instanceId = null, string? machineType = null, string? stateId = null, string? detailedStateId = null, Granularity? granularity = null);

        /// <summary>
        /// generic approarch for counter data with much filters
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="machineNumber"></param>
        /// <param name="instanceId"></param>
        /// <param name="machineType"></param>
        /// <param name="counterId"></param>
        /// <param name="granularity"></param>
        /// <returns></returns>
        Task<IEnumerable<MachineCounter>> GetCounterData(DateTime? from = null, DateTime? to = null,
            string? machineNumber = null, string? instanceId = null, string? machineType = null, string? counterId = null, Granularity? granularity = null);

        /// <summary>
        /// get all machines, the customer has access to
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MmrMachine>> GetMmrMachines();

        /// <summary>
        /// get one machine information
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<MmrMachine>> GetMmrMachine(string machineNumber);

        #endregion
    }
}