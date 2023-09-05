# HOMAG MMR Mobile Client

With these packages you can easily integrate different workflows of HOMAG applications into your own application.

## Version history

Version   | Date     | Comment 
----------|----------|---------
1.0.0     |05.09.2023| First Draft

## Content table

1. [User-Documentation](#user-Documentation)<br/>
    1.1 [TL;DR](#tldr)<br/>
    1.2 [PowerBI](#use-in-power-bi)<br/>
    1.3 [Excel](#use-data-in-excel)
2. [Developer-Documentation](#developer-documentation)<br/>
    2.1 [Homag Connect MMR Mobile interface overview](#homag-connect-mmr-mobile-interface-overview)<br/>
    2.2 [Details](#details)

## User-Documentation
### TL;DR

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


### Use in Power BI

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


### Use data in Excel

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

## Developer-Documentation
### Homag Connect MMR Mobile interface overview

Name           | Method | API                                                                                                                                                                                                                              | Usage 
---------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------
GetStateData   |GET     |`api/{subscriptionId}/mmr/`<br/>`states?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&stateId={stateId}`<br/>`&stateGroupId={stateGroupId}`| Returns all state data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.
GetCounterData |GET     |`api/{subscriptionId}/mmr/`<br/>`counter?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&counterId={counterId}`                              | Returns all counter data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.

### Details
#### GetStateData
##### Input 
Parameter      | Description
---------------|------------------------------------------------------
subscriptionId | The id of the subscription
from           | DateTime that the search should start from
to             | DateTime that the search should end
machineNumber  | Number of the machine
instanceId     | The id of the instance
machineType    | Type of machine (e.g. CNC, Drilling, etc.)
stateId        | Id of the state
stateGroupId   | Id of the state group

##### Output
Property          | Description
------------------|-----------------------------------------------
Machine Number    | Number of the machine
Machine Name      | Name of the machine
Machine Type      | Type of machine (e.g. CNC, Drilling, etc.)
Timestamp         | Time when the data was gathered
Duration [h]      | Time that the machine spent in the state in hours 
Instance Id       | Id of the instance
Detailed State Id | Internal detailed state id
Detailed State    | Detailed state translated into the requested language
State Id          | Internal state id
State             | State translated into the requested language

##### Example

Request

```text
GET /api/{someSubscriptionId}/mmr/states
api-version: 2023-09-05
Accept-Language: de-DE
Authorization: Basic NjU1MDFEMDktMkJCOS00M0MyLUI5RDMtMUZCMDAwNkE3NjlFOnNkMDlzaGR1Z985OGffc2ZkZ3pz32Y5ZGhzYWZkaHNmZN92ODlwYmZkOXZiaGFmZGd2
tracestate: someinternaltracedata
```

Response (200 OK)

```text
Content-Type: application/json; charset=utf-8
```

```json
[
    {
        "Machine Number": "0-242-92-1234",
        "Machine Name": "Some Machine | 0-242-92-1234",
        "Machine Type": "CNC",
        "Timestamp": "2022-09-27T00:00:00",
        "Duration [h]": 4.348055555555556,
        "Instance Id": "M1-C1",
        "Detailed State Id": "S_OMU_MODE1",
        "Detailed State": "Hauptnutzung Tisch links",
        "State Id": "s_mainusage",
        "State": "Hauptnutzung"
    }
]
```

#### GetCounterData
##### Input
Parameter      | Description
---------------|------------------------------------------------------
subscriptionId | The id of the subscription
from           | DateTime that the search should start from
to             | DateTime that the search should end
machineNumber  | Number of the machine
instanceId     | The id of the instance
machineType    | Type of machine (e.g. CNC, Drilling, etc.)
counterId      | Id of the state

##### Output
Property       | Description
---------------|-----------------------------------------------
Machine Number | Number of the machine
Machine Name   | Name of the machine
Machine Type   | Type of machine (e.g. CNC, Drilling, etc.)
Timestamp      | Time when the data was gathered
Value          | Output value 
Instance Id    | Id of the instance
Counter Id     | Internal counter id
Counter        | Counter translated into the requested language

##### Example

Request

```text
GET /api/{someSubscriptionId}/mmr/counters
api-version: 2023-09-05
Accept-Language: de-DE
Authorization: Basic NjU1MDFEMDktMkJCOS00M0MyLUI5RDMtMUZCMDAwNkE3NjlFOnNkMDlzaGR1Z985OGffc2ZkZ3pz32Y5ZGhzYWZkaHNmZN92ODlwYmZkOXZiaGFmZGd2
tracestate: someinternaltracedata
```

Response (200 OK)

```text
Content-Type: application/json; charset=utf-8
```

```json
[
    {
        "Machine Number": "0-242-92-1234",
        "Machine Name": "Some Machine | 0-242-92-1234",
        "Machine Type": "CNC",
        "Timestamp": "2022-09-05T00:00:00",
        "Value": 62.0,
        "Instance Id": "M1-C1",
        "Counter Id": "S_OUT_CyclesAll",
        "Counter": "Ausbringung Zyklen"
    }
]
```

## Contribute

If you find anything, feel free to contribute to this repository. We are happy for every improvement ❤️.
