using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestIOServices : IIOServices
    {
        private readonly FileSystemMock _fileSystemMock;

        private readonly Assembly _assembly;

        public TestIOServices(FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectStorage os)
        {
            _fileSystemMock = fileSystemMock;
            _assembly = typeof(IIOServices).Assembly;

            this.Path = this.Instantiate<IPath>("DoenaSoft.AbstractionLayer.IOServices.Path");

            this.Folder = new TestFolder(fileSystemMock, searchPatternMatches, os);

            this.File = new TestFile(fileSystemMock, os);
        }

        public IPath Path { get; private set; }

        public IFolder Folder { get; private set; }

        public IFile File { get; private set; }

        public IFileInfo GetFileInfo(string fileName)
        {
            var fi = _fileSystemMock.Files.FirstOrDefault(f => f.FullName == fileName);

            return fi ?? new TestFileInfo(fileName, _fileSystemMock);
        }

        public IFolderInfo GetFolderInfo(string path)
        {
            var fi = _fileSystemMock.Folders.FirstOrDefault(f => f.FullName == path);

            return fi ?? new TestFolderInfo(path, _fileSystemMock);
        }

        public System.IO.Stream GetFileStream(string fileName
            , System.IO.FileMode mode
            , System.IO.FileAccess access
            , System.IO.FileShare share)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDriveInfo> GetDriveInfos(System.IO.DriveType? driveType)
        {
            throw new NotImplementedException();
        }

        public IDriveInfo GetDriveInfo(string driveLetter)
        {
            throw new NotImplementedException();
        }

        public System.IO.StreamWriter GetStreamWriter(System.IO.Stream stream
            , Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public IFileSystemWatcher GetFileSystemWatcher(string path
            , string filter = null)
        {
            throw new NotImplementedException();
        }

        private T Instantiate<T>(string typeName
            , params object[] args)
        {
            var type = _assembly.GetType(typeName);

            var instance = (T)Activator.CreateInstance(type, args);

            return instance;
        }
    }
}