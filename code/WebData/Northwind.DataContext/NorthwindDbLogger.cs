using static System.Environment;

namespace Northwind.EntityModels;

public class NorthwindDbLogger
{
  public static void WriteLine(string message)
  {
    string folder = Path.Combine(GetFolderPath(
      SpecialFolder.DesktopDirectory), "book-logs");

    if (!Directory.Exists(folder))
      Directory.CreateDirectory(folder);

    string dateTimeStamp = DateTime.Now.ToString(
      "yyyy-MM-dd_HH-mm-ss");

    string path = Path.Combine(folder, 
      $"northwindlog-{dateTimeStamp}.txt");

    StreamWriter textFile = File.AppendText(path);
    textFile.WriteLine(message);
    textFile.Close();
  }
}
