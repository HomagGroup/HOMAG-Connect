# HOMAG Connect MMR Mobile

HOMAG Connect MMR Mobile gives you direct access to your machine data (counters, states, PLC-Items companion Spec, Alerts) from MMR Mobile. You can then conveniently integrate this into your applications or BI-Tools.
To help you get started, we have prepared a few examples that you can find below.

## Version history

Version   | Date     | Comment
----------|----------|------------
1.0.0     |07.09.2023| First Draft
1.1.0     |27.10.2023| Add granularity for getting the data and updating the technical documentation
1.2.0     |27.03.2024|Add endpoint for machine data
1.3.0     |01.06.2024|Add endpoint for alertdata

## Content table

1. [TL;DR](#tldr)
2. [HOMAG Connect MMR Mobile interface overview](#homag-mmr-mobile-advanced-interface-overview-machinedata)
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

## HOMAG Connect MMR Mobile interface overview (states and counters)

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

## HOMAG Connect MMR Mobile interface overview (machinedata)

Name| Method | API | Usage
--|--|---|--
GetMachineList|GET     |`api/mmr-mobile/machinedata/machines`| Returns all machines assigned to the subscription, which have a license for the HOMAG Connect MMR Mobile Addon.
GetNodeList|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br/>`/nodes`| Returns for one valid machine, which nodes are available for this machine.
GetCurrentValues|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br/>`/nodes/{nodeName}`| Returns for one valid machine and a set of nodes the last reported values.
GetValuesAtTimestamp|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br />`/nodes/{nodeName}?timestamp={before}`| Returns for one valid machine and a set of nodes the last reported values at a specific point in time.
GetHistoricalValues|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br />`/nodes/{nodeName}/history?from={from}&to={to}&take={take}&skip0{skip}`| Returns for one valid machine and a set of nodes all reported values during a defined timespan.
GetAlertEventsFromMachine|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br />`//alerts/history?from={from}&to={to}&take={take}&skip0{skip}`| Returns for one valid machine and a list of alerts during a defined timespan.
GetRecentAlertEvents|GET     |`api/mmr-mobile/machinedata/machines/{machineNumber}`<br />`/alerts/history?daysBack&take={take}&skip0{skip}`| Returns for one valid machine and a list of alerts during a defined timespan.

## Details

### GetMachineList

#### Input

Parameter                    | Type     | Description
-----------------------------|----------|---------------------------------------------------

#### Output (List)

Property          | Type     | Description
------------------|----------|-----------------------------------------------
MachineNumber    | string   | Number of the machine
MachineName      | string   | Name of the machine

#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines
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
        "MachineNumber": "0-242-92-1234",
        "MachineName": "Some Machine | 0-242-92-1234"
    },
    {
        "MachineNumber": "0-242-92-1235",
        "MachineName": "Other Machine | 0-242-92-1235"
    }
]
```

### GetNodeList

#### Input

Parameter                    | Type     | Description
------------------|----------|---------------------------------------------------
MachineNumber    | string   | Number of the machine (any supported format: "hg1234567890", '1234567890', '0-123-45-6789')

#### Output

Property          | Type     | Description
------------------|----------|-----------------------------------------------
MachineNumber    | string   | Number of the machine
MachineName      | string   | Name of the machine
Nodes | string[]  | List of Node-Names

#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines/1234567890/nodes
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
{
    "MachineNumber": "0-242-92-1234",
    "MachineName": "Some Machine | 0-242-92-1234",
    "Nodes": [
        "Identification.Key1",
        "Identification.Key2",
        "Identification.Key3",
        "Identification.Key4"
    ]
},
```

### GetCurrentValue

#### Input

Parameter                    | Type     | Description
------------------|----------|---------------------------------------------------
MachineNumber    | string   | Number of the machine
node    | string   | List of Node-Names. <br />These names are inside one string and separated by comma. Every entry is either a node (complete name) or a branch (selecting all nodes beginning with the entry).<br />*sample Node*: Identification.Key1<br />*sample Branch*: Identification.<br />*sample combination*: Identification.Key.,Identification.Test1

#### Output (List)

Property          | Type     | Description
------------------|----------|-----------------------------------------------
MachineNumber    | string   | Number of the machine
MachineName      | string   | Name of the machine
Nodes | MmrNodeData[] | Structure for one Value at a specific point in time
`-` node |string|Nodename
`-` Value |string|Value
`-` Timestamp |Datetime|Timestamp, when the value was reported in UTC

  
#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines/1234567890/nodes/Identification.Key1
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
{
    "machineNumber": "0-256-02-2942",
    "machineName": "SANTEQ W-300 | 0-256-02-2942",
    "nodes": [
        {
            "timestamp": "2024-03-27T12:36:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        }
    ]
}
```

### GetValuesAtTimestamp

#### Input

Parameter    | Type     | Description
------|----------|------
MachineNumber    | string   | Number of the machine
node    | string   | List of Node-Names. <br />These names are inside one string and separated by comma. Every entry is either a node (complete name) or a branch (selecting all nodes beginning with the entry).<br />*sample Node*: Identification.Key1<br />*sample Branch*: Identification.<br />*sample combination*: Identification.Key.,Identification.Test1
timestamp    | DateTime   | Date in a valid ISO form and timezone

#### Output (List)

Property          | Type     | Description
------------------|----------|-----------------------------------------------
MachineNumber    | string   | Number of the machine
MachineName      | string   | Name of the machine
Nodes | MmrNodeData[] | Structure for one Value at a specific point in time
`-` node |string|Nodename
`-` Value |string|Value
`-` Timestamp |Datetime|Timestamp, when the value was reported in UTC

#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines/1234567890/nodes/Identification.Key1?timestamp=2024-03-29T05:16:51Z
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
{
    "machineNumber": "0-256-02-2942",
    "machineName": "SANTEQ W-300 | 0-256-02-2942",
    "nodes": [
        {
            "timestamp": "2024-03-29T04:10:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        }
    ]
}
```

### GetHistoricalValues

#### Input

Parameter    | Type     | Description
------|----------|------
MachineNumber    | string   | Number of the machine
node    | string   | List of Node-Names. <br />These names are inside one string and separated by comma. Every entry is either a node (complete name) or a branch (selecting all nodes beginning with the entry).<br />*sample Node*: Identification.Key1<br />*sample Branch*: Identification.<br />*sample combination*: Identification.Key.,Identification.Test1
from    | DateTime   | Date in a valid ISO form and timezone. Optional, Default = now minus  2 weeks
to    | DateTime   | Date in a valid ISO form and timezone. Optional, Default = now
take    | int   | Number of rows to read. Optional, Default = 10.000
skip    | DateTime   | Number of rows to skip. Optional, Default = 0

#### Output

Property          | Type     | Description
------------------|----------|-----------------------------------------------
MachineNumber    | string   | Number of the machine
MachineName      | string   | Name of the machine
Nodes | MmrNodeData[] | Structure for one Value at a specific point in time
`-` node |string|Nodename
`-` Value |string|Value
`-` Timestamp |Datetime|Timestamp, when the value was reported in UTC

#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines/1234567890/nodes/Identification.Key1?timestamp=2024-03-29T05:16:51Z
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
{
    "machineNumber": "0-256-02-2942",
    "machineName": "SANTEQ W-300 | 0-256-02-2942",
    "nodes": [
        {
            "timestamp": "2024-03-29T04:10:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        },
        {
            "timestamp": "2024-03-29T02:10:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        },
        {
            "timestamp": "2024-03-28T22:10:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        },
        {
            "timestamp": "2024-03-28T21:10:20.2778399Z",
            "node": "Identification.Key1",
            "value": "0.003"
        }
    ]
}
```

### GetAlertEventsFromMachine / GetRecentAlertEvents

#### Input

Parameter    | Type     | Description
------|----------|------
MachineNumber    | string   | Number of the machine
from    | DateTime   | Date in a valid ISO form and timezone. Optional, Default = now minus  2 weeks
to    | DateTime   | Date in a valid ISO form and timezone. Optional, Default = now
daysBack | int | Alternative to from and to. Takes always to = actual timestamp and goes back a number of days
take    | int   | Number of rows to read. Optional, Default = 10.000
skip    | DateTime   | Number of rows to skip. Optional, Default = 0

#### Output (List of Alerts)

Property          | Type     | Description
------------------|----------|-----------------------------------------------
startTime | DateTimeOffset | Start of the alert
endTime |  DateTimeOffset | End of the alert
instanceId | string |Internal Key
machineNumber | string | formatted Homag Machinenumber0-222-33-444
severity| int Number between 1 and 1000
localizedSource| Dictionary | Source of the event (in multiple languages
localizedMessage| Dictionary | Description of the event (multiple languages)
category | string | Category of the event
sourceClass | string | technical sourc e of the event
sourceInstance | string | Instance of the source (may be multiple)
sourceMessageId| int | internal MessageId
causality | string | reason of the alter

#### Example

Request

```text
GET /api/mmr-mobile/machinedata/machines/1234567890/alerts/history?daysBack=1
api-version: 2023-09-05
Accept-Language: de-DE
Authorization: Basic xxxx
tracestate: someinternaltracedata
```

Response (200 OK)

```text
Content-Type: application/json; charset=utf-8
```

```json
[
    {
        "startTime": "2024-07-12T14:44:15.4043838+02:00",
        "endTime": "2024-07-12T14:55:50.4438614+02:00",
        "instanceId": "1e3a2e60-8023-4a58-9da0-c41948d34a08",
        "machineNumber": "0-222-33-4444",
        "severity": 1000,
        "localizedSource": {
            "": "Control computer (Test)"
        },
        "localizedMessage": {
            "de-DE": "test pre umgebung",
            "en-US": "test pre environment",
            "it-IT": "test pre contexto"
        },
        "category": "Fault",
        "sourceClass": "LINUXERRORPIPE",
        "sourceInstance": "LINUXERRORPIPE_M1-C1",
        "sourceMessageId": 1,
        "causality": "Unknown"
    },
    .....
]
```


## Contribute

If you find anything, feel free to contribute to this repository. We are happy for every improvement ❤️.
