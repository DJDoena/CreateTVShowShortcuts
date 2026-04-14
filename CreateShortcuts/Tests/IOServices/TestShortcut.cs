using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices;

internal sealed class TestShortcut : IShortcut
{
    private readonly string FileName;

    private readonly FileSystemMock FileSystemMock;

    private readonly IObjectStorage ObjectStorage;

    public TestShortcut(string fileName
        , FileSystemMock fileSystemMock
        , IObjectStorage os)
    {
        FileName = fileName;
        FileSystemMock = fileSystemMock;
        ObjectStorage = os;
    }

    public string TargetPath { get; set; }

    public string WorkingFolder { get; set; }

    public string Description { get; set; }

    public string Arguments { get; set; }

    public string FullName { get; }

    public string Hotkey { get; set; }

    public string IconLocation { get; set; }

    public int WindowStyle { get; set; }

    public void Save()
    {
        ILogger logger;

        logger = ObjectStorage.Logger;

        logger.WriteLine("Create virtual shortcut \"{0}\"", true, FileName);
        logger.WriteLine("for virtual             \"{0}\"", this.TargetPath);

        FileSystemMock.AddFile(FileName, true);
    }

    public void Load(string pathLink)
    {
        throw new System.NotImplementedException();
    }
}