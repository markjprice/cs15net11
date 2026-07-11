<Query Kind="Expression">
  <Connection>
    <ID>4b835bd3-def6-4496-ba14-6c0e6a866b7f</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>C:\cs15net11\WebData\Northwind.DataContext\bin\Release\net11.0\Northwind.DataContext.dll</CustomAssemblyPath>
    <CustomTypeName>Northwind.EntityModels.NorthwindDb</CustomTypeName>
    <CustomCxString>Data Source=C:\cs15net11\WebData\Northwind.db</CustomCxString>
    <DisplayName>NorthwindDb (Sqlite)</DisplayName>
    <DriverData>
      <UseDbContextOptions>true</UseDbContextOptions>
      <EFProvider>Microsoft.EntityFrameworkCore.SQLite</EFProvider>
    </DriverData>
  </Connection>
</Query>

Customers.AsEnumerable().FullJoin(
  inner: Suppliers,
  outerKeySelector: customer => customer.City,
  innerKeySelector: supplier => supplier.City,
  resultSelector: (customer, supplier) => new
  {
    City = customer == null
      ? supplier!.City
      : customer.City,

    CustomerName = customer == null
      ? null
      : customer.CompanyName,

    SupplierName = supplier == null
      ? null
      : supplier.CompanyName
  })
  .OrderBy(row => row.City)
  .ThenBy(row => row.CustomerName)
  .ThenBy(row => row.SupplierName)
