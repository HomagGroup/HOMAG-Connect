using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMmrMobileClient
    {

        #region machinedata
        /// <summary>
        /// Request the current value of one machine node or node subtree.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrNodeData> GetCurrentValuesFromMachine(string machineNumber, string node, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get all available machines with a HOMAG CONNECT MMR Mobile license.
        /// </summary>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<IEnumerable<MmrMachine>> GetMachines(OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get all available nodes of a machine.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrNodeList> GetNodesOfMachine(string machineNumber, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Request the value of one machine node or node subtree at a specific point in time.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="timestamp">The point in time to query.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrNodeData> GetPointInTimeValuesFromMachine(string machineNumber, string node, DateTime timestamp, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get for a given duration and a given list of nodes all values reported from the machine.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="from">Inclusive start timestamp.</param>
        /// <param name="to">Inclusive end timestamp.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get for a given duration and a given list of nodes all values reported from the machine.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="daysBack">Optional relative lookback window in days.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

/// <summary>
/// upload a zip-file for a specific machine.
/// </summary>
/// <returns></returns>
        Task UploadProductionProtocol(Stream file, string machine);

        #endregion

        #region eventdata

        /// <summary>
        /// Get all events from the requested time span.
        /// Events that overlap the requested duration are included as well.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="from">Inclusive start timestamp.</param>
        /// <param name="to">Inclusive end timestamp.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns cref="AlertEvent">A list of AlertEvents</returns>
        Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get all events from a relative lookback window.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="daysBack">Number of days to look back from now.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns cref="AlertEvent">A list of AlertEvents</returns>
        Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get recent alerts. The end date is fixed to now and the start date is calculated from <paramref name="daysBack"/>.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="daysBack">Number of days to look back from now.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// Get recent alerts for the given time span.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="from">Inclusive start timestamp.</param>
        /// <param name="to">Inclusive end timestamp.</param>
        /// <param name="take">Maximum number of rows to return.</param>
        /// <param name="skip">Number of rows to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

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
        /// <param name="take">Optional maximum number of state data buckets to return.</param>
        /// <param name="skip">Optional number of state data buckets to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
#pragma warning disable S107
        Task<IEnumerable<MachineState>> GetStateData(DateTime? from = null, DateTime? to = null,
            string? machineNumber = null,
            string? instanceId = null, string? machineType = null, string? stateId = null, string? detailedStateId = null, Granularity? granularity = null,
            int? take = null, int skip = 0,
            OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);
#pragma warning restore S107

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
        /// <param name="take">Optional maximum number of counter data buckets to return.</param>
        /// <param name="skip">Optional number of counter data buckets to skip.</param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<IEnumerable<MachineCounter>> GetCounterData(DateTime? from = null, DateTime? to = null,
            string? machineNumber = null, string? instanceId = null, string? machineType = null, string? counterId = null, Granularity? granularity = null,
            int? take = null, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo? culture = null);

        /// <summary>
        /// get all machines, the customer has access to
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MmrMachine>> GetMmrMachines();

        /// <summary>
        /// get one machine information
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="format">Output format of the response.</param>
        /// <param name="culture">Culture used for localized output.</param>
        /// <returns></returns>
        Task<MmrMachine?> GetMmrMachine(string machineNumber, JsonFormat format = JsonFormat.Default, CultureInfo? culture = null);

        #endregion
    }
}