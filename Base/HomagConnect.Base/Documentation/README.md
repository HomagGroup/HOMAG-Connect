# HOMAG Connect Base

## Version history

Version | Date     | Comment 
--------|----------|---------
0.7     |31.08.2023| First Draft

## Introduction

HOMAG Connect is the central hub for external applications to communicate with HOMAG applications.
Each partner needs to register with HOMAG and tapio in order to use this communication interface.

### Terms

Term                      | Description
--------------------------|--------------------------------------------------------------------------
Homag Connect             | A central gateway for communicating with HOMAG applications. It is deployed worldwide on different regions. The call will automatically be routed to the nearest endpoint / region.
SAT                       | Subscription Access Token (similar to a PAT (Personal Access Token)).<br>It can be created in the tapio admin UI by the customer; also it can be revoked if needed.<br>This SAT must be created by the end customer and stored inside the partners application for this customer. It is then passed to the API gateway to authenticate the access.<br>In tapio this is also called `Authorization key`.
HOMAG partner id          | Each partner is assigned a unique partner id. This id must be passed in each call to the API gateway. It is mainly used to identify the calling application (it is more an information). The authentication is still done with the SAT.

### Partner / developer experience

The partner must register at HOMAG. Then the partner gets an partner id and can then start to call Homag Connect with this partner id and the SAT for the chosen application (MMR Mobile, intelliDivide, etc.), which was created within an tapio subscription.

### Customer experience

The customer need to buy the according license from tapio or HOMAG. It will then be assigned to his tapio subscription.

Then he need to go to the "Applications" section and create a SAT and copy this into the local application of the partner.

1. Go the the correct license and add a SAT

![tapio_sat01.png](./Assets/tapio_sat01.png)

2. Add an authorization key (SAT)

![tapio_sat02.png](./Assets/tapio_sat02.png)

3. You can enter a name, so that you can later identify this token better. This name is only for displaying and identifying the token

![tapio_sat03.png](./Assets/tapio_sat03.png)

4. After confirming, you must copy the generated token by pressing the "Copy" button. This token cannot be retrieved after you close this dialog.

![tapio_sat04.png](./Assets/tapio_sat04.png)

5. The customer must paste this SAT into the partner application.

## Customer Workflow and System Context

### Customer Workflow

<!---
```plantuml
@startuml puml_wf

actor CustomerAdmin as "Customer admin"

actor Customer

node "tapio" #Grey {
  component tapioAdminUI #LightGray
  component tapioApi #LightGray
}

component PartnerSoftware

node "HOMAG" #Blue {
component HomagApi as "HOMAG API Gateway" #LightBlue
component HomagInternalApi #LightBlue
}
tapioAdminUI <- CustomerAdmin : 1. Create SAT (authorization key)\nin tapio customer subscription

CustomerAdmin -> PartnerSoftware: 2. Paste SAT into partner app

Customer -> PartnerSoftware: 3. Do some action which\nrequires HOMAG API

PartnerSoftware -> HomagApi : 4. Call HOMAG API in behave\nof the customer subscription

tapioApi <- HomagApi: 5. Validate SAT
tapioApi -> HomagApi: 6. Return assigned\nsubscription
HomagApi -> HomagInternalApi: 7. Do the action
HomagApi <-- HomagInternalApi: 8. Return result

PartnerSoftware <-- HomagApi : 9. Return result

Customer <--- PartnerSoftware: 10. Display result

@enduml
```
--->

### System Context

![id](./Assets/puml_ctx.png)

<!---
```plantuml
@startuml puml_ctx

node Customer
node tapio #Grey 
node Partner
node HOMAG #Blue 

Customer -> tapio: \n*Register as customer\n*Create tapio subscription\n*Buy HOMAG software\n*Buy HOMAG partner connectivity\n*Create SAT for partner license

Partner -> HOMAG: \n*Register as partner\n*Call HOMAG gateway APIs

Customer -> Partner: \n*Uses partner software\n*Enter SAT in partner software
HOMAG -> tapio: \n*Validate SAT and return subscriptionId\nand application/licenseId

@enduml
```
--->

## Interface description

### Overview

#### Authentication

We use [basic authentication](https://swagger.io/docs/specification/authentication/basic-authentication/).
The username is the given from HOMAG, which is your `HOMAG partner id` and the password is the provided SAT from the customer in your application.

#### Additional header data

##### Additional data / tracing

We request the partners to provide some additional information in the headers (like software version / software features / ...), so we can track down problems easier, with this data. Be sure not send any data which are user-related to comply with GDPR.

We support the following headers for tracing:

* `traceparent`
* `tracestate`

For more information, please refer to the [W3C Trace Context - Level 1 specification](https://www.w3.org/TR/trace-context/).

### REST APIs

#### Host

* Production system =  `https://connect.homag.cloud`
* Test system =  `https://preview-connect.homag.cloud`

#### Error handling / response

If an error occur, we normally return the problem details as a json object in the response. This is done for all HTTP status codes >= 400.
The main properties are:

* title: A human readable title
* detail: An explanation of the error
* operationId: An reference to our internal logging system, so we can also find the corresponding request in order to dig down the problem

For more information, please refer to [RFC7807](https://tools.ietf.org/html/rfc7807).

### Interface overview

#### Homag Connect MMR Mobile interface
Follow the [link](/Applications/MmrMobile/Documentation/README.md#)

