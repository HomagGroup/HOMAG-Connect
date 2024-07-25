using HomagConnect.MmrMobile.Contracts;

namespace HomagConnect.Applications.MmrMobile.Samples.Console.AlertsEvents
{
    /// <summary>
    /// Sample for customers
    /// </summary>
    internal static class AlertEventsSamples
    {
        /// <summary>
        /// get all events of a period
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetAlerts(IMmrMobileClient mmrMobileClient)
        {
            var machines = await mmrMobileClient.GetMachines();
            if (machines != null)
            {
                foreach (var machine in machines)
                {
                    var alerts = await mmrMobileClient.GetAlertEventsFromMachine(machine.MachineNumber, DateTime.Now.AddDays(-14), DateTime.Now, 1000);
                    if (machines != null)
                    {
                        System.Console.WriteLine($"Alerts fo machine {machine.MachineNumber}");
                        foreach (var alertEvent in alerts)
                        {
                            System.Console.WriteLine($"You have an alert {alertEvent.LocalizedMessage.First().Value} from {alertEvent.StartTime} to {alertEvent.EndTime}" );
                        }
                    }
                    else
                    {
                        System.Console.WriteLine($"You have no alerts for machine {machine.MachineNumber} named {machine.MachineName}");
                    }
                }
            }
            else
            {
                System.Console.WriteLine("No data has been found related to this subscription.");
            }
        }
    }
}