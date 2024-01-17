# HOMAG Connect Base

## Version history

Version   | Date     | Comment 
----------|----------|---------
1.0.0     |07.09.2023| First Draft
1.1.0     |27.10.2023| Updating documentation

## Introduction

HOMAG Connect is the central hub for external applications to communicate with HOMAG applications.
Each partner needs to register with HOMAG and tapio in order to use this communication interface.

### Terms

Term                      | Description
--------------------------|--------------------------------------------------------------------------
HOMAG Connect             | A central gateway for communicating with HOMAG applications. It is deployed worldwide on multiple regions. The call will automatically be routed to the nearest available endpoint / region.
SAT                       | Subscription Access Token (similar to a PAT (Personal Access Token)).<br>It can be created in the tapio admin UI by the customer; also it can be revoked if needed.<br>This SAT must be created by the end customer and stored inside the partners application for this customer. It is then passed to HOMAG Connect to authenticate the access.<br>In tapio this is also called `Authorization key`.
HOMAG subscription id     | The subscription id must be passed in each call to HOMAG Connect. It is mainly used to identify the calling application (it is more an information). The authentication is still done with the SAT.

## Interface description

### Overview

#### Authentication

We use [basic authentication](https://swagger.io/docs/specification/authentication/basic-authentication/).
The username is the given from HOMAG, which is your `HOMAG subscription id` and the password is the provided SAT from the customer in your application.

#### Rate Limiting

To control the rate of requests send or received by a network interface controller, rate limiting was implemented. The current maximum for calls of a controller differ per application.
Afterwards you will receive a **HTTP 429: Too many request** as response.

#### Additional header data

##### Versioning

You should provide a `api-version` header, so we known, which version of the API you want to call. If this header is not present, we assume always the latest version.
The current version is `2023-10-27`.

##### Locale

You should provide the [`accept-language`](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Accept-Language) header, so you also receive possible error messages or other localized texts in the preferred language.

##### Additional data / tracing

We request the partners to provide some additional information in the headers (like software version / software features / ...), so we can track down problems easier, with this data. Be sure not send any data which are user-related to comply with GDPR.

We support the following headers for tracing:

* `traceparent`
* `tracestate`

For more information, please refer to the [W3C Trace Context - Level 1 specification](https://www.w3.org/TR/trace-context/).

### REST APIs

#### Host

* Production system =  `https://connect.homag.cloud`
* Test system =  `https://connect-preview.homag.cloud`

#### Error handling / response

If an error occur, we normally return the problem details as a json object in the response. This is done for all HTTP status codes >= 400.
The main properties are:

* title: A human readable title
* detail: An explanation of the error
* operationId: An reference to our internal logging system, so we can also find the corresponding request in order to dig down the problem

For more information, please refer to [RFC7807](https://tools.ietf.org/html/rfc7807).

### Interface overview

#### HOMAG Connect MMR Mobile interface
Follow the [link](/Applications/MmrMobile/Documentation/README.md#homag-connect-mmr-mobile-interface-overview)

