# Prompts for *Chapter 7 Packaging and Distributing .NET Types*

```
Please explain the difference between a .NET project SDK, an
installed .NET SDK, a target framework, a reference pack, a shared
framework, a runtime, a workload, and a NuGet package. Use one console
application project to show what each contributes at build time and at
run time.
```

```
Please explain the relationships among a C# type, a namespace, an
assembly, a project, and a NuGet package. Use one type that is defined
in one assembly but belongs to a namespace that spans several assemblies.
```

```
Please explain Central Package Management in a multi-project
.NET solution. Show a complete `Directory.Packages.props` file containing
`ManagePackageVersionsCentrally`, shared version properties, and
`PackageVersion` items, followed by project files containing `PackageReference` items without versions.
```

```
Please explain `VersionOverride`, conditional `PackageVersion` items
for multi-targeted projects, global package references, and transitive
pinning in CPM. For each feature, explain when it is useful and what
maintenance risks it introduces.
```

```
Show how to configure public and private NuGet sources safely
using `<clear />` and Package Source Mapping and explain how this reduces
dependency-confusion attacks. Given a nuget.config containing exact
package IDs, prefix patterns such as `Northwind.*`, and the wildcard
pattern `*`, explain which source NuGet will use for each package. Include
the precedence rules for exact IDs, longer prefixes, shorter prefixes,
and the wildcard.
```

```
Please explain how a self-contained .NET application receives .NET
runtime security fixes, and why its publisher must rebuild and redeploy
it after runtime servicing updates.
```

```
Compare framework-dependent deployment, a portable frameworkdependent DLL, a framework-dependent apphost executable, self-contained deployment, single-file publishing, ReadyToRun, trimming, and Native AOT.
Give a decision table for command-line tools, desktop applications, web
services, and containers.
```

```
Please explain how trimming interacts with reflection, dependency
injection, serializers, and dynamically loaded types. Include
DynamicallyAccessedMembers, RequiresUnreferencedCode, RequiresDynamicCode,
and source-generated alternatives.
```

```
Please explain what invariant globalization removes from a .NET
application. Demonstrate the effect on dates, numbers, currency, sorting,
and casing using both `en-US` and `en-GB` examples.
```

```
Please explain why a trimmed or Native AOT application must be
tested from its published output, even when its ordinary debug build
works and the compiler reports no trimming or AOT warnings.
```

```
Please show me how to investigate why a NuGet package was selected
during restore. Include direct and transitive dependencies, `project.assets.
json`, `dotnet package list --include-transitive`, verbose restore output,
and dependency-resolution rules.
```

```
Please explain exactly how NuGet resolves `Version="1.2.3"`,
`Version="[1.2.3]"`, `Version="[1.2.3,2.0.0)"`, and `Version="1.2.*"`. Include direct dependencies, transitive dependencies, lowest applicable version, and `packages.lock.json`.
```

```
Please explain the difference between assembly version, file
version, informational version, and NuGet package version. Recommend a
versioning policy for a public library that follows Semantic Versioning.
```

```
Show how to build, inspect, and test a NuGet package using a
local folder feed before pushing it to nuget.org. Include incrementing
the package version, clearing local NuGet caches, and removing the test
package afterward.
```

```
Design a multi-targeted .NET library that supports both .NET Framework
consumers through `netstandard2.0` and modern .NET consumers through `net11.0`. Show how to use conditional compilation and conditional package references only when necessary.
```
