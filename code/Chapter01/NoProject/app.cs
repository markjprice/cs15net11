#!/usr/bin/env dotnet
#:include helpers.cs
#:include models/customer.cs

Customer customer = new("Bob");

Console.WriteLine(Helpers.FormatCustomer(customer));
