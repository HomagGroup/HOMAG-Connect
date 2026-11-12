# HOMAG Connect materialAssist

The HOMAG Connect materialAssist Client streamlines the process of accessing the materialAssist REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.MaterialAssist.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.

## Getting started

### Authentication / Authoriziation

To begin using the HOMAG Connect materialAssist Client, you must first ensure that you have a valid HOMAG Connect materialManager License. The license can be purchased as an Add-on to materialManager in the tapio Marketplace.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the materialAssist client:
            
var client = new MaterialAssistClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](../../Documentation/Authentication/Readme.md) page for detailed instructions.

### Samples
More information on how to use HOMAG Connect materialAssist is located in the [Samples](./Samples/Readme.md) page.

### Required edgeband creation length

`MaterialAssistRequestEdgebandEntity.Length` is nullable to distinguish an unsupplied
length from explicit zero. It remains required for creation: missing or null values
fail local validation before HTTP, while supplied metric values from 0 to 2000 m
retain their existing validity. Update length remains optional for PATCH.

The public creation property changes from `double` to `double?`; consumers reading
it must handle null and gateway/backend consumers must adopt the aligned package
and verify their request validation and mappings. Null is not a valid creation
value and must not be silently converted to zero.



## Further details and explanations

- [Utilize Templates to Analyze Material Data with Excel](Samples/Statistics/Excel/Templates/Readme.md)<br>
