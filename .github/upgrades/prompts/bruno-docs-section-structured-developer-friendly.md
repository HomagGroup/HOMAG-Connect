# Custom Prompt: Update Bruno `docs` Section (Structured, Developer-Friendly)

## Objective
Update the active Bruno request file’s `docs { ... }` block with clear, implementation-aligned API documentation.

## Required Structure
Use this exact section order:

1. **Action-oriented headline**
2. **Endpoint** (must include full URL, e.g. `https://connect.homag.cloud/...`)
3. **Purpose**
4. **Parameters** (path/query/body as applicable)
5. **Request example**
6. **Success response example**
7. **Possible error codes**
8. **C# example** (runnable, with placeholders)
9. **PowerShell example** (runnable, with placeholders)
10. **Referenced types (GitHub)** (absolute links)

## Formatting Rules
- Keep wording concise and developer-focused.
- Keep examples realistic and copy/paste friendly.
- Use markdown tables for parameters.
- Keep code snippets consistent with endpoint behavior.

## Constraints
- Do **not** use the term `gateway`.
- Do **not** reference `IIntelliDivideClient`.
- Prefer **`list`** wording over `discover`.
- Use **absolute URLs** for links.

## Source of Truth for Behavior
Derive parameters/error behavior from controller implementation where available:
- `IntelliDivideController.cs`
- `LocalizationsController.cs`
- `ProductionManagerController.cs`

## Link Format
Use absolute GitHub URLs in this form:

`https://github.com/HomagGroup/HOMAG-Connect/blob/main/<path>/<TypeName>.cs`

## Done Checklist
- [ ] `docs` section uses the required 10-part structure.
- [ ] Endpoint includes `https://connect.homag.cloud`.
- [ ] Error codes align with controller logic.
- [ ] C# sample is runnable with placeholders.
- [ ] PowerShell sample is runnable with placeholders.
- [ ] Referenced types are absolute GitHub links.
- [ ] No `gateway` term.
- [ ] No `IIntelliDivideClient` reference.
- [ ] Prefer `list` wording.
