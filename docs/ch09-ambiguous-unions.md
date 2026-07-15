# Handling ambiguous union cases

The default behavior works best when the cases have different JSON shapes. For example:

```cs
public union ProductIdentifier(int, string);
```

has one number case and one string case. The first JSON token tells the deserializer which case to construct.

Some unions cannot be distinguished so easily. Consider the payment union created in *Chapter 5*:

```cs
public union PaymentOutcome(
  PaymentApproved,
  PaymentDeclined,
  PaymentPending);
```

All three records are represented as JSON objects beginning with `{`. Serialization is possible because the existing union value already identifies its active case. During deserialization, however, the default classifier cannot determine the case merely from the JSON token kind because all three cases look like objects. The same issue occurs when a union has two string-shaped cases, such as `Guid` and `DateTimeOffset`.

To handle this ambiguity you must use union-specific JSON metadata and customization APIs, including:

* `JsonTypeInfoKind.Union`
* `JsonUnionAttribute`
* `JsonUnionCaseInfo`
* `JsonTypeClassifier`
* `JsonSerializerOptions.TypeClassifiers`

These APIs allow libraries and advanced applications to control how union cases are discovered and selected. The union contract is supported by both reflection-based metadata and the `System.Text.Json` source generator.

These are advanced APIs so I do not cover the use in this book.

> **Good practice:** Prefer union cases with clearly distinguishable JSON shapes when the union will be deserialized from untagged JSON. If multiple cases serialize to the same shape, define and document an explicit classification or discriminator strategy. Do not assume that the serializer can reliably choose between two unrelated object cases merely by inspecting their property values.

