- [Serializing object graphs as XML](#serializing-object-graphs-as-xml)
  - [Generating compact XML](#generating-compact-xml)
  - [Deserializing XML files](#deserializing-xml-files)


# Serializing object graphs as XML

To show a typical XML example:
1.	In the `Serialization` project, in `Program.cs`, import the namespace to work with XML serialization:
```cs
using System.Xml.Serialization; // To use XmlSerializer.
```

3.	In `Program.cs`, add statements at the bottom to serialize the object graph of `Person` instances as XML:
```cs
SectionTitle("Serializing as XML");

// Create serializer to format a "List of Person" as XML.
XmlSerializer xs = new(type: people.GetType());

// Create a file to write to.
string path = Combine(CurrentDirectory, "people.xml");

using (FileStream stream = File.Create(path))
{
  // Serialize the object graph to the stream.
  xs.Serialize(stream, people);
} // Closes the stream.

OutputFileInfo(path);
```

4.	Run the code, view the result, and note that an exception is thrown:
```
Unhandled Exception: System.InvalidOperationException: Packt.Shared.Person cannot be serialized because it does not have a parameterless constructor.
```
5.	In `Person.cs`, add a statement to define a parameterless constructor:
```cs
// A parameterless constructor is required for XML serialization.
public Person() { }
```

> The constructor does not need to do anything, but it must exist so that XmlSerializer can call it to instantiate new Person instances during the deserialization process.

6.	Run the code and view the result, and note that the object graph is serialized as XML elements, such as `<FirstName>Bob</FirstName>`, and that the `Salary` property is not included because it is not a public property:
```
**** File Info ****
File: people.xml
Path: C:\cs15net11\Chapter09\Serialization\bin\Debug\net11.0
Size: 793 bytes.
/------------------
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfPerson xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Person>
    <FirstName>Alice</FirstName>
    <LastName>Smith</LastName>
    <DateOfBirth>1974-03-14T00:00:00</DateOfBirth>
  </Person>
  <Person>
    <FirstName>Bob</FirstName>
    <LastName>Jones</LastName>
    <DateOfBirth>1969-11-23T00:00:00</DateOfBirth>
  </Person>
  <Person>
    <FirstName>Charlie</FirstName>
    <LastName>Cox</LastName>
    <DateOfBirth>1984-05-04T00:00:00</DateOfBirth>
    <Children>
      <Person>
        <FirstName>Sally</FirstName>
        <LastName>Cox</LastName>
        <DateOfBirth>2012-07-12T00:00:00</DateOfBirth>
      </Person>
    </Children>
  </Person>
</ArrayOfPerson>
------------------/
```

That XML looks good, but it is rather verbose. Let’s see whether we can make it valid XML but more compact.

## Generating compact XML

We could make the XML more compact using attributes instead of elements for some fields:
1.	At the top of `Person.cs`, import the `System.Xml.Serialization` namespace so that you can decorate some properties with the `[XmlAttribute]` attribute:
```cs
using System.Xml.Serialization; // To use [XmlAttribute].
```

2.	In `Person.cs`, decorate the first name, last name, and date of birth properties with the `[XmlAttribute]` attribute, and set a short name for each property:
```cs
[XmlAttribute("fname")]
public string? FirstName { get; set; }
[XmlAttribute("lname")]
public string? LastName { get; set; }
[XmlAttribute("dob")]
public DateTime DateOfBirth { get; set; }
```

3.	Run the code, and note that the size of the file has reduced from 793 to 488 bytes, a space-saving of more than a third. This reduction was achieved by outputting property values as XML attributes:
```
**** File Info ****
File: people.xml
Path: C:\cs15net11\Chapter09\Serialization\bin\Debug\net11.0
Size: 488 bytes.
/------------------
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfPerson xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Person fname="Alice" lname="Smith" dob="1974-03-14T00:00:00" />
  <Person fname="Bob" lname="Jones" dob="1969-11-23T00:00:00" />
  <Person fname="Charlie" lname="Cox" dob="1984-05-04T00:00:00">
    <Children>
      <Person fname="Sally" lname="Cox" dob="2012-07-12T00:00:00" />
    </Children>
  </Person>
</ArrayOfPerson>
------------------/
```

That’s much more efficient. Now, let’s make sure we know how to deserialize XML back into live in-memory objects.

## Deserializing XML files

Now, let’s try deserializing the XML file back into live objects in memory:
1.	In `Program.cs`, add statements to open the XML file, and then deserialize it:
```cs
SectionTitle("Deserializing XML files");

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{
  // Deserialize and cast the object graph into a "List of Person".
  List<Person>? loadedPeople =
    xs.Deserialize(xmlLoad) as List<Person>;

  if (loadedPeople is not null)
  {
    foreach (Person p in loadedPeople)
    {
      WriteLine("{0} has {1} children.",
        p.LastName, p.Children?.Count ?? 0);
    }
  }
}
```

2.	Run the code, and note that the people are loaded successfully from the XML file and then enumerated:
```
Smith has 0 children.
Jones has 0 children.
Cox has 1 children.
```

There are many other attributes defined in the `System.Xml.Serialization` namespace that can be used to control the XML generated. A good place to start is the official documentation for the `XmlAttributeAttribute` class found here: https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeattribute. Do not get this class confused with the `XmlAttribute` class in the `System.Xml` namespace. That is used to represent an XML attribute when reading and writing XML, using `XmlReader` and `XmlWriter`.

If you don’t use any annotations, `XmlSerializer` performs a case-insensitive match using the property name when deserializing.

> **Good practice**: When using `XmlSerializer`, remember that only the public fields and properties are included, and the type must have a parameterless constructor. You can customize the output with attributes.
