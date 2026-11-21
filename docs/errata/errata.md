**Errata** (1 item)

If you find any mistakes, then please [raise an issue in this repository](https://github.com/markjprice/cs15net11/issues) or email me at markjprice (at) gmail.com.

> **Warning!** Avoid copying and pasting links that break over multiple lines and include hyphens or dashes because your PDF reader might remove a hyphen thinking that it being helpful but break the link! Just click on the links and they will work. Or carefully check that your PDF reader has not removed a hyphen after pasting into your web browser address bar. [See an example of this issue here](https://github.com/markjprice/cs13net9/issues/77).

- [Page 14 - Installing other extensions](#page-14---installing-other-extensions)



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

