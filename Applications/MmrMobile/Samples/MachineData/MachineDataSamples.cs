using HomagConnect.MmrMobile.Contracts;

namespace HomagConnect.Applications.MmrMobile.Samples
{
    /// <summary>
    /// Sample for customers
    /// </summary>
    internal static class MachineDataSamples
    {
        /// <summary>
        /// get all machines of the customer
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetMachines(IMmrMobileClient mmrMobileClient)
        {
            var machines = await mmrMobileClient.GetMachines();
            if (machines != null)
            {
                foreach (var machine in machines)
                {
                    Console.WriteLine($"You have a machine {machine.MachineNumber} named {machine.MachineName}");
                }
            }
            else
            {
                Console.WriteLine("No data has been found related to this subscription.");
            }
        }


        /// <summary>
        /// get all nodes of a machine
        /// </summary>
        /// <param name="mmrMobileClient"></param>
        /// <returns></returns>
        public static async Task GetNodeList(IMmrMobileClient mmrMobileClient)
        {
            IEnumerable<string?> machineNumbers = await GetMachineNumbers(mmrMobileClient);
            Console.WriteLine($"Found {machineNumbers.Count()} machines");

            foreach (var machineNumber in machineNumbers)
            {
                MmrNodeList nodes = await mmrMobileClient.GetNodesOfMachine(machineNumber ?? "");

                if (nodes != null && nodes.Nodes?.Length > 0)
                {
                    foreach (var node in nodes.Nodes)
                    {
                        Console.WriteLine($"machine {nodes.MachineName} has a node {node} ");
                    }
                }
                else
                {
                    Console.WriteLine($"No nodes have been found related to this machine: {machineNumber}.");
                }
            }
        }

        public static async Task GetActualNodeValue(IMmrMobileClient mmrMobileClient)
        {
            (string machine, string nodeName) = await GetValidMachineNode(mmrMobileClient);

            MmrNodeData value = await mmrMobileClient.GetCurrentValuesFromMachine(machine, nodeName);

            if (value != null && value.Nodes?.Length > 0)
            {
                foreach (MmrNode node in value.Nodes)
                {
                    Console.WriteLine($"machine '{value.MachineName}' has the value in node '{node.Node}' : '{node.Value}' from '{node.Timestamp}' ");
                }
            }
            else
            {
                Console.WriteLine($"No current value has been found related to the machine '{machine}' and the node '{nodeName}'.");
            }
        }

        public static async Task GetOldNodeValue(IMmrMobileClient mmrMobileClient)
        {
            (string machine, string nodeName) = await GetValidMachineNode(mmrMobileClient);

            MmrNodeData value = await mmrMobileClient.GetPointInTimeValuesFromMachine(machine, nodeName, DateTime.Now.AddDays(-4));

            if (value != null && value.Nodes?.Length > 0)
            {
                foreach (var node in value.Nodes)
                {
                    Console.WriteLine($"machine '{value.MachineName}' has the value in node '{node.Node}' : '{node.Value}' from '{node.Timestamp}' ");
                }
            }
            else
            {
                Console.WriteLine($"No current value has been found related to the machine '{machine}' and the node '{nodeName}'.");
            }
        }

        public static async Task GetNodeHistory(IMmrMobileClient mmrMobileClient)
        {
            (string machine, string nodeName) = await GetValidMachineNode(mmrMobileClient);

            MmrNodeData value = await mmrMobileClient.GetTimeSeriesFromMachine(machine, nodeName, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-2));

            if (value != null && value.Nodes?.Length > 0)
            {
                foreach (var node in value.Nodes)
                {
                    Console.WriteLine($"machine '{value.MachineName}' has the value in node '{node.Node}' : '{node.Value}' from '{node.Timestamp}' ");
                }
            }
            else
            {
                Console.WriteLine($"No current value has been found related to the machine '{machine}' and the node '{nodeName}'.");
            }
        }

        #region internal helpers
        private static async Task<IEnumerable<string?>> GetMachineNumbers(IMmrMobileClient mmrMobileClient)
        {
            var machines = (await mmrMobileClient.GetMachines()).ToArray();
            if (!machines.Any())
            {
                return new List<string?>();
            }

            return machines.Select(m => m.MachineNumber).ToList();
        }

        private static async Task<(string, string)> GetValidMachineNode(IMmrMobileClient mmrMobileClient)
        {
            string?[] machineNumbers = (await GetMachineNumbers(mmrMobileClient)).ToArray();
            if (machineNumbers.Length == 0)
            {
                return ("", "");
            }

            string[]? nodes = (await mmrMobileClient.GetNodesOfMachine(machineNumbers[0] ?? ""))?.Nodes?.ToArray();
            if (nodes == null || nodes.Length == 0)
            {
                return (machineNumbers[0] ?? "", "");
            }

            return (machineNumbers[0] ?? "", nodes[0]);
        }


        #endregion
    }
}