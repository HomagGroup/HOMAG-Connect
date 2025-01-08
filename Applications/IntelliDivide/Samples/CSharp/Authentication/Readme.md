# Authentication / Authorization

To access the HOMAG IntelliDivide Client Interface, a HOMAG Connect IntelliDivide License is required. This license is included in the IntelliDivide Advanced and Premium licenses.

To create an instance of the client, you will need both a Subscription Id and a Authorization Key. 

```c#
// Create new instance of the intelliDivide client:
            
var client = new IntelliDivideClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](https://redirect.homag.cloud/homagconnect_authentication) page for detailed instructions.

<strong>Note:</strong> Make sure to keep your access token confidential as it provides authorized access to the apps.

The file [AuthenticationSamples.cs](AuthenticationSamples.cs)  provides several examples for authorization. 