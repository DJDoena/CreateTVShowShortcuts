using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFile : IFile
    {
        private readonly FileSystemMock _fileSystemMock;

        private readonly IObjectStorage _objectStorage;

        IIOServices IIOServiceItem.IOServices => throw new NotImplementedException();

        public IIOServices IOServices => throw new NotImplementedException();

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

        StreamWriter IFile.CreateText(string path)
        {
            throw new NotImplementedException();
        }

        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            throw new NotImplementedException();
        }

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void AppendAllText(string path, string contents)
        {
            throw new NotImplementedException();
        }

        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public StreamWriter AppendText(string path)
        {
            throw new NotImplementedException();
        }

        public FileAttributes GetAttributes(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            throw new NotImplementedException();
        }

        public Stream OpenRead(string path)
        {
            throw new NotImplementedException();
        }

        public StreamReader OpenText(string path)
        {
            throw new NotImplementedException();
        }

        public Stream OpenWrite(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(string path)
        {
            throw new NotImplementedException();
        }

        public string[] ReadAllLines(string path)
        {
            throw new NotImplementedException();
        }

        public string[] ReadAllLines(string path, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            throw new NotImplementedException();
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            throw new NotImplementedException();
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            throw new NotImplementedException();
        }

        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, string[] contents)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string contents)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }
    }
}