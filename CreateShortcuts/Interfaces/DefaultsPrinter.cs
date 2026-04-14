using System;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Interfaces;

public static class DefaultsPrinter
{
    public static void PrintDefaults(this IProgram program)
    {
        Console.WriteLine("Program:");
        Console.WriteLine(program.GetType().FullName);
        Console.WriteLine();

        Console.WriteLine("RootFolderForShortcutFiles:");
        Console.WriteLine(program.RootFolderForShortcutFiles);
        Console.WriteLine();

        Console.WriteLine("SeriesNamePattern:");
        Console.WriteLine(program.SeriesNamePattern);
        Console.WriteLine();

        Console.WriteLine("SeasonFolderPattern:");
        Console.WriteLine(program.SeasonFolderPattern);
        Console.WriteLine();

        Console.WriteLine("ShortcutExtension:");
        Console.WriteLine(program.ShortcutExtension);
        Console.WriteLine();

        Console.WriteLine("VideoFileFolders:");
        foreach (var folder in program.VideoFileFolders)
        {
            Console.WriteLine(folder);
        }
        Console.WriteLine();

        Console.WriteLine();
    }

    public static void PrintDefaults()
    {
        Console.WriteLine("Program:");
        Console.WriteLine(typeof(Defaults).FullName);
        Console.WriteLine();

        Console.WriteLine("RootFolderForShortcutFiles:");
        Console.WriteLine(Defaults.RootFolderForShortcutFiles);
        Console.WriteLine();

        Console.WriteLine("SeriesNamePattern:");
        Console.WriteLine(Defaults.SeriesNamePattern);
        Console.WriteLine();

        Console.WriteLine("SeasonFolderPattern:");
        Console.WriteLine(Defaults.SeasonFolderPattern);
        Console.WriteLine();

        Console.WriteLine("StaffelFolderPattern:");
        Console.WriteLine(Defaults.StaffelFolderPattern);
        Console.WriteLine();

        Console.WriteLine("ShortcutExtension:");
        Console.WriteLine(Defaults.ShortcutExtension);
        Console.WriteLine();

        Console.WriteLine("VideoFileFolders:");
        foreach (var folder in Defaults.VideoFileFolders)
        {
            Console.WriteLine(folder);
        }
        Console.WriteLine();

        Console.WriteLine();
    }
}