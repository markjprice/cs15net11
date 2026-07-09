# JSON Patch implementation improvements

**JSON Patch** is a standard format for describing changes to apply to a JSON document, defined in RFC 6902. It represents a sequence of operations, such as add, remove, replace, move, copy, and test, that can be applied to modify a JSON document.

In web applications, JSON Patch is commonly used in an HTTP `PATCH` request to perform partial updates of a resource. Instead of sending the entire resource for an update, clients can send a JSON Patch document containing only the changes. This reduces payload size and improves efficiency.

.NET has an implementation of JSON Patch (RFC 6902) for ASP.NET Core based on `System.Text.Json`. This implementation provides improved performance and reduced memory usage compared to the Newtonsoft.Json-based implementation.

To enable JSON Patch support with `System.Text.Json`, use the `Microsoft.AspNetCore.JsonPatch.SystemTextJson` NuGet package. This package provides a `JsonPatchDocument<T>` class to represent a JSON Patch document for objects of the `T` type and custom logic for serializing and deserializing JSON Patch documents using `System.Text.Json`. 

The key method of the `JsonPatchDocument<T>` class is `ApplyTo`, which applies the patch operations to a target object of the `T` type:
1.	In the `Serialization` project, add a package reference for the latest version of `Microsoft.AspNetCore.JsonPatch.SystemTextJson`:
```xml
<PackageReference
  Include="Microsoft.AspNetCore.JsonPatch.SystemTextJson"
  Version="11.0.0" />
```

2.	Build the `Serialization` project to restore packages.
3.	In `Program.cs`, at the top of the file, add statements to import the namespace so that we can work with the `JsonPatchDocument<T>` class:
```cs
// To use JsonPatchDocument<T>.
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
```

4.	In `Program.cs`, add statements to create an instance of `Person` and an instance of `JsonPatchDocument<Person>`, and use that to patch the object:
```cs
Person person = new()
{
  FirstName = "Cassian",
  LastName = "Andor",
  DateOfBirth = new DateTime(1990, 1, 1)
};

// Output original object.
WriteLine($"Before: {FastJson.Serialize(person)}");

// Define a JSON patch document.
string jsonPatch = """
[
  { "op": "replace", "path": "/FirstName", "value": "Varian" },
  { "op": "replace", "path": "/LastName", "value": "Skye" },
  { "op": "remove", "path": "/DateOfBirth"}
]
""";

// Deserialize the JSON patch document.
JsonPatchDocument<Person>? patchDoc =
  FastJson.Deserialize<JsonPatchDocument<Person>>(jsonPatch);

// Apply the JSON patch document.
patchDoc!.ApplyTo(person);

// Output updated object.
WriteLine($"After: {FastJson.Serialize(person)}");
```

5.	Run the code and view the result:
```
Before: {"FirstName":"Cassian","LastName":"Andor","DateOfBirth":"1990-01-01T00:00:00","Children":null}
After: {"FirstName":"Varian","LastName":"Skye","DateOfBirth":"0001-01-01T00:00:00","Children":null}
```

> **Warning!** The `System.Text.Json` implementation of JSON Patch isn't a complete drop-in replacement for the existing `Newtonsoft.Json`-based implementation. In particular, the implementation doesn't support dynamic types such as `ExpandoObject`.
