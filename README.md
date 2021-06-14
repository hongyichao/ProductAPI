# ProductAPI
A Product API with basic endpoints to Create, Get, Update and Delete products


Please run the command below to create Database
dotnet ef  database update --project .\ProductData\ProductData.csproj --startup-project .\ProductAPI\ProductAPI.csproj

or
1) delete the Migrations folder under ProductData
2) dotnet ef migrations add Initial --project .\ProductData\ProductData.csproj --startup-project .\ProductAPI\ProductAPI.csproj
3) dotnet ef  database update --project .\ProductData\ProductData.csproj --startup-project .\ProductAPI\ProductAPI.csproj


Assumption:

The logging is not required and can be improved 
