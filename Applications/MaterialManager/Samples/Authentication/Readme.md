# Authorization

To access the HOMAG materialManager Client Interface, a HOMAG Connect materialManager License is required. The license can be purchased as an Add-on to materialManager in the tapio Marketplace.

To create an instance of the client, you will need both a Subscription Id and a Authorization Key. 

```c#
// Create new instance of the materialManager client:
            
var client = new materialManagerClient(subscriptionId, authorizationKey);
``` 

Both can be obtained by subscription administrators following these steps:

- Log in to your Tapio account at https://my.tapio.one.

- Copy the Subscription Id from the browser bar.

<b>Picture to be added</b>

- Navigate to the materialManager in the applications section.

- Navigate to the HOMAG Connect materialManager details in the Add-ons section.

<b>Picture to be added</b>

- Open the Authorization Keys dialog.

<b>Picture to be added</b>

- Click on Add and Confirm to create a new key. 
Enter a meaningful description for your authorization key so that you can easily identify the correct key later if you wish to revoke it.

<b>Picture to be added</b>

- Copy the generated Authorization Key

<b>Picture to be added</b>


<strong>Note:</strong> Make sure to keep your access token confidential as it provides authorized access to the apps.