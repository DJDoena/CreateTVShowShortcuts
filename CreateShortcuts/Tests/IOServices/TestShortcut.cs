using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
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

        public string TargetPath { private get; set; }

        public string WorkingFolder { private get; set; }

        public string Description { private get; set; }

        public void Save()
        {
            ILogger logger;

            logger = ObjectStorage.Logger;

            logger.WriteLine("Create virtual shortcut \"{0}\"", true, FileName);
            logger.WriteLine("for virtual             \"{0}\"", this.TargetPath);

            FileSystemMock.AddFile(FileName, true);
        }
    }
}