# Common responses to a `GET` request

Common HTTP status code responses to an HTTP `GET` request include success and multiple types of failure, as shown in the following table:

Status code|Description
---|---
`101 Switching Protocols`|The requester has asked the server to switch protocols, and the server has agreed to do so. For example, it is common to switch from HTTP to WebSockets for more efficient communication.
`103 Early Hints`|This is used to convey hints that help a client make preparations to process the final response. For example, the server might send the following response before then sending a normal `200 OK` response for a web page that uses a stylesheet and JavaScript file:<br/>`HTTP/1.1 103 Early Hints`<br/>`Link: </style.css>; rel=preload; as=style`<br/>`Link: </script.js>; rel=preload; as=script`
`200 OK`|The path was correctly formed, and the resource was successfully found, serialized into an acceptable media type, and then returned in the response body. The response headers specify `Content-Type`, `Content-Length`, and `Content-Encoding`, for example, `GZIP`.
`301 Moved Permanently`|Over time, a web service may change its resource model, including the path used to identify an existing resource. The web service can indicate the new path by returning this status code and a response header named `Location` that has the new path.
`302 Found`|This is similar to `301`.
`304 Not Modified`|If the request includes the `If-Modified-Since` header, then the web service can respond with this status code. The response body is empty because the client should use its cached copy of the resource.
`307 Temporary Redirect`|The requested resource has been temporarily moved to the URL in the `Location` header. The browser should make a new request using that URL. For example, this is what happens if you enable `UseHttpsRedirection` and a client makes an HTTP request.
`400 Bad Request`|The request was invalid, for example, it used a path for a product using an integer ID where the ID value is missing.
`401 Unauthorized`|The request was valid and the resource was found, but the client did not supply credentials or is not authorized to access that resource. Re-authenticating may enable access, for example, by adding or changing the `Authorization` request header.
`403 Forbidden`|The request was valid and the resource was found, but the client is not authorized to access that resource. Re-authenticating will not fix the issue.
`404 Not Found`|The request was valid, but the resource was not found. The resource may be found if the request is repeated later. To indicate that a resource will never be found, return `410 Gone`.
`406 Not Acceptable`|The request has an `Accept` header that only lists media types that the web service does not support. For example, the client requests JSON, but the web service can only return XML.
`409 Conflict`|The request couldn’t be completed because it conflicts with the current state of the resource. For example, a canceled payment cannot be canceled again, or when trying to upload or update a resource that already exists in a way that violates uniqueness, such as creating a user with a username that’s already taken.
`429 Too Many Requests`|The client has sent too many requests in a given amount of time. The client should reduce the rate due to this rate limiting. A `Retry-After` header may be included in this response to indicate how long a client should wait before making another request.
`451 Unavailable for Legal Reasons`|A website hosted in the USA might return this for requests coming from Europe to avoid having to comply with the **General Data Protection Regulation (GDPR)**. The number was chosen as a reference to the novel Fahrenheit 451, in which books are banned and burned.
`500 Server Error`|The request was valid, but something went wrong on the server side while processing the request. Retrying again later might work.
`503 Service Unavailable`|The web service is busy and cannot handle the request. Trying again later might work.
`504 Gateway Timeout`|A server acting as a gateway or proxy didn’t get a timely response from the upstream server it needed to access to complete the request. 
