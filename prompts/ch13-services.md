# Prompts for *Chapter 13 Building and Consuming Web Services*

```
Create a table comparing GET, POST, PUT, PATCH, and DELETE in
terms of purpose, safety, idempotency, request bodies, successful response
codes, and whether an automatic retry is normally safe.
```

```
Please explain when a value should appear as a route parameter,
query string parameter, header, or request body in a Minimal API. Use
filtering customers by country and requesting a number of weather-forecast
days as examples.
```

```
Compare returning a string, an arbitrary object, IResult, Results,
TypedResults, and Results<T1, T2> from a Minimal API endpoint. Explain
the effects on serialization, unit testing, compile-time checking, and
OpenAPI metadata. Then rewrite a Minimal API GET-by-ID endpoint so that
it returns Results<Ok<Customer>, NotFound> instead of IResult, and explain
how the change improves the generated OpenAPI document.
```

```
Please explain the Problem Details standard for HTTP APIs. Show
how ProblemDetails, ValidationProblemDetails, TypedResults.Problem, trace
identifiers, stable type URIs, and extension members should be used without
revealing sensitive information.
```

```
Extend the /customers endpoint with pagination, filtering, and
sorting. Propose query parameters, validation rules, response metadata,
navigation links, and the corresponding OpenAPI description. Then add
optimistic concurrency to the customer API using ETags and the If-Match
header. Explain how this prevents one user from silently overwriting
another user's changes.
```

```
Please show me how to integration-test Minimal API endpoints with
WebApplicationFactory, a test database, and HttpClient. Include successful
requests, invalid input, missing resources, and malformed JSON.
```

```
Please explain the difference between an OpenAPI document name,
the OpenAPI specification version, the document's info.version, and
application-level API versioning through URLs or headers.
```

```
Please explain how one ASP.NET Core app can expose the same
OpenAPI document as JSON and YAML. Discuss runtime generation, buildtime generation, caching, and whether the document should be public in
production.
```

```
Trace how ASP.NET Core generates an OpenAPI document from Minimal
API endpoint metadata, typed results, data annotations, XML documentation
comments, and OpenAPI transformers.
```

```
Compare the roles of Microsoft.AspNetCore.OpenApi, Scalar, Swagger
UI, ReDoc, NSwag, and OpenAPI Generator. Distinguish document generation,
interactive documentation, validation, and client-code generation.
```

```
Please explain what should and should not be included in HTTP
request and response logs. Cover passwords, authorization headers, cookies,
personal data, body-size limits, redaction, structured logging, and trace
identifiers.
```

```
Please explain CORS from first principles. Cover origins, the
importance of scheme, host and port, simple requests, preflight requests,
allowed methods, allowed headers, credentials, and why CORS is not
authentication. Then design authentication and authorization for the
Northwind API. Explain how to protect read and write endpoints differently,
and why CORS does not replace authorization.
```

```
Design separate request and response DTOs for the Northwind
customer API. Explain how DTOs prevent overposting, avoid serialization
cycles, hide persistence details, and allow the API contract to evolve
independently from EF Core entities.
```
