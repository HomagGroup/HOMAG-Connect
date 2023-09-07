# HOMAG MMR Mobile Client

HOMAG Connect MMR Mobile gives you direct access to your machine data (counters, states) from MMR Mobile. You can then conveniently integrate this into your applications.
To help you get started, we have prepared a few examples that you can find below.

## Version history

Version   | Date     | Comment 
----------|----------|------------
1.0.0     |07.09.2023| First Draft

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
var states = await mmrMobileService.GetStateData(subscriptionId);
var counters = await mmrMobileService.GetCounterData(subscriptionId);
Console.WriteLine($"You got {states.Count()} states and {counters.Count()} counter for the last 14 days")
~~~

~~~bash
dotnet run
~~~

## Homag Connect MMR Mobile interface overview

Name           | Method | API                                                                                                                                                                                                                               | Usage 
---------------|--------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------
GetStateData   |GET     |`api/{subscriptionId}/mmr/`<br/>`states?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&stateId={stateId}`<br/>`&detailedStateId={detailedStateId}`| Returns all state data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.
GetCounterData |GET     |`api/{subscriptionId}/mmr/`<br/>`counter?from={from}&to={to}`<br/>`&machineNumber={machineNumber}`<br/>`&instanceId={instanceId}`<br/>`&machineType={machineType}`<br/>`&counterId={counterId}`                                    | Returns all counter data for the asked time window (default: 14 days) for all machines assigned to the subscription, if not asked specifically.

## Details
### GetStateData
#### Input       
Parameter                    | Type     | Description                                        
-----------------------------|----------|---------------------------------------------------
subscriptionId *(Required)*  | string   | The id of the subscription 
from *(Optional)*            | DateTime | DateTime that the search should start from         
to *(Optional)*              | DateTime | DateTime that the search should end                
machineNumber *(Optional)*   | string   | Number of the machine (Format: x-xxx-xx-xxxx)                              
instanceId *(Optional)*      | string   | The id of the instance                             
machineType *(Optional)*     | string   | Type of machine                                    
stateId *(Optional)*         | string   | Id of the state                                    
detailedStateId *(Optional)* | string   | Id of the detailed state                             

#### Output
Property          | Type     | Description
------------------|----------|-----------------------------------------------
Machine Number    | string   | Number of the machine
Machine Name      | string   | Name of the machine
Machine Type      | string   | Type of machine
Timestamp         | DateTime | Day when the data was gathered
Duration [h]      | double   | Time that the machine spent in the state in hours 
Instance Id       | string   | Id of the instance
Detailed State Id | string   | Id of the detailed state 
Detailed State    | string   | Detailed state translated into the requested language
State Id          | string   | Id of the state 
State             | string   | State translated into the requested language

#### Example

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

### GetCounterData
#### Input
Parameter                   | Type     | Description                                                        
----------------------------|----------|-------------------------------------------
subscriptionId *(Required)* | string   | The id of the subscription                                          
from *(Optional)*           | DateTime | DateTime that the search should start from 
to *(Optional*)             | DateTime | DateTime that the search should end         
machineNumber *(Optional)*  | string   | Number of the machine (Format: x-xxx-xx-xxxx)                                               
instanceId *(Optional)*     | string   | The id of the instance                                              
machineType *(Optional)*    | string   | Type of machine                                                     
counterId *(Optional)*      | string   | Id of the counter                                                     
   
#### Output      
Property       | Type     | Description                                    
---------------|----------|------------------------------------------------ 
Machine Number | string   | Number of the machine                          
Machine Name   | string   | Name of the machine                            
Machine Type   | string   | Type of machine                                
Timestamp      | DateTime | Day when the data was gathered                
Value          | double   | Output value                                   
Instance Id    | string   | Id of the instance                             
Counter Id     | string   | Id of the counter                           
Counter        | string   | Counter translated into the requested language 

#### Example

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
