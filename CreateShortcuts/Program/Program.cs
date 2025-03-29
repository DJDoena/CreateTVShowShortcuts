using System;
using DoenaSoft.CreateShortcuts.Interfaces;

namespace DoenaSoft.CreateShortcuts;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"v{typeof(Program).Assembly.GetName().Version}");

        try
        {
            using var program = new ActualProgram(args);

            program.PrintDefaults();

            program.Process();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            Console.ReadLine();
        }
    }
}