# HOMAG MMR Mobile advanced

HOMAG MMR Mobile advanced gives you direct access to your machine data (counters, states) from MMR Mobile. You can then conveniently integrate this into your applications.
To help you get started, we have prepared a few examples that you can find below.

## Getting started

### Authoriziation

To begin using the HOMAG MMR Mobile advanced, you must first ensure that you have a valid HOMAG MMR Mobile advanced License. 

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the MmrMobileClient client:
            
var client = new MmrMobileClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authorization](./Samples/Authentication/README.md) page for detailed instructions.<br />
For a runnable sample, please visit the [MmrConsolApp](Samples/MmrConsole.cs) page or start the C# project nearby.<br />
For a technical description of all endpoints regarding "HOMAG MMR Mobile advanced", please visit the [Endpoints](./Client/README.md) page.

### Perform a query

#### Sample: Query detail states of the last 3 days of all of your machines machine

Create a client to access the api
```c#
var client = new HttpClient
{
    BaseAddress = new Uri("https://connect.homag.cloud")
};

// get the 2 values from tapio. See paragraph authentication
string subscriptionId = "*** get from Tapio ***";
string authorizationKey = "*** get from Tapio ***";
var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

var mmrMobileClient = new MmrMobileClient(client);

```

Use the client to invoke any of the prepared functions
Hint: See all the parameters of the function in the [Endpoints](Client)-documentation
```c#

// hint: have a look at the various filters, this method GetStateData provides
var states = await mmrMobileClient.GetStateData(from: DateTime.Now.AddDays(-3), to: DateTime.Now );

```

## Further details and explanations

For a detailed examples and explanations, please refer to [HOMAG MMR Mobile advanced Samples](./Samples/README.md).

