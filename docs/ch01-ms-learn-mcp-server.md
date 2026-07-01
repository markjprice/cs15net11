# Microsoft Learn documentation MCP server

Microsoft has created an MCP server for its official documentation so that chatbots can be configured to use the official documentation as a tool in their responses. The MCP server is accessible to any code editor or tool that supports the Model Context Protocol (MCP) using the following endpoint:
https://learn.microsoft.com/api/mcp.

You can install it for VS Code and Cursor using the following link:
https://github.com/MicrosoftDocs/mcp?tab=readme-ov-file#-installation--getting-started.

For Visual Studio, at the time of writing in July 2026, you must configure it manually using the following steps:
1.	In the `Chapter01` folder, create a file named `.mcp.json`.
2.	In the `.mcp.json` file, define the endpoint for the Microsoft Learn MCP Server, as shown in the following JSON:
```json
{
  "servers": {
    "microsoft.docs.mcp": {
      "type": "http",
      "url": "https://learn.microsoft.com/api/mcp"
    }
  }
}
```
3.	In Visual Studio, make sure the `Chapter01` solution is open in **Solution Explorer**.
4.	In the toolbar, click **GitHub Copilot**, and then navigate to **Settings** | **Options…**.
5.	In the **Options** dialog box, in the GitHub | Copilot | Copilot Chat section, select the **Enable Agent mode in the chat pane** and **Enable MCP server integration in agent mode** checkboxes.

> Visual Studio is likely to have a checkbox labeled **Enable Microsoft Learn function in chat**, so that you do not have to manually create the `.mcp.json` file. But it is useful for you to see that you can manually add any MCP servers that you like to that file, rather than rely on the Visual Studio team to add checkboxes for each one.

6.	Close the **Options** tab or click **OK**.
7.	Open the **GitHub Copilot Chat** window.
8.	At the bottom of the chat window, select **Agent** mode, select the tool named `microsoft.docs.mcp`, and select a model like GPT-5 mini, GPT-4.1, or Claude Sonnet 3.7.

> The list of models available to you will vary. Some models, like Gemini 2.5 Pro, might require additional configuration and cost premium requests. You will have a monthly limit of premium requests. Requests to GPT 4.1 or GPT-4o do not use premium requests.

9.	Click the **+** button to add a reference, select **Solution**, and note that **#solution** is added next to **Active Document** in the references.
10.	In the chat box, ask what the HelloCS project does, as shown in the following query:
```text
What does the HelloCS project do?
```
11.	Note the response (the response you get will be different since LLMs are non-deterministic):
```text
4 references
Read 4 code snippets for ‘HelloCS’, ‘Main’, ...
The HelloCS project is a simple .NET 10 console application. It prints "Hello, C#!" to the console, displays the namespace of the Program class, and then throws an exception. Its main purpose is likely to demonstrate basic console output and exception handling in C#.
```
12.	Ask what the remarks are about the int data type in the official documentation, as shown in the following query:
```text
what do the docs say about int?
```
13.	When the model tries to use the MCP server and its `microsoft_docs_search` tool, click **Confirm**. Other options include **Always allow**, **Allow in this solution**, and **Deny**.
14.	Note the response:
```text
Here are some key remarks about int in the official documentation:
•	int in C# is a 32-bit signed integer, represented by System.Int32, with a value range from -2,147,483,648 to 2,147,483,647.
•	int is an immutable value type.
•	Integer division truncates the result, and the remainder can be obtained with the % operator.
•	int has minimum and maximum limits; exceeding these causes overflow or underflow, wrapping the value.
•	int is a value type (struct) and derives from System.Object.
•	Type conversions involving int may require explicit casting, and overflow can occur if the value is out of range.
```

You can read more about the Microsoft Learn MCP server at the following link: https://github.com/MicrosoftDocs/mcp. 

Another ten MCP servers that can accelerate your development workflow can be found at the following link: https://devblogs.microsoft.com/blog/10-microsoft-mcp-servers-to-accelerate-your-development-workflow.
