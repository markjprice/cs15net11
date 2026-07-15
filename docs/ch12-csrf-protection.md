- [Understanding automatic cross-origin CSRF protection](#understanding-automatic-cross-origin-csrf-protection)
- [How ASP.NET Core protects forms automatically](#how-aspnet-core-protects-forms-automatically)
- [Deferring rejection until a form is consumed](#deferring-rejection-until-a-form-is-consumed)
- [Comparing automatic protection with antiforgery tokens](#comparing-automatic-protection-with-antiforgery-tokens)
- [Allowing a trusted cross-origin form client](#allowing-a-trusted-cross-origin-form-client)
- [Opting out](#opting-out)


# Understanding automatic cross-origin CSRF protection

A **Cross-Site Request Forgery (CSRF)** attack occurs when a malicious website causes a visitor’s browser to send a request to another website where the visitor is already authenticated. If the target website uses cookie authentication, the browser might automatically include the authentication cookie. Unless the server can identify the request as untrusted, it might perform an operation using the victim’s identity, such as changing an address, placing an order, or deleting a record.

For this reason, HTTP methods that are considered safe, including `GET` and `HEAD`, must not modify application state. Operations that create, update, or delete data should use methods such as `POST`, `PUT`, `PATCH`, or `DELETE` and should be protected against forged requests.

# How ASP.NET Core protects forms automatically

Applications created using `WebApplication.CreateBuilder` automatically include lightweight cross-origin CSRF protection. You do not need to register a service or add middleware explicitly. It applies to Minimal API web services, MVC websites, Razor Pages websites, and Blazor projects.

Unlike the traditional token-based antiforgery system, the automatic middleware does not generate a cookie and hidden form token. Instead, it inspects headers supplied by the browser:

* `Sec-Fetch-Site` describes the relationship between the page that initiated the request and the requested server.
* `Origin` identifies the scheme, host, and port from which the request originated.

JavaScript cannot assign arbitrary values to these headers because browsers treat them as forbidden request headers. A malicious page cannot change `Sec-Fetch-Site: cross-site` to `same-origin` to disguise its request.

The middleware evaluates requests in the following order:

1. Safe methods such as `GET`, `HEAD`, `OPTIONS`, and `TRACE` are allowed.
2. `Sec-Fetch-Site: same-origin` is allowed.
3. `Sec-Fetch-Site: none` is allowed because it normally represents a request initiated directly by the user, such as typing a URL or choosing a bookmark.
4. A request from an origin explicitly trusted by the endpoint’s CORS policy is allowed.
5. Other `Sec-Fetch-Site` values, including `same-site` and `cross-site`, are denied unless the origin was trusted through CORS.
6. For an older browser that does not send `Sec-Fetch-Site`, the `Origin` header is compared with the scheme, host, and port of the requested application.
7. A request containing neither header is normally treated as a non-browser request and is allowed.

This means that different subdomains can be considered the same **site** but not the same **origin**. For example, a form sent from `https://shop.example.com` to `https://admin.example.com` is cross-origin and is denied unless the calling origin is explicitly trusted. Different schemes or port numbers also create different origins.

# Deferring rejection until a form is consumed

The automatic middleware does not immediately terminate every suspicious request. It records an allowed or denied verdict in the request’s `IAntiforgeryValidationFeature`. The request continues through the pipeline, and the verdict is enforced if a component attempts to read submitted form data. A denied form submission normally receives an HTTP `400 Bad Request` response before the endpoint handler performs its operation.

Form consumers include:

* A Blazor static server-side rendered form post.
* A Minimal API parameter bound from a form.
* An MVC controller action that consumes form data.
* Code that reads `HttpRequest.Form` directly.

This protection is not a general cross-origin firewall. If an endpoint reads a JSON request body rather than form data, nothing automatically enforces the recorded verdict. JSON APIs need an appropriate security design, including authentication, authorization, CORS configuration, and careful handling of credentials.

The customer create, edit, and delete pages in this chapter use Interactive Server rendering. Their event handlers normally execute over the existing Blazor circuit rather than through conventional static SSR form posts. Automatic CSRF protection still protects any static SSR forms and other form-processing endpoints hosted by the same ASP.NET Core application.

# Comparing automatic protection with antiforgery tokens

The automatic middleware and the existing token-based antiforgery system are separate protections:

| Feature                             | Automatic cross-origin protection               | Token-based antiforgery                            |
| ----------------------------------- | ----------------------------------------------- | -------------------------------------------------- |
| Enabled                             | Automatically by `WebApplication.CreateBuilder` | Explicitly with `app.UseAntiforgery()`             |
| Evidence checked                    | `Sec-Fetch-Site` and `Origin`                   | Cookie and request-token pair                      |
| Server-generated token              | No                                              | Yes                                                |
| Data Protection required for tokens | No                                              | Yes                                                |
| Main target                         | Cross-origin browser form submissions           | Form submissions without the matching server token |

An application can use both systems. If `app.UseAntiforgery()` is present, the token middleware runs after the automatic middleware and its result becomes authoritative. A valid token can therefore allow a request that the origin check marked as invalid, while a missing or invalid token can reject a request that passed the origin check.

Keep token-based antiforgery when:

* The application must support older browsers that do not provide Fetch Metadata headers.
* A security or compliance requirement mandates antiforgery tokens.
* The application uses `IAntiforgeryAdditionalDataProvider`.
* You want a second, independent layer of protection.

For applications that target modern browsers, the automatic middleware might be sufficient on its own. Removing `app.UseAntiforgery()` also means that Blazor static SSR forms no longer render antiforgery tokens. Their form posts are validated using the browser headers instead.

# Allowing a trusted cross-origin form client

A legitimate browser application might need to submit forms across origins. For example:

```text
https://app.example.com
        |
        | POST form
        v
https://api.example.com
```

Declare the precise calling origin in a CORS policy:

```cs
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy
      .WithOrigins("https://app.example.com")
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});
```

After building the application, enable CORS:

```cs
app.UseCors();
```

The automatic CSRF middleware can use the CORS policy as evidence that the origin is trusted. A named policy applied using `.RequireCors("policy-name")` or `[EnableCors("policy-name")]` is also recognized. `AllowAnyOrigin` is deliberately not accepted as a CSRF trust signal because allowing every origin to alter authenticated state would defeat the protection.

# Opting out

A particular endpoint can opt out when cross-origin browser submission is intentional and another mechanism secures the operation.

For a Minimal API endpoint:

```cs
app.MapPost("/webhook", HandleWebhook)
  .DisableAntiforgery();
```

For an MVC controller or action:

```cs
[IgnoreAntiforgeryToken]
public IActionResult ReceiveWebhook()
{
  // Process the webhook.
}
```

Do not opt out an endpoint that is accessible from a browser and relies on automatically supplied cookies for authentication. An opt-out is more appropriate for a webhook or service endpoint authenticated using a bearer token, API key, signature, or another credential that the browser does not attach automatically.

The middleware can be disabled for the entire application in `appsettings.json`:

```json
{
  "DisableCsrfProtection": true
}
```

This setting is intended as a temporary migration or troubleshooting option, not as a normal production configuration. If the application has endpoints that require antiforgery validation, disabling automatic CSRF protection without adding `app.UseAntiforgery()` can leave no antiforgery middleware available and cause requests to fail.

For unusual trust requirements, an application can replace the default `ICsrfProtection` service with a custom implementation. This should be reserved for cases that cannot be represented by a precise CORS policy or a narrowly scoped endpoint opt-out.

> **Good practice:** Keep all `GET` and other safe-method endpoints free of side effects. Trust only exact cross-origin clients through CORS, and do not use `AllowAnyOrigin` as permission to perform authenticated writes. Avoid disabling CSRF protection on any browser-accessible endpoint that relies on cookies. CSRF protection does not replace authentication, authorization, input validation, or database integrity checks.
