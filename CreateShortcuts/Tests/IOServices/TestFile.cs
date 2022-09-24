using System;
using System.IO;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFile : IFile
    {
        private readonly FileSystemMock FileSystemMock;

        private readonly IObjectStorage ObjectStorage;

        public TestFile(FileSystemMock fileSystemMock
            , IObjectStorage os)
        {
            FileSystemMock = fileSystemMock;
            ObjectStorage = os;
        }

        public Boolean Exists(String path)
        {
            Boolean exists;

            exists = FileSystemMock.Files.Contains(path);

            return (exists);
        }

        public void Copy(String sourceFileName
            , String destFileName
            , Boolean overwrite = true)
        {
            ILogger logger;

            if (FileSystemMock.Files.Contains(sourceFileName) == false)
            {
                throw (new NotSupportedException());
            }

            logger = ObjectStorage.Logger;

            logger.WriteLine("Copy virtual file \"{0}\"", true, sourceFileName);
            logger.WriteLine("to virtual        \"{0}\"", destFileName);

            FileSystemMock.AddFile(destFileName);
        }

        public void Delete(String path)
        {
            throw new NotImplementedException();
        }

        public void Move(String oldFileName
            , String newFileName
            , Boolean overwrite)
        {
            throw new NotImplementedException();
        }

        public void SetAttributes(String fullName
            , System.IO.FileAttributes fileAttributes)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}