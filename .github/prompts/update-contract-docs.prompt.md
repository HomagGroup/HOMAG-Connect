---
name: "Update C# Contract Documentation"
description: "Improve XML summaries, examples, and verified OpenAPI defaults across the repository's C# contracts."
argument-hint: "Optional: specify projects, namespaces, or types; otherwise review all C# contract projects."
agent: "agent"
---

Review and update XML documentation for C# contract models across the repository. First identify contract projects and source areas, including `Applications/**/Contracts`, `Base/**`, `DataExchange/**`, and other projects named or organized as contracts. Unless the user narrows the scope, process all identified contract types: class summaries, public property summaries, and enum summaries. Do not stop after editing only the currently open file.

Before editing:
- Read the repository's `.github/copilot-instructions.md` and any closer applicable instructions. Inspect each target type, its property types, enum declarations/converters, initializers, and relevant nearby behavior.
- Form a local hypothesis about intended meaning and defaults, then verify it against code or authoritative documentation. Do not infer business semantics from a property name alone.
- Search existing docs links and, when needed, confirm the exact matching page on the relevant official documentation site. Never invent a URL.

Documentation rules:
- Write concise, user-facing summaries that explain what a class or property represents and why it matters in its domain. Do not use accessor boilerplate such as “Gets or sets”. Explain computed values by describing what they show and their relevant inputs.
- Keep class summaries brief (usually 1–3 sentences); do not repeat the generated property table. Add an XML `<see href="...">display text</see>` link only when a matching conceptual page has been confirmed. Otherwise improve the wording without a link.
- Preserve existing XML tags and their organization unless a targeted correction is needed. Do not change property names/types, requiredness, serialization attributes, validation, JSON order, or enum numeric values.

Example and default validation:
- Consult the matching `docs.homag.cloud` conceptual or tutorial pages for meaningful, domain-authentic example values whenever such documentation exists. Prefer documented examples over invented placeholders, but validate every sourced value against the current declared type and contract before using it.
- Check every `<example>` against the declared type and actual contract. For enum examples, use actual member names as serialized by the enum converter; do not substitute display labels or assumed terms.
- Validate class-level JSON examples against real serialized member names and types. Remove unsupported fields and ensure representative values agree with property examples, confirmed documentation, and established domain conventions. If no relevant documentation exists, use verified project samples/tests rather than making up domain-specific values.
- Determine enum defaults per property from explicit initializers, constructors, and verified behavior. Do not assign defaults to every enum or treat a zero-valued member as a meaningful business default without evidence.
- When a default is intentional and verified, expose it with `[DefaultValue(EnumType.Member)]`; make a property initializer explicit only when it preserves verified existing runtime behavior. Document the fallback in the summary when useful. Do not invent a default to make the schema look complete.

After editing:
- Review the diff to confirm changes are limited to the intended XML documentation and verified default metadata/initializers.
- Run focused diagnostics or tests available for the touched contracts, plus `git diff --check`.
- Report which projects or areas were updated, any concepts/defaults that could not be verified, and validation results. Do not claim that OpenAPI emits defaults unless the generated schema has been checked.