using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.MmrMobile.Client.Models;

namespace HomagConnect.MmrMobile.Client.Interfaces
{
    public interface IMmrMobileService
    {
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
            string machineNumber = null,
            string instanceId = null, string machineType = null, string stateId = null, string detailedStateId = null, Granularity? granularity = null);

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
            string machineNumber = null, string instanceId = null, string machineType = null, string counterId = null, Granularity? granularity = null);
    }
}