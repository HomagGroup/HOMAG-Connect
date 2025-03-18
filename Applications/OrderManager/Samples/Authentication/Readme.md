# Authentication / Authorization

To access the HOMAG orderManager Client Interface, a _HOMAG Connect orderManager_ license is required.

To create an instance of the client, you will need both a _subscription Id_ and an _authorization key_. 

```c#
// Create new instance of the orderManager client:
            
var client = new OrderManagerClient(subscriptionId, authorizationKey);
``` 

Both can be obtained by subscription administrators following these steps:

- Log into your tapio account at https://my.tapio.one.

- Copy the subscription Id from the browser bar.

![alt text](SubscriptionId.jpg "Subscription Id")

- Navigate to the orderManager in the applications section.

- Navigate to the HOMAG Connect orderManager **DETAILS** in the add-ons section.

![alt text](AuthorizationKey01.jpg)

- Open the "Authorization keys" dialog by clicking **EDIT**.

![alt text](AuthorizationKey02.jpg)

- Click on **+ Add** and **Confirm** to create a new authorization key. 
Enter a meaningful description for your authorization key so that you can easily identify the correct authorization key later if you wish to revoke it.

![alt text](AuthorizationKey03.jpg)

- Copy the generated authorization key by clicking **Copy**.

![alt text](AuthorizationKey04.jpg)

<strong>Note:</strong> Make sure to keep your access token confidential as it provides authorized access to the apps.

The file [AuthenticationSamples.cs](AuthenticationSamples.cs) provides several examples for authorization. 
