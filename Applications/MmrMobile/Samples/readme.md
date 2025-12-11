# HOMAG MMR Mobile Client Samples

HOMAG MMR Mobile advanced gives you direct access to your machine data (counters, states) from MMR Mobile. You can then conveniently integrate this into your applications.
To help you get started, we have prepared a few examples that you can find below.

## Code samples

This folder contains a runnable console app, that allows you to test the various endpoints on HOMAG MMR Mobile Client 
Please proceed as follows
- Create a MmrMobileClient with the 3 required Informations
  -	SubscriptionId (from Tapio, See the URL in browser)
  - AuthorizationKey (from Tapio, Your private key to access your data) --> keep it confidential!
  - Base-URL : https://docs.homag.cloud/datenaustausch/homag-connect/connect-api for production system
- Use one of the methods of the client to fetch data (e.g. all states of the machine for the last 3 days)
- Proceed with these data for your analysis (group and summarize per day, calculate percentages ...)

Most of the function s are already used in the [MmrConsolApp](MmrConsole.cs)

## powerBI-Samples
For a intro on how to use the MMR Mobile API with powerBI please refer to [powerBI](../Documentation/powerBi/README.md)
You will find several prepared powerBI files 
- [CompanionSpec](https://reference.opcfoundation.org/Woodworking/v100/docs/) data of the machine [StatesAndCounters](./MachineData/MachineData.pbix)
- Alerts/Events [StatesAndCounters](./AlertsEvents/AlertList.pbix)
- State and counters of the machine [StatesAndCounters](./StatesAndCounters/StatesAndCounters.pbix)

## Excel Samples
For a intro on how to use the MMR Mobile API with Excel please refer to [Excel](../Documentation/Excel/README.md)
You will find prepared excel files 
- State and counters of the machine [StatesAndCounters](./StatesAndCounters/StatesAndCounters.xlsx)

## Postman Samples
For postman, we provide a collection and an (empty) environment [postman data](./Postman)
Please import both files, change the variables in the environment (with your user-data) and make this environment active.
Then you can begin to invoke the endpoints