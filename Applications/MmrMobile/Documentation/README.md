# HOMAG MMR Mobile Client

With these packages you can easily integrate different workflows of HOMAG applications into your own application.

# Content table

1. [TL;DR](#tldr)
2. [PowerBI](#use-in-power-bi)
3. [Excel](#use-data-in-excel)

## TL;DR

~~~bash
NuGet Coming soon 
~~~

~~~csharp
using System.Net.Http.Headers;
using System.Text;

using HomagConnect.MmrMobile.Client.Services;

Console.WriteLine("Hello at the HOMAG MMR Mobile Client");

var client = new HttpClient();
client.BaseAddress = new Uri("https://connect.homag.cloud");
Console.WriteLine("Please insert your subscription Id:");
var subscriptionId = Console.ReadLine();
Console.WriteLine("Please insert your token:");
var token = Console.ReadLine();
var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{token}"));
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
var states = await mmrMobileService.GetStateData(subscriptionId);
var counters = await mmrMobileService.GetCounterData(subscriptionId);
Console.WriteLine($"You got {states.Count()} states and {counters.Count()} counter for the last 14 days")
~~~

~~~bash
dotnet run
~~~


## Use in Power BI

1. Get the Power BI [sample file](/Applications/MmrMobile/Samples/PowerBI/StatesAndCounters.pbix).<br><br>


2. Click on **Transform data**.<br>
![Transform](Assets/pbi_main.png)<br><br>

3. Adjust the parameters.<br>
Remark: You must add here your subscriptionId (from tapio) and perhaps adjust the number of days for which you want to get data.<br>
![Parameter adjust](Assets/pbi_params.png)<br><br>

4. Adjust credentials. <br>
Remark: Please add your personal credentials into the dialog. If you don´t know how to get your credentials click [here](/README.md#get-your-personal-access-token-for-your-application).<br>
![Parameter adjust](Assets/pbi_connect.png)<br><br>

5. Hit **Close** and **Apply** button in the ribbon.<br>


## Use data in Excel

1. Copy the excel [sample file](/Applications/MmrMobile/Samples/Excel/StatesAndCounters.xlsx).<br><br>

2. Go to the powerQuery Management.   
   1. Click on queries in the ribbon.
   2. Double-click one of the queries. 
   3. Select the first query and click on **advanced editor**.

   ![Parameter adjust](Assets/excel_main.png)<br><br>

3. Change the subscription Id in the advanced query editor
![Advanced editor](Assets/excel_editor.png)<br><br>

4. Change the subscription and credentials. If you don´t know how to get your credentials click [here](/README.md#get-your-personal-access-token-for-your-application).<br>
Remark: The username is the name of your tapio-account (see it in the url of your browser, when you are in the management view) 
![Alt text](Assets/excel_credentials.png)<br><br>

5. Hit **Close** and **Apply** button in the ribbon.


## Contribute

If you find anything, feel free to contribute to this repository. We are happy for every improvement ❤️.
