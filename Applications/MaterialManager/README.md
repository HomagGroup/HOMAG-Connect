# HOMAG materialManager Client

The HOMAG materialManager Client enables access to the materialManager application for integration into your own applications. The functions offered on the website are available for a programmatic integration.
The client is also available as a NuGet package on nuget.org and can be smoothly integrated into .NET applications.

The HOMAG materialManager Client is written in C# ([Source Code](./Client/MaterialManagerClient.cs)). It facilitates access to the materialManager REST API. The source code can be used to convert the relevant functions for integration into another programming language.

The most important functions are described in the Documentation below, more can be found [here](./Samples/Readme.md) and in the [Source Code](./Client/MaterialManagerClient.cs) on GitHub.

## Getting started

### Authentication / Authoriziation

To begin using the HOMAG Connect materialManager Client, you must first ensure that you have a valid HOMAG Connect materialManager License. The license can be purchased as an Add-on to materialManager in the tapio Marketplace.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the materialManager client:
            
var client = new materialManagerClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](../../Documentation/Authentication/Readme.md) page for detailed instructions.

## Local material validation

MaterialManager and MaterialAssist creation/update methods check mapped static
constraints before sending requests. This includes file-bearing type requests,
allocation updates, MaterialAssist storage dimensions and supplied import fields.
Invalid values produce `ValidationException` with the public property and limit;
nested errors include the collection index. GET responses are not validated.
Nullable update fields remain optional and merge-patch validates supplied properties
without requiring unrelated creation fields.

The numeric annotations use **metric bounds only**, inclusively:

| Property | Metric range |
| --- | --- |
| Board length / width, offcut and template dimensions | 0.1..19999.9 mm |
| Board thickness | 0.1..999.9 mm |
| Board density | >=0.1 kg/m3, no upper bound |
| Board / edgeband costs | 0.1..99999.99 in the existing monetary convention |
| Edgeband default / instance / stored length | 0..2000 m |
| Edgeband height | 0.1..999.9 mm |
| Edgeband thickness / current thickness | 0.01..99.99 mm |
| MaterialAssist board / edgeband quantity | 1..9999 |
| Edgeband allocated length | 0..99999 m |
| Edgeband length warning threshold | 0..999999.99 m |

Existing constrained text limits are also applied to read-model properties used
as patch targets. Advisory characters are not made into blocking regex rules.
Board allocation quantity uses its own minimum of one, not the inventory maximum.
No `Available` constraint is inferred for an allocation's `UsedLength`.
Texture dimensions retain their existing nonnullable types and unit metadata,
without new range checks. Omitted dimensions still serialize as zero; optional
texture-dimension validation is deferred to avoid breaking existing requests.

`ValueDependsOnUnitSystem` describes conversion/display metadata; it does **not**
convert `Range` bounds. Imperial validation is not fixed: for example, converting
20000 mm to approximately 787.4016 inches can incorrectly pass a metric length
range; converting 1.27 mm to approximately 0.05 inches can incorrectly fail it.
No request unit-system fields, automatic normalization or formatting changes were
introduced. Keep using the endpoint's existing unit convention.

Inclusive local lower bounds can differ from stricter backend comparisons. Costs
and inventory quantities also have narrower local cloud-metadata bounds than some
backend rules. A local pass means only eligibility for submission, not guaranteed
service acceptance: uniqueness, references, inventory and other state-dependent
checks remain authoritative on the service.

Imports validate only mapped supplied fields. Missing nullable dimensions and
unused response metadata are not demanded; nonpositive import quantities retain
their cleanup semantics. The existing import `UnitSystem` field does not make
these checks unit-aware. URI internals and open extension dictionaries are not
recursively validated.

### Compatibility and maintenance

These are intentional earlier rejections of previously unchecked input. SDK,
gateway and service consumers must coordinate package adoption. No new validation
annotations remain on the shared `AdditionalDataTexture` type. Default flat
validation for unrelated clients is unchanged.

Material-domain maintainers own the source-to-property mappings and discrepancy
review; SDK maintainers own annotation execution and package compatibility. For
each authoritative definition change:

1. Review the material/operation mapping, units, warning severity and requiredness.
2. Update the copied annotation and the affected focused boundary/transport tests.
3. Run `MaterialConstraintAnnotationTests`, `MaterialUnitMetadataTests`,
   `MaterialMutationValidationTests` and `MaterialConstraintCoverageTests` in both
   material test projects; keep the imperial characterization cases.
4. Build all existing library target frameworks.
5. Release through the normal approved process and verify private drift checks
   against that exact adopted public package revision. Public-checkout tests alone
   do not detect a change in private authoritative definitions.

## Further details and explanations

1. [Samples](Samples/Readme.md)
2. [Excel (analog to MmrMobile)](../MmrMobile/Documentation/Excel/README.md)
3. [powerBI (analog to MmrMobile)](../MmrMobile/Documentation/powerBi/README.md)
