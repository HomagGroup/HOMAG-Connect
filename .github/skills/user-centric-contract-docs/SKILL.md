---
name: user-centric-contract-docs
description: 'Improve or review customer-facing documentation for HOMAG Connect contracts, including C# DTOs, enums, interfaces, and exact-match shared interface reuse. Use when adding or changing contract types, or when asked to improve, review, or validate XML docs, examples, defaults, constraints, inherited metadata, or samples in Applications/*/Contracts and Base.'
---

# HOMAG Connect contract documentation

## Goal
Make every public contract type understandable for customers and integrators, not only HOMAG developers. XML documentation and attributes are the source of truth for NuGet XML files, IntelliSense, OpenAPI, and the automatically generated public Document360 "Contracts & Enums" pages. These outputs include summaries, examples, constraints, defaults, and enum descriptions.

## Scope
Cover public classes, interfaces, and enums in `Applications/*/Contracts` and `Base/HomagConnect.Base.Contracts`. Edit documentation, examples, and documentation metadata. The only permitted declaration/metadata refactoring is exact-match shared interface reuse under the rules below. Otherwise, never change declarations, property names, types, JSON names, or runtime behavior. Report suspicious defaults or constraints instead of silently changing them.

## Rules

### Summaries
- Describe the user-facing meaning and effect of a value, not its implementation. Never write "Gets or sets."
- For computed values, say what the value shows and which inputs it depends on.
- Distinguish related concepts, such as a specific board type and its shared material group.
- Mention units and unit-system dependencies, for example `<para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>`.
- State whether values are optional or computed and what happens when a value is omitted. Keep summaries to one or two sentences.
- Use `<see cref>` for related types and `<see href>` for useful external links.
- Use `<inheritdoc />` only when the inherited text accurately describes the documented member.

### Shared interfaces and inherited metadata
- Check existing shared interfaces, including their inherited members, before documenting properties. Automatically implement the most specific compatible interface without asking only when every affected member has the same business meaning, name, type, accessors, nullability, and matching metadata.
- Require a 100% match for each attribute supplied by the interface: attribute type, constructor arguments, and effective named/default values. Compare range bounds and operand types, display resource types/keys, units, and rounding precision. An absent class attribute, a stricter range, or a different display key is not an exact match; do not infer equivalence.
- Keep the concrete properties. Remove only exactly duplicated interface-provided attributes, such as `[Range]`, `[Display]`, and `[ValueDependsOnUnitSystem]`, and replace duplicated summaries/examples with `<inheritdoc />`. Preserve class-specific attributes, including `[Required]`, JSON serialization attributes, defaults, and any operation-specific documentation.
- Verify that adding the interface introduces no other rules or behavior, and that affected validation, conversion, localization, serialization, and generated-documentation paths already resolve interface metadata. C# does not automatically inherit interface property attributes. Do not remove attributes when support is missing or unverified.
- Trace the actual validation call sites, not just the available helpers. `HomagConnect.Base.Extensions.DataAnnotationsValidator.TryValidateObjectRecursive` checks interface constraints for whole objects; `HomagConnect.Base.Contracts.Extensions.PropertyValidation` supports interface-aware flat-object and single-property validation. Standard `System.ComponentModel.DataAnnotations.Validator.TryValidateObject` and `TryValidateProperty` do not inspect interface attributes. An interface-aware helper existing elsewhere is not evidence that a call site uses it.
- Preserve validation scope: do not replace single-property patch or sparse-import validation with whole-object recursive validation, which can reject intentionally omitted fields. If a call site uses an interface-unaware validator, keep the concrete attributes and report that prerequisite; do not modify infrastructure as part of this skill.
- If any member or consumer fails these checks, leave the candidate interface adoption and its property metadata unchanged. Report the near match and exact reason, for example: "Skipped IDimensionProperties: Thickness minimum is 0.1 on the class but 0.01 on the interface; automatic replacement could change validation."
- Do not change property types/nullability, shared interfaces, or infrastructure to force a match. Report required follow-up work instead. Add or update focused tests for successful reuse: prove the attributes are absent from the concrete property, invalid values still fail through the actual operation, boundaries and null/omission behavior are preserved, and unit conversion, localized labels/units, and serialization still work where applicable.

### Examples
- Add a class-level `<example>` for every class. Add property-level examples when they clarify the property's wire value; avoid redundant or misleading examples, especially for complex properties.
- Enum examples must use an actual enum member name as serialized by the JSON converter, never a display label or invented business term.
- `DateTimeOffset` examples must use ISO 8601 wire format, including an offset when applicable, even when the property has `[DisplayFormat]`. `[DisplayFormat]` affects presentation and does not replace the serialized example format.
- Class examples must be valid JSON using the actual wire names (camelCase unless `[JsonProperty(Name)]` specifies otherwise). Include required properties, existing members only, and values with matching types and formats, such as ISO 8601 dates and numeric JSON numbers.
- Keep examples consistent: use the same board, order, or code across the class example and its property examples.

### Constraints and defaults
- Treat validation attributes such as `[Required]`, `[StringLength]`, `[Range]`, and `[RegularExpression]` as the source of truth for generated constraints. Verify that documentation matches them, but do not repeat attribute-derived constraints in summaries when the generated documentation already exposes them. Add prose only for non-obvious implications or limits that are not otherwise surfaced. Explain `[Obsolete("use X instead")]` and `[DisplayFormat]` only when their user-facing effect is not clear from the generated documentation.
- Verify that `[DefaultValue(...)]` matches an actual runtime default and the property initializer. Do not repeat attribute-derived defaults in summaries when the generated documentation already exposes them; describe omission behavior only when it adds information beyond the metadata.
- Treat suspicious defaults as findings. For example, a first enum member such as `AcrylicCompositeMaterials` may be accidental when `Undefined` exists or the example uses `Chipboard`. Do not describe a suspicious value as intended.
- An implicit enum zero value is not a business default. Do not add `[DefaultValue]` for it.

### Suspicious patterns and questions
- Treat these patterns as signals to investigate, not automatic defects:
	- An entity-like class (a resource that is returned, listed, patched, or referenced by an identifier, such as a board type, order, or lot) without a `[Key]` property: ask which property, or combination of properties, uniquely identifies an entry, and whether it should carry `[Key]`. `System.ComponentModel.DataAnnotations.KeyAttribute` is the standard marker; use one `[Key]` per property of a composite key. The generated documentation shows it as "Key (unique identifier)" in the property details and names the key above the property table, so customers can see how to address an entry. Do not ask for pure request, parameter, filter, event-payload, or value-object classes unless they are addressed by an identifier. Check that a `[Key]` property is also `[Required]` and not nullable, and ask when it is not; never guess the key property.
- A `DateTimeOffset` without `[DisplayFormat]`: ask whether a culture-aware display format is expected. Keep serialized JSON examples in their wire format.
	- A unit-sensitive `double` without effective `[ValueDependsOnUnitSystem]` metadata on the class or its interfaces: ask which physical unit applies and whether values should change with the unit system. Do not assume every `double` is unit-dependent.
	- A `string` without `[StringLength]`: ask whether a maximum length exists elsewhere or is intentionally unrestricted. Never invent a limit.
	- A nullable property with `[Required]`: explain that the C# type permits null while validation rejects null, then ask whether that distinction is intentional. Do not assume this is a mismatch.
- Ask a focused question before making a documentation decision that depends on unknown intent. Do not add or change constraints, defaults, unit-conversion metadata, or display formats based on a guess; these may change validation, conversion, or presentation behavior.
- If clarification is unavailable, leave the uncertain contract unchanged and report the pattern as a finding, including the specific question an owner should answer. Continue with unrelated documentation work that does not depend on that answer.

### Enums
- Give every member a customer-facing description using `[Display(Description = "...")]` or an XML summary. Explain when to use the member; do not rely on its name alone.
- Explain what the enum classifies and what `Undefined` or `None` means, when present.

### Names and language
- Keep type and property names stable. Never rename for documentation reasons.
- Use Pascal-style property names in prose, and the correct serialized spelling in examples.
- Write in English, present tense, and full sentences. Avoid internal jargon, unexplained abbreviations, and ticket numbers.
- Do not mention products, customers, tenants, subscription IDs, or secrets.
- Do not describe endpoints in type documentation; the "Used by" section is generated.

## Workflow
1. **Scope the change.** Work on the application or folder named by the user. If none is named, use the provided files or current diff; ask a concise question only when the scope cannot be determined.
2. **Inspect before editing.** For each type, read its declarations, attributes, initializers, base types, interfaces, and existing documentation. Check exact-match shared interface reuse by default using the rules above.
3. **Investigate suspicious patterns.** Use nearby contracts and existing code to determine whether a pattern is intentional. Ask focused questions when user or owner intent is needed; do not infer missing constraints or metadata.
4. **Improve documentation and reuse exact-match interfaces.** Improve XML docs, examples, and documentation attributes. Apply interface reuse only when all checks above pass; otherwise preserve declarations and behavior-affecting attributes.
5. **Validate examples.** Check each example against its declared type, constraints, required properties, JSON names, and actual enum members.
6. **Cross-check contracts.** Compare defaults with initializers and examples; check `[Required]` against nullable (`?`) types without treating that combination as automatically wrong; check that entity-like classes have a `[Key]` (and ask when one is missing, see above); verify constraint ranges, `<see cref>` targets, and XML validity.
7. **Build the contracts project** with documentation generation enabled. Fix relevant XML documentation warnings, including CS1570, CS1572, CS1574, and CS1573. When reusing interfaces, also run the focused regression tests described above.
8. **Report results.** List changed files and adopted interfaces briefly, then report skipped near matches with their exact mismatches or missing consumer support. Report other findings separately, including unresolved questions about defaults, constraints, keys, unit metadata, display formats, nullable/required combinations, or enum descriptions.

## Output
Make edits directly and do not create extra Markdown files. End with a compact summary of the types and properties improved and any findings that require an owner decision.