# HOMAG Connect OrderManager Samples

- [Authentication / Authorization](../../../Documentation/Authentication/Readme.md)

- Import an order
	- Using a structured file (.Zip)
		- [Start import job for an order](CSharp/Orders/Import/Readme.md)
		- [Get import status](CSharp/Orders/Import/Readme.md#utilize-the-outcome-of-the-import)
		- [Wait for import completion](CSharp/Orders/Import/Readme.md#wait-for-the-import-to-be-completed)
	- Using an OrderDetails object (optionally with file references)
		- [Import order using OrderDetails](CSharp/Orders/Import/Readme.md)

- Adding or updating groups within an existing order
	- [Add or update group using file references or a project .zip file](CSharp/Orders/Import/Readme.md#add-or-update-a-group-within-an-existing-order)

- Retrieving orders
	- Get orders
		- [Retrieve available orders (plain, by order number, by state, changedSince, releasedSince)](CSharp/Orders/Actions/Readme.md)
		- [Retrieve a single order by order id or order number](CSharp/Orders/Actions/Readme.md#example-getorder)

- Release orders
	- Release order
		- [Release order](CSharp/Orders/Actions/Readme.md)

	- Reset release order
		- [Reset release order](CSharp/Orders/Actions/Readme.md)

- Removing orders
	- Delete single order
		- [Delete order by order id](CSharp/Orders/Actions/Readme.md#example-deleteordersbyorderid)
		- [Delete order by order number](CSharp/Orders/Actions/Readme.md#example-deleteordersbyordernumber--deleteordersbyordernumbers)

	- Delete multiple orders
		- [Delete orders by order ids](CSharp/Orders/Actions/Readme.md#example-deleteordersbyorderids)
		- [Delete orders by order numbers](CSharp/Orders/Actions/Readme.md#example-deleteordersbyordernumber--deleteordersbyordernumbers)

- Retrieving and managing customers
	- Get customers
		- [Retrieve all customers](CSharp/Customers/Actions/Readme.md)
		- [Retrieve customers by customer numbers or ids](CSharp/Customers/Actions/Readme.md#example-getcustomers-by-customer-numbers--ids)
		- [Retrieve a single customer](CSharp/Customers/Actions/Readme.md#example-getcustomer-single-by-number-or-id)
	- Create customer
		- [Create a new customer](CSharp/Customers/Actions/Readme.md#example-createcustomer)
	- Delete customers
		- [Delete customers by ids or numbers](CSharp/Customers/Actions/Readme.md#example-deletecustomersbycustomerids--deletecustomersbycustomernumbers)

- Configuration data management (cDM)
	- Get libraries and custom tables
		- [Retrieve available libraries](CSharp/ConfigurationDataManagement/Actions/Readme.md)
		- [Retrieve custom tables of a library](CSharp/ConfigurationDataManagement/Actions/Readme.md#example-getcustomtables)
		- [Retrieve the structure of a custom table](CSharp/ConfigurationDataManagement/Actions/Readme.md#example-getcustomtablestructure)
		- [Retrieve the content of a custom table](CSharp/ConfigurationDataManagement/Actions/Readme.md#example-getcustomtabledata)
	- Set custom table data
		- [Replace the content of a custom table](CSharp/ConfigurationDataManagement/Actions/Readme.md#example-setcustomtabledata)
