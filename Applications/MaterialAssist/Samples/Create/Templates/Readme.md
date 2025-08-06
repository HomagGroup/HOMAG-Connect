# Create template entity

With the HOMAG Connect materialAssist board client, template entities can be created. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var templateEntityRequest = new MaterialAssistRequestTemplateEntity
            {
                Id = "42",
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                Comments = "This is a comment",
                Length = 1000,
                Width = 50,
                Quantity = 5,
            };
            var newTemplateEntity = await client.CreateTemplateEntity(templateEntityRequest);
            Console.WriteLine($@"Created template entity: {newTemplateEntity.Id}");
```

