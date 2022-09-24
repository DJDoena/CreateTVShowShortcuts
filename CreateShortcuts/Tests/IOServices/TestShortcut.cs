using System;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestShortcut : IShortcut
    {
        private readonly String FileName;

        private readonly FileSystemMock FileSystemMock;

        private readonly IObjectStorage ObjectStorage;

        public TestShortcut(String fileName
            , FileSystemMock fileSystemMock
            , IObjectStorage os)
        {
            FileName = fileName;
            FileSystemMock = fileSystemMock;
            ObjectStorage = os;
        }

        public String TargetPath { private get; set; }

        public String WorkingFolder { private get; set; }

        public String Description { private get; set; }

        public void Save()
        {
            ILogger logger;

            logger = ObjectStorage.Logger;

            logger.WriteLine("Create virtual shortcut \"{0}\"", true, FileName);
            logger.WriteLine("for virtual             \"{0}\"", TargetPath);

            FileSystemMock.AddFile(FileName);
        }
    }
}