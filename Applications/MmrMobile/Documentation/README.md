<span style="color:red">[This is preliminary documentation and is subject to change.] </span>

# HOMAG MMR Mobile Client

HOMAG Connect MMR Mobile gives you direct access to your machine data (counters, states) from MMR Mobile. You can then conveniently integrate this into your applications.
To help you get started, we have prepared a few examples that you can find below.

## Version history

Version   | Date     | Comment
----------|----------|------------
1.0.0     |07.09.2023| First Draft
1.1.0     |27.10.2023| Add granularity for getting the data and updating the technical documentation

## Content table
1. [TL;DR](#tldr)
2. [Homag Connect MMR Mobile interface overview](#homag-connect-mmr-mobile-interface-overview)
3. [Details](#details)

## Authorization


## TL;DR

~~~bash
mkdir test-homag-connect-mmr-client
dotnet new console
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
dotnet add package HomagGroup.HomagConnect.MmrMobile.Client
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
var states = await mmrMobileService.GetStateData();
var counters = await mmrMobileService.GetCounterData();
Console.WriteLine($"You got {states.Count()} states and {counters.Count()} counter for the last 14 days")
~~~

~~~bash
dotnet run
~~~

## Homag Connect MMR Mobile interface overview

Name           | Method | API                                                                                                                                                                                                                                                                | Usage
---------------|--------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------
GetStateData   |GET     |`api/mmr/`<br/>`states?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&stateId={stateId}`<br/>`&detailedStateId={detailedStateId}`<br/>`&granularity={granularity}`| Returns all state data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.
GetCounterData |GET     |`api/mmr/`<br/>`counter?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&counterId={counterId}`<br/>`&granularity={granularity}`                                    | Returns all counter data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.

## Details
### Rate Limiting
As mentioned in the base documentation, each application has a different rate limitation. For the following endpoints this limit is currently set to 6 requests in a minute.

### GetStateData
#### Input
Parameter                    | Type     | Description
-----------------------------|----------|---------------------------------------------------
from *(Optional)*            | DateTime | DateTime that the search should start from
to *(Optional)*              | DateTime | DateTime that the search should end
machineNumber *(Optional)*   | string   | Number of the machine (Format: x-xxx-xx-xxxx)
instanceId *(Optional)*      | string   | The id of the instance
machineType *(Optional)*     | string   | Type of machine
stateId *(Optional)*         | string   | Id of the state
detailedStateId *(Optional)* | string   | Id of the detailed state
granularity *(Optional)*     | string   | Specifies granualrity of the returned data (hour, day, week, month). Default will be like the following: 1 day: hourly, 2-14 days: daily, 15 days - 3 months: weekly, every timespan requested bigger than 3 months: monthly if not asked specifically. The hourly data is only available for the last 14 days.

#### Output
Property          | Type     | Description
------------------|----------|-----------------------------------------------
Machine Number    | string   | Number of the machine
Machine Name      | string   | Name of the machine
Machine Type      | string   | Type of machine
Timestamp         | DateTime | Day when the data was gathered
Granularity       | string   | Granularity of the requested data
Duration [h]      | double   | Time that the machine spent in the state in hours
Instance Id       | string   | Id of the instance
Detailed State Id | string   | Id of the detailed state
Detailed State    | string   | Detailed state translated into the requested language
State Id          | string   | Id of the state
State             | string   | State translated into the requested language


#### Example

Request

```text
GET /api/mmr/states
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
        "Granularity": "day",
        "Duration [h]": 4.348055555555556,
        "Instance Id": "M1-C1",
        "Detailed State Id": "S_OMU_MODE1",
        "Detailed State": "Hauptnutzung Tisch links",
        "State Id": "s_mainusage",
        "State": "Hauptnutzung"
    }
]
```
The default route with no timespan added will always return related data for the last 14 days.
### GetCounterData
#### Input
Parameter                   | Type     | Description
----------------------------|----------|-------------------------------------------
from *(Optional)*           | DateTime | DateTime that the search should start from
to *(Optional*)             | DateTime | DateTime that the search should end
machineNumber *(Optional)*  | string   | Number of the machine (Format: x-xxx-xx-xxxx)
instanceId *(Optional)*     | string   | The id of the instance
machineType *(Optional)*    | string   | Type of machine
counterId *(Optional)*      | string   | Id of the counter
granularity *(Optional)*    | string   | Specifies granualrity of the returned data (hour, day, week, month). Default will be like the following: 1 day: hourly, 2-14 days: daily, 15 days - 3 months: weekly, every timespan requested bigger than 3 months: monthly if not asked specifically. The hourly data is only available for the last 14 days.

#### Output
Property       | Type     | Description
---------------|----------|------------------------------------------------
Machine Number | string   | Number of the machine
Machine Name   | string   | Name of the machine
Machine Type   | string   | Type of machine
Timestamp      | DateTime | Day when the data was gathered
Granularity    | string   | Granularity of the requested data
Value          | double   | Output value
Instance Id    | string   | Id of the instance
Counter Id     | string   | Id of the counter
Counter        | string   | Counter translated into the requested language

#### Example

Request

```text
GET /api/mmr/counters
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
        "Timestamp": "2022-09-05T05:00:00",
        "Granularity": "hour",
        "Value": 62.0,
        "Instance Id": "M1-C1",
        "Counter Id": "S_OUT_CyclesAll",
        "Counter": "Ausbringung Zyklen"
    }
]
```
The default route with no timespan added will always return related data for the last 14 days.

## Contribute

If you find anything, feel free to contribute to this repository. We are happy for every improvement ❤️.
