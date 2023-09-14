using System;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFile : IFile
    {
        private readonly FileSystemMock _fileSystemMock;

        private readonly IObjectStorage _objectStorage;

        public TestFile(FileSystemMock fileSystemMock
            , IObjectStorage os)
        {
            _fileSystemMock = fileSystemMock;
            _objectStorage = os;
        }

        public bool Exists(string path)
        {
            bool exists;

            exists = _fileSystemMock.Files.Select(f => f.FullName).Contains(path);

            return exists;
        }

        public void Copy(string sourceFileName
            , string destFileName
            , bool overwrite = true)
        {
            ILogger logger;

            if (_fileSystemMock.Files.Select(f => f.FullName).Contains(sourceFileName) == false)
            {
                throw new NotSupportedException();
            }

            logger = _objectStorage.Logger;

            logger.WriteLine("Copy virtual file \"{0}\"", true, sourceFileName);
            logger.WriteLine("to virtual        \"{0}\"", destFileName);

            _fileSystemMock.AddFile(destFileName, overwrite);
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string oldFileName
            , string newFileName
            , bool overwrite)
        {
            throw new NotImplementedException();
        }

        public void SetAttributes(string fullName
            , System.IO.FileAttributes fileAttributes)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream Create(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}