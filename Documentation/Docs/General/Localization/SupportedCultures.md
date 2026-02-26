# Supported UI Cultures API

This document describes the `/api/localizations/cultures` endpoint for retrieving all language codes supported by HOMAG Connect localization resources.

---

## Endpoint

```http
GET /api/localizations/cultures
```

## Description

Returns two-letter ISO 639-1 language codes for all cultures currently available in resource files.
The list is generated dynamically, so newly added resource cultures appear automatically.

**Example (C#):**

```csharp
using var client = new HttpClient
{
    BaseAddress = new Uri("https://connect.homag.cloud/")
};

var cultures = await client.GetFromJsonAsync<List<string>>("api/localizations/cultures");
```

---

## Responses

### 200 OK

```json
["de", "en", "fr", "it", "pt"]
```

### 401 Unauthorized

Missing or invalid credentials.

---

## Usage notes

- Use this endpoint to populate language selectors dynamically.
- Codes are normalized to two-letter ISO values.
- The list expands automatically when new resource files are added.

---

## Related

- [Enum Localisations API](EnumLocalizations.md)
- [Type Property Localisations API](TypePropertyLocalisations.md)
