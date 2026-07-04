# Converting from a binary object to a string using Base64 encoding

When you have a binary object like an image or video that you want to either store or transmit, you sometimes do not want to send the raw bits because you do not know how those bits could be misinterpreted, for example, by the network protocol transmitting them or another operating system that is reading the stored binary object.

The safest thing to do is to convert the binary object into a string of safe characters. Programmers call this **Base64 encoding**. Base64 is an encoding scheme that converts arbitrary bytes into text using a specific set of 64 characters. It’s widely used for data transfer and has long been supported through various methods.

The `Convert` type has a pair of methods, `ToBase64String` and `FromBase64String`, that perform this conversion for you. Let’s see them in action:

1.	Type statements to create an array of bytes randomly populated with byte values, write each byte nicely formatted to the console, and then write the same bytes converted into Base64 to the console:
```cs
// Allocate an array of 128 bytes.
byte[] binaryObject = new byte[128];

// Populate the array with random bytes.
Random.Shared.NextBytes(binaryObject);

WriteLine("Binary Object as bytes:");

for (int index = 0; index < binaryObject.Length; index++)
{
  Write($"{binaryObject[index]:X2} ");
}
WriteLine();

// Convert the array to Base64 string and output as text.
string encoded = ToBase64String(binaryObject);
WriteLine($"Binary Object as Base64: {encoded}");
```

> By default, an `int` value would output assuming decimal notation, that is, Base10. You can use format codes such as `:X2` to format the value using hexadecimal notation.

2.	Run the code and view the result:
```
Binary Object as bytes:
EB 53 8B 11 9D 83 E6 4D 45 85 F4 68 F8 18 55 E5 B8 33 C9 B6 F4 00 10 7F CB 59 23 7B 26 18 16 30 00 23 E6 8F A9 10 B0 A9 E6 EC 54 FB 4D 33 E1 68 50 46 C4 1D 5F B1 57 A1 DB D0 60 34 D2 16 93 39 3E FA 0B 08 08 E9 96 5D 64 CF E5 CD C5 64 33 DD 48 4F E8 B0 B4 19 51 CA 03 6F F4 18 E3 E5 C7 0C 11 C7 93 BE 03 35 44 D1 6F AA B0 2F A9 CE D5 03 A8 00 AC 28 8F A5 12 8B 2E BE 40 C4 31 A8 A4 1A
Binary Object as Base64: 61OLEZ2D5k1FhfRo+BhV5bgzybb0ABB/y1kjeyYYFjAAI+aPqRCwqebsVPtNM+FoUEbEHV+xV6Hb0GA00haTOT76CwgI6ZZdZM/lzcVkM91IT+iwtBlRygNv9Bjj5ccMEceTvgM1RNFvqrAvqc7VA6gArCiPpRKLLr5AxDGopBo=
```

## Base64 for URLs

Base64 is useful, but some of the characters it uses, like `+` and `/`, are problematic for certain uses, such as query strings in URLs, where these characters have special meanings.

To address this issue, the Base64Url scheme was created. It is like Base64 but uses a slightly different set of characters, making it suitable for contexts like URLs.

> You can learn more about the Base64Url scheme at the following link: https://base64.guru/standards/base64url.

The .NET `Base64Url` class offers a range of optimized methods for encoding and decoding data using the Base64Url scheme. For example, you can convert some arbitrary bytes into Base64Url:
```cs
ReadOnlySpan<byte> bytes = ...;
string encoded = Base64Url.EncodeToString(bytes);
```

> **Warning!**: Base64 and Base64Url are encodings, not encryption or hashing. Anyone can decode the original bytes, and the encoded representation is usually about one-third larger.
