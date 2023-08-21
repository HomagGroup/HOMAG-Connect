// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text;

using HomagConnect.MmrMobile.Client.Services;
using HomagConnect.MmrMobile.Samples.Console;

Console.WriteLine("Hello at the HOMAG MMR Mobile Client");

var client = new HttpClient();
client.BaseAddress = new Uri("https://connect-preview.homag.cloud");
Console.WriteLine("Please insert your subscription Id:");
var subscriptionId = Console.ReadLine();
Console.WriteLine("Please insert your token:");
var token = Console.ReadLine();
var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{token}"));
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

do
{
    var choice = string.Empty;
    do
    {
        Console.WriteLine("\nDo you want to receive an evaluation of \n" +
                          "1. All your machines counter data for the last 14 days \n" +
                          "2. All your machines state data for the last 14 days \n" +
                          "3. Get your production cycle for the last 14 days");
        choice = Console.ReadLine();
        if (choice == "1" || choice == "2" || choice == "3")
        {
            break;
        }
    } while (true);

    var mmrMobileService = new MmrMobileService(client);

    switch (choice)
    {
        case "1":
            GetCounterData().Wait();
            break;
        case "2":
            GetStateData().Wait();
            break;
        case "3":
            GetProductionCycle().Wait();
            break;
    }

    async Task GetCounterData()
    {
        var counters = await mmrMobileService.GetCounterData(subscriptionId);
        if (counters != null)
        {
            var groupedCounter = counters.GroupBy(c => new { c.CounterId }).Select(s => new
            {
                s.Key.CounterId,
                TotalCounter = s.Sum(x => x.Value)
            });
            Console.WriteLine($"You produced {groupedCounter.First().TotalCounter} pieces in the last 14 days");
        }
        else
        {
            Console.WriteLine("No data has been found related to this subscription");
        }
    }

    async Task GetStateData()
    {
        var states = await mmrMobileService.GetStateData(subscriptionId);
        if (states != null)
        {
            var groupedStates = states.GroupBy(c => new { c.StateId }).Select(s => new
            {
                s.Key.StateId,
                StateTime = s.Sum(x => x.DurationInHours),
            });
            var maximumWorkingHours = groupedStates.Sum(c => c.StateTime);
            Console.WriteLine("\nYour machines have been in the following stages for this many hours:");
            foreach (var state in groupedStates)
            {
                Console.WriteLine($"{state.StateId}: {Math.Round(state.StateTime, 2)}");
            }

            Console.WriteLine("\nSeen in percentage it would be (out of operation excluded)");
            foreach (var state in groupedStates.Where(y => y.StateId != StateGroupCodes.OutOfOperation))
            {
                Console.WriteLine($"{state.StateId}: {Math.Round(state.StateTime / maximumWorkingHours * 100, 2)}%");
            }
        }
        else
        {
            Console.WriteLine("No data has been found related to this subscription");
        }
    }

    async Task GetProductionCycle()
    {
        var states = await mmrMobileService.GetStateData(subscriptionId);
        var counters = await mmrMobileService.GetCounterData(subscriptionId);

        if (counters != null && states != null)
        {
            var groupedStates = states.GroupBy(c => new { c.StateId }).Select(s => new
            {
                s.Key.StateId,
                StateTime = s.Sum(x => x.DurationInHours),
            }).Where(y => y.StateId != StateGroupCodes.OutOfOperation);
            var maximumWorkingHours = groupedStates.Sum(c => c.StateTime);

            var groupedCounter = counters.GroupBy(c => new { c.CounterId }).Select(s => new
            {
                s.Key.CounterId,
                TotalCounter = s.Sum(x => x.Value)
            });

            Console.WriteLine(
                $"Your production cycle for the last 14 days was {Math.Round(groupedCounter.First().TotalCounter / maximumWorkingHours, 2)} parts per hour ");
        }
    }

    Console.WriteLine("\nDo you want to exit? [y/n]");
    var exit = Console.ReadLine();
    if (exit.ToLower() == "y")
    {
        break;
    }
} while (true);