# Copilot Instructions

## General Guidelines
- First general instruction
- Second general instruction
- Use absolute URLs for links.
- Use structured developer-friendly Bruno docs with action-oriented headlines, purpose, parameter tables, examples, error codes, and runnable C# and PowerShell samples. Ensure links are absolute GitHub type links. Avoid using the term 'gateway' and do not reference IIntelliDivideClient.

## Code Style
- Use specific formatting rules
- Follow naming conventions, including using a leading underscore for private fields in this codebase/workspace and for static readonly fields (e.g., _fieldName).
- Use `/// <inheritdoc />` for properties derived from an interface or base class instead of duplicating documentation when documenting contracts.
- When implementing deserialization-only private setters, prefer a StyleCop- and ReSharper-compatible implementation instead of an empty setter body.
- Use Newtonsoft.Json instead of System.Text.Json in this codebase when working on JSON serialization/deserialization.

## Project-Specific Rules
- Attributes should accept arrays of (string, int) pairs. Due to C# attribute parameter constraints, prefer repeatable attributes with a (string, int) constructor.
- In this codebase, do not use a Grain enum example value 'Longitudinal'; use actual existing Grain enum values only.
- Prefer returning Dictionary mappings instead of a list of key-value pairs for category-to-id lookup APIs when practical.