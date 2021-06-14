# ProductAPI
A Product API with basic endpoints to Create, Get, Update and Delete products

dotnet ef migrations add Initial --project .\ProductData\ProductData.csproj --startup-project .\ProductAPI\ProductAPI.csproj

dotnet ef  database update --project .\ProductData\ProductData.csproj --startup-project .\ProductAPI\ProductAPI.csproj
