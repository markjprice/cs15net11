# Custom number formatting

You can take complete control of number formatting using custom format codes, as shown in the following table:

Format code|Description
---|---
`0`|Zero placeholder. Replaces the zero with the corresponding digit if present; otherwise, it uses zero. For example, `0000.00` formatting the value `123.4` would give `0123.40`.
`#`|Digit placeholder. Replaces the hash with the corresponding digit if present; otherwise, it uses nothing. For example, `####.##` formatting the value `123.4` would give `123.4`.
`.`|Decimal point. Sets the location of the decimal point in the number. Respects culture formatting, so it is a `.` (dot) in US English but a `,` (comma) in French.
`,`|Group separator. Inserts a localized group separator between each group. For example, `0,000` formatting the value `1234567` would give `1,234,567`. Also used to scale a number by dividing by multiples of 1,000 for each comma. For example, `0.00,,` formatting the value `1234567` would give `1.23` because the two commas mean divide by 1,000 twice.
`%`|Percentage placeholder. Multiplies the value by 100 and adds a percentage character.
`\`|Escape character. Makes the next character a literal instead of a format code. For example, `\##,###\#` formatting the value `1234` would give `#1,234#`.
`;`|Section separator. Defines different format strings for positive, negative, and zero numbers. For example, `[0];(0);Zero` formatting: `13` would give `[13]`, `-13` would give `(13)`, and `0` would give `Zero`.
Others|All other characters are shown in the output as is.

A full list of custom number format codes can be found at the following link: https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings.

You can apply standard number formatting using simpler format codes, such as `C` and `N`. They support a precision number to indicate how many digits of precision you want. The default is two. The most common are shown in the following table:

Format code|Description
---|---
`C` or `c`|Currency. For example, in US culture, `C` formatting the value `123.4` gives `$123.40`, and `C0` formatting the value `123.4` gives `$123`.
`N` or `n`|Number. Integer digits with an optional negative sign and grouping characters.
`D` or `d`|Decimal. Integer digits with an optional negative sign but no grouping characters.
`B` or `b`|Binary. For example, `B` formatting the value `13` gives `1101`, and `B8` formatting the value `13` gives `00001101`.
`X` or `x`|Hexadecimal. For example, `X` formatting the value `255` gives `FF`, and `X4` formatting the value `255` gives `00FF`.
`E` or `e`|Exponential notation. For example, `E` formatting the value `1234.567` would give `1.234567000E+003`, and `E2` formatting the value `1234.567` would give `1.23E+003`.

A full list of standard number format codes can be found at the following link: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings.

You can take complete control of date and time formatting using custom format codes, as shown in the following table:

Format code|Description
---|---
`/`|Date part separator. Varies by culture; for example, `en-US` uses `/` but `fr-FR` uses `-` (dash).
`\`|Escape character. Useful if you want to use a special format code as a literal character; for example, `h \h m \m` would format a time of 9:30 AM as `9 h 30 m`.
`:`|Time part separator. Varies by culture; for example, `en-US` uses `:` but `fr-FR` uses `.` (dot).
`d`, `dd`|The day of the month, from `1` to `31`, or with a leading zero from `01` through `31`.
`ddd`, `dddd`|The abbreviated or full name of the day of the week. For example, `Mon` or `Monday`, localized for the current culture.
`f`, `ff`, `fff`|The tenths of a second, hundredths of a second, or milliseconds.
`g`|The period or era, for example, `A.D`.
`h`, `hh`|The hour, using a 12-hour clock from `1` to `12`, or from `01` to `12`.
`H`, `HH`|The hour, using a 24-hour clock from `0` to `23`, or from `01` to `23`.
`K`|Time zone information. `null` for an unspecified time zone, `Z` for UTC, and a value such as `-8:00` for local time adjusted from UTC.
`m`, `mm`|The minute, from `0` through `59`, or with a leading zero from `00` through `59`.
`M`, `MM`|The month, from `1` through `12`, or with a leading zero from `01` through `12`.
`MMM`, `MMMM`|The abbreviated or full name of the month. For example, `Jan` or `January`, localized for the current culture.
`s`, `ss`|The second, from `0` through `59`, or with a leading zero from `00` through `59`.
`t`, `tt`|The first character or both characters of the AM/PM designator.
`y`, `yy`|The year of the current century, from `0` through `99`, or with a leading zero from `00` through `99`.
`yyy`|The year with a minimum of three digits, and as many as needed. For example, 1 A.D. is `001`. The first sacking of Rome was in `410`. The year the first edition of this book was published in `2016`.
`yyyy`, `yyyyy`|The year as a four- or five-digit number.
`z`, `zz`|Hours offset from UTC, with no leading zeros, or with leading zeros.
`zzz`|Hours and minutes offset from UTC, with a leading zero. For example, `+04:30`.

> **Warning!** When defining a custom format, as defined in the preceding table, you must use multiple characters. If you try to define a custom format with a single character, like `d` or `y`, then your format will be interpreted as a standard format. Standard format codes take priority over custom format codes.

A full list of custom format codes can be found at the following link: https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings.

You can apply standard date and time formatting using simpler format codes, such as the `d` and `D` we used in the code example, as shown in the following table:

Format code|Description
---|---
`d`|Short date pattern. Varies by culture, for example, `en-US` uses `M/d/yyyy` and `fr-FR` uses `dd/MM/yyyy`.
`D`|Long date pattern. Varies by culture; for example, `en-US` uses `dddd, MMMM d, yyyy` and `fr-FR` uses `dddd, dd MMMM yyyy`.
`f`|Full date/time pattern (short time – hours and minutes). Varies by culture.
`F`|Full date/time pattern (long time – hours, minutes, seconds, and AM/PM). Varies by culture.
`m`, `M`|Month/day pattern. Varies by culture.
`o`, `O`|A standardized pattern, suitable to serialize date/time values for roundtrips – for example, `2023-05-30T13:45:30.0000000-08:00`.
`r`, `R`|RFC1123 pattern.
`t`|Short time pattern. Varies by culture; for example, `en-US` uses `h:mm tt` and `fr-FR` uses `HH:mm`.
`T`|Long time pattern. Varies by culture; for example, `en-US` uses `h:mm:ss tt` and `fr-FR` uses `HH:mm:ss`.
`u`|Universal sortable date/time pattern – for example, `2009-06-15 13:45:30Z`.
`U`|Universal full date/time pattern. Varies by culture; for example, `en-US` might be `Monday, June 15, 2009 8:45:30 PM`.
`y`, `Y`|Year month pattern. For example, `en-US` might be `November 2026`.

A full list of format codes can be found at the following link: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings.

> **Warning!** Note the difference between `"m"` (a shorthand for the month/day format, such as July 4 in the USA or 4 July in the UK, which omits the year and presents the month and day in a culture-specific manner) and `"d m"` (a custom format string that displays the day number and then the month number with a space in between, such as `4 7` in any country for 4th July).
