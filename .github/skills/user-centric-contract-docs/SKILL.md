---
name: user-centric-contract-docs
description: 'Improve or review customer-facing documentation for HOMAG Connect contracts, including C# DTOs, enums, and interfaces. Use when adding or changing contract types, or when asked to improve, review, or validate XML docs, examples, defaults, constraints, or samples in Applications/*/Contracts and Base.'
---

# HOMAG Connect contract documentation

## Goal
Make every public contract type understandable for customers and integrators, not only HOMAG developers. XML documentation and attributes are the source of truth for NuGet XML files, IntelliSense, OpenAPI, and the automatically generated public Document360 "Contracts & Enums" pages. These outputs include summaries, examples, constraints, defaults, and enum descriptions.

## Scope
Cover public classes, interfaces, and enums in `Applications/*/Contracts` and `Base/HomagConnect.Base.Contracts`. Edit documentation, examples, and documentation metadata only. Never change declarations, property names, types, JSON names, or runtime behavior. Report suspicious defaults or constraints instead of silently changing them.

## Rules

### Summaries
- Describe the user-facing meaning and effect of a value, not its implementation. Never write "Gets or sets."
- For computed values, say what the value shows and which inputs it depends on.
- Distinguish related concepts, such as a specific board type and its shared material group.
- Mention units and unit-system dependencies, for example `<para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>`.
- State whether values are optional or computed and what happens when a value is omitted. Keep summaries to one or two sentences.
- Use `<see cref>` for related types and `<see href>` for useful external links.
- Use `<inheritdoc />` only when the inherited text accurately describes the documented member.

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
	- A `DateTimeOffset` without `[DisplayFormat]`: ask whether a culture-aware display format is expected. Keep serialized JSON examples in their wire format.
	- A unit-sensitive `double` without `[ValueDependsOnUnitSystem]`: ask which physical unit applies and whether values should change with the unit system. Do not assume every `double` is unit-dependent.
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
2. **Inspect before editing.** For each type, read its declarations, attributes, initializers, base types, interfaces, and existing documentation.
3. **Investigate suspicious patterns.** Use nearby contracts and existing code to determine whether a pattern is intentional. Ask focused questions when user or owner intent is needed; do not infer missing constraints or metadata.
4. **Edit documentation only.** Improve XML docs, examples, and documentation attributes while preserving declarations and runtime behavior. Do not change behavior-affecting attributes without explicit confirmation.
5. **Validate examples.** Check each example against its declared type, constraints, required properties, JSON names, and actual enum members.
6. **Cross-check contracts.** Compare defaults with initializers and examples; check `[Required]` against nullable (`?`) types without treating that combination as automatically wrong; verify constraint ranges, `<see cref>` targets, and XML validity.
7. **Build the contracts project** with documentation generation enabled. Fix relevant XML documentation warnings, including CS1570, CS1572, CS1574, and CS1573.
8. **Report results.** List changed files briefly, then report findings separately for issues not changed, including unresolved questions about defaults, constraints, unit metadata, display formats, nullable/required combinations, or enum descriptions.

## Output
Make edits directly and do not create extra Markdown files. End with a compact summary of the types and properties improved and any findings that require an owner decision.