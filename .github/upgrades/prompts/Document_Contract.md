# Custom Prompt: `Document_Contract`

## Objective
Document a .NET contract type for API consumers, with backend integration in mind.

## Audience
Write for developers integrating the API into backend systems.
Documentation is consumed through OpenAPI and Scalar.

## Required Output
Update XML documentation (`///`) for the selected contract type and its public properties.

## Rules
1. Keep wording concise and implementation-aligned.
2. Use clear, integration-focused terms.
3. Add `<example>` tags for each important public property.
4. Document units explicitly when values depend on unit system:
   - state Metric vs Imperial behavior clearly.
5. Preserve existing nullability, attributes, and serialization behavior.
6. Do not change runtime logic unless explicitly requested.

## Unit Documentation Guidance
- If a property is percentage-based, document unit as `%` for both Metric and Imperial.
- If a property has `[ValueDependsOnUnitSystem(...)]`, describe units for:
  - `UnitSystem.Metric`
  - `UnitSystem.Imperial`

## Example Style
- Property example: `/// <example>148.95</example>`
- Object example:
  
  `/// <example>`
  `/// { "materialCode": "P2_Gold_Craft_Oak_19.0", "wasteWithOffcutsByBoard": 32.41 }`
  `/// </example>`

## Done Checklist
- [ ] Class summary is accurate.
- [ ] Public properties are documented for API integrators.
- [ ] `<example>` tags are included.
- [ ] Metric/Imperial unit behavior is documented where applicable.
- [ ] No unintended logic/serialization changes were introduced.
