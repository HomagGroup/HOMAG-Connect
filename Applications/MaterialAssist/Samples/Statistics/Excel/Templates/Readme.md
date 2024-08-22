# Utilize Templates to Analyze Material Data with Excel

The data retrieved from the materialManager and materialAssist API can be used to create comprehensive reports within Excel.  

We have developed templates that can be used as a basis for creating your own customized reports.

## Templates

### Board disposal candidates

English: [Candidates for disposal (Boards).xlsx](DisposalCandidates/Candidates%20for%20disposal%20(Boards).xlsx)<br>
Deutsch: [Kandidaten für die Entsorgung (Platten).xlsx](DisposalCandidates/Kandidaten%20für%20die%20Entsorgung%20(Platten).xlsx)

The report lists all board entities sorted by their age and the by the last used date of the material. Entities at the top of the list are candidates for disposal as the changes to use them are low. They consume only space in the storage.

![alt text](Images/Candidates%20for%20disposal%20(Boards).png)

## How to use the templates

Download the template you want to use.

![alt text](Images/Excel-Templates-Download-de.png)

Open the template in Excel and activate editing.

![alt text](Images/Excel-Templates-Activate-de.png)

In the ribbon section data click on <strong>Refresh all</strong> to retrieve the data from the API.

![alt text](Images/Excel-Templates-Refresh-de.png)

Enter Subscription Id (1) and Authentication Key (2). Please check the [Authorization page](../../../Authentication/Readme.md) for more information about how to get them.

![alt text](Images/Excel-Templates-Authentication-de.png)

The excel file is now connected to your subscription and ready to use. 

The authentication informationen is stored in the Excel file. When you open the file again, you can refresh the data without entering the authentication information again.

