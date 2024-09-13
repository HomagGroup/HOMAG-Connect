# Authentication / Authorization

In order to access a HOMAG Connect application via the REST API, you need to have a licence for the relevant application and the necessary authentication details, which include the Subscription Id (username) and an Authorization Key (password).

The license can be purchased as an Add-on to the application in the tapio Marketplace.

The Subscription Id and Authorization Key can be obtained by subscription administrators following these steps:

- Log in to your Tapio account at https://my.tapio.one.

- Copy the Subscription Id from the browser bar.

![alt text](SubscriptionId.jpg "Subscription Id")

- Navigate to the respective application in the applications section.

- Navigate to the HOMAG Connect details in the Add-ons section.

![alt text](AuthorizationKey01.jpg)

- Open the Authorization Keys dialog.

![alt text](AuthorizationKey02.jpg)

- Click on Add and Confirm to create a new key. 
Enter a meaningful description for your authorization key so that you can easily identify the correct key later if you wish to revoke it.

![alt text](AuthorizationKey03.jpg)

- Copy the generated Authorization Key

![alt text](AuthorizationKey04.jpg)

<strong>Note:</strong> Make sure to keep your access token confidential as it provides authorized access to the apps.