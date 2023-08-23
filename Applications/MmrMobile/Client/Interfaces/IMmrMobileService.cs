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
        /// <param name="subscriptionId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="machineNumber"></param>
        /// <param name="instanceId"></param>
        /// <param name="machineType"></param>
        /// <param name="stateId"></param>
        /// <param name="stateGroupId"></param>
        /// <returns></returns>
        Task<IEnumerable<MachineState>> GetStateData(string subscriptionId, DateTime? from = null, DateTime? to = null,
            string machineNumber = null,
            string instanceId = null, string machineType = null, string stateId = null, string stateGroupId = null);

        /// <summary>
        /// generic approarch for counter data with much filters
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="machineNumber"></param>
        /// <param name="instanceId"></param>
        /// <param name="machineType"></param>
        /// <param name="counterId"></param>
        /// <returns></returns>
        Task<IEnumerable<MachineCounter>> GetCounterData(string subscriptionId, DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null, string machineType = null, string counterId = null);
    }
}