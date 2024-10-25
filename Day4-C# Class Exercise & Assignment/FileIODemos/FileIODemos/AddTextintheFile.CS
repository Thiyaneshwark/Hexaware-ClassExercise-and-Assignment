using System.Text;
string path = "D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Text.txt";
string path1 = @"D:\HEXAWARE\C#\DOT NET FSD\\Assignments\Day4-C# Class Exercise\Text.txt";
if (File.Exists(path))
{
    File.Delete(path);
}
using (FileStream fs = File.Create(path))
{
    //AddTextintheFile(fs, "Hello!.....");
    Console.WriteLine("Enter the content to write in the File");
    string content = Console.ReadLine();
    AddTextintheFile(fs, content);

    //AddTextintheFile(fs, "\n Welcome!.....");
    //AddTextintheFile(fs, "\r Welcome!.....");
}
//Console.ReadLine();
static void AddTextintheFile(FileStream fs, string input)
{
    byte[] byteInfo = new UTF32Encoding().GetBytes(input);
    fs.Write(byteInfo, 0, byteInfo.Length);
}