// See https://aka.ms/new-console-template for more information

using HomagConnect.Applications.MmrMobile.Samples;
using HomagConnect.MmrMobile.Client;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;

Console.WriteLine("Hello at the HOMAG MMR Mobile Client");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("19e6f831-f72a-4ad8-9f70-72a3fe4c4e60")
    .Build();

var client = new HttpClient
{
    BaseAddress = new Uri(configuration["BaseUrl"]??"please check url in settings")
};

Console.WriteLine("Please insert your subscription Id:");
var subscriptionId = Console.ReadLine();
if (string.IsNullOrEmpty(subscriptionId))
{
    subscriptionId = configuration["SubscriptionId"];
}

Console.WriteLine("Please insert your token:");
var token = Console.ReadLine();
if (string.IsNullOrEmpty(token))
{
    token = configuration["Token"];
}

var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{token}"));
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

var choice = string.Empty;

do
{

    do
    {
        Console.WriteLine("\nDo you want to receive an evaluation of: \n" +
                          "1. All your machines counter data for the last 14 days. \n" +
                          "2. All your machines state data for the last 14 days.\n" +
                          "--------------------------------------------------------\n" +
                          "10. All your machines\n" +
                          "11. Nodes of machines\n" +
                          "12. Actual value of a node\n" +
                          "13. Old value of a node\n" +
                          "14. Timeline of a node\n" +
                          "--------------------------------------------------------\n" +
                          "99. Exit\n" +
                          " ");
        choice = Console.ReadLine();
        List<string> choices = new(["1", "2", "10", "11", "12", "13", "14", "99"]);
        if (choices.Exists(c => c == choice))
        {
            break;
        }
    } while (true);

    var mmrMobileClient = new MmrMobileClient(client);

    switch (choice)
    {
        case "1":
            await StatesAndCountesSamples.GetCounterData(mmrMobileClient);
            break;
        case "2":
            await StatesAndCountesSamples.GetStateData(mmrMobileClient);
            break;
        case "10":
            await MachineDataSamples.GetMachines(mmrMobileClient);
            break;
        case "11":
            await MachineDataSamples.GetNodeList(mmrMobileClient);
            break;
        case "12":
            await MachineDataSamples.GetActualNodeValue(mmrMobileClient);
            break;
        case "13":
            await MachineDataSamples.GetOldNodeValue(mmrMobileClient);
            break;
        case "14":
            await MachineDataSamples.GetNodeHistory(mmrMobileClient);
            break;
        case "99":
            break;
        default:
            Console.WriteLine($"Unexpected choice {choice}");
            break;
    }

} while (choice != "99");