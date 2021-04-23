using System;
using System.IO;
using System.Runtime.InteropServices;

// https://stackoverflow.com/questions/6374673/unblock-file-from-within-net-4-c-sharp
[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool DeleteFile(string name);

Console.WriteLine("Enter directory to unblock:");
string directory = Console.ReadLine();

if (!Directory.Exists(directory))
{
    Console.WriteLine("Directory does not exist");
    Console.ReadKey();
    return;
}

Console.WriteLine("Unblock sub-dirs? [y/n]");
ConsoleKeyInfo key = Console.ReadKey();

if (key.Key == ConsoleKey.Y)
{
    foreach (string file in Directory.GetFiles(directory!, "*", SearchOption.AllDirectories))
        DeleteFile(file + ":Zone.Identifier");
}
else
{
    foreach (string file in Directory.GetFiles(directory!, "*"))
        DeleteFile(file + ":Zone.Identifier");
}