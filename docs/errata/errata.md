**Errata** (2 items)

If you find any mistakes, then please [raise an issue in this repository](https://github.com/markjprice/cs15net11/issues) or email me at markjprice (at) gmail.com.

> **Warning!** Avoid copying and pasting links that break over multiple lines and include hyphens or dashes because your PDF reader might remove a hyphen thinking that it being helpful but break the link! Just click on the links and they will work. Or carefully check that your PDF reader has not removed a hyphen after pasting into your web browser address bar. [See an example of this issue here](https://github.com/markjprice/cs13net9/issues/77).

- [Cover and pages 1, 12, 36](#cover-and-pages-1-12-36)
- [Page 14 - Installing other extensions](#page-14---installing-other-extensions)

# Cover and pages 1, 12, 36

In four places this book calls the November 2026 release of Microsoft's IDE "Visual Studio 2027". The correct name is Visual Studio 2026, version 18.12 or later. At the time of writing, the current release is 18.12.1.

When Visual Studio 2026 was released in November 2025, Microsoft announced a new release model. A new version would come out every November alongside each new .NET release. Microsoft's own example said that the following November, Visual Studio 2026 will update in place to Visual Studio 2027. The press reported it the same way, saying the next release in November 2026 will be called Visual Studio 2027. This book was written using that announced plan. 

Microsoft appears to have changed course now that .NET 11 has shipped. On 10 November 2026 it released Visual Studio 2026 18.12 on the Stable channel, along with a new Long-Term Servicing Channel release. Microsoft said that the product remains Visual Studio 2026, and no "Visual Studio 2027" was released. 

Nothing in the book's instructions changes. Wherever you see "Visual Studio 2027", read it as "Visual Studio 2026" and make sure you have version 18.12 or later. That version fully supports .NET 11 and C# 15. If you already have Visual Studio 2026, use the Visual Studio Installer to update it to the latest version. You don't need to install anything new.

# Page 14 - Installing other extensions

> Thanks to **Jan Hahne** / `janhahne` in the book's Discord channel for raising this issue on November 21, 2026.

Jan wrote:
```text
The Visual Studio Code Extension
tintoy.msbuild-project-tools
causes an ugly error in Visual Studio Code:
https://github.com/tintoy/msbuild-project-tools-vscode/issues/186
So I uninstalled it
```

This extension is not vital to complete the book so removing it until they deploy a fix is an option for now for readers who find the error message annoying.

As they explain in the GitHub issue you linked to, this happens because the version of MSBuild shipped with 10.x and newer directly references that assembly (which causes problems when we want to load different versions of MSBuild than the one language server was built with). Work on this issue is being tracked by https://github.com/tintoy/msbuild-project-tools-server/issues/146. They have a solution but have not released the fix, yet (and they are making some changes to CI to hopefully catch this sort of problem if it comes up again in the future).

