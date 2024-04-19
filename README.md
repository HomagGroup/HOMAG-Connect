<span style="color:red">[This is preliminary documentation and is subject to change.] </span>

# HOMAG Connect

The following repository contains the **HOMAG Connect Clients**, 

1. MMR Mobile <br> 
   [Documentation for MMR Mobile Client](/Applications/MmrMobile/Documentation/README.md) and [some samples](/Applications/MmrMobile/Samples/) for the usage.

2. intelliDivide <br> 
   [Documentation for intelliDivide Client](/Applications/IntelliDivide/Readme.md) and [some samples](/Applications/IntelliDivide/Samples/) for the usage.

3. materialManager <br>
   [Documentation for materialManager Client](./Applications/MaterialManager/Documentation/README.md) and [some samples](./Applications/MaterialManager/Samples) for the usage.

With these packages you can easily integrate different workflows of HOMAG applications into your own application. For further details and prerequisites for using the Homag Connect, please see the documentation of each application.

## Get your personal access token for your application
1. Get your personal access token from https://my.tapio.one

2. Choose the application you want to receive your personal access token for (MMR Mobile, materialManager etc.)

3. Select your subscription, select **Applications**, open your **desired application** and click on the applications **'HOMAG Connect Application' Add-on**.

4. Click on Authorization keys then **Edit** and click on **Add**. Insert a name for your token, confirm and copy the token to your clipboard.

## Getting started

1. Clone the repository.

~~~bash
git clone https://github.com/HomagGroup/HOMAG-Connect
cd homag-connect
~~~

2. Set up your environment.

    1. Copy *Applications/MmrMobile/Samples/CSharp/Tests/appsettings.json* to *Applications/MmrMobile/Samples/CSharp/Tests/appsettings.test.json*.

    ~~~bash
    cp Applications/MmrMobile/Samples/CSharp/Tests/appsettings.json Applications/MmrMobile/Samples/CSharp/Tests/appsettings.test.json
    ~~~

    2. Insert your access token and subscription Id in the *Applications/MmrMobile/Samples/CSharp/Tests/appsettings.test.json*. It should look like below.

    ~~~json
    {
        "HomagConnect": {
        "BaseUrl": "https://connect.homag.cloud",
        "MmrMobile": {
            "SubscriptionId": "", // Enter your subscriptionId for your requests
            "Token": "" // Use your personal access token from tapio related to this application
            }
        }
    }
    ~~~

3. Build the solution.

~~~bash
dotnet build
~~~

4. Run tests.

~~~bash
dotnet test
~~~


## Contribute

If you find anything, feel free to contribute to this repository. We are happy for every improvement ❤️.
