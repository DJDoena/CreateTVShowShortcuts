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

        private static readonly Assembly _assembly;

        static TestIOServices()
        {
            _assembly = typeof(IIOServices).Assembly;
        }

        public TestIOServices(FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectStorage os)
        {
            _fileSystemMock = fileSystemMock;

            this.Path = Instantiate<IPath>("DoenaSoft.AbstractionLayer.IOServices.Path", this);

            this.Folder = new TestFolder(this, fileSystemMock, searchPatternMatches, os);

            this.File = new TestFile(fileSystemMock, os);
        }

        public IPath Path { get; private set; }

        public IFolder Folder { get; private set; }

        public IFile File { get; private set; }

        public IFileInfo GetFile(string fileName)
        {
            var fi = _fileSystemMock.Files.FirstOrDefault(f => f.FullName == fileName);

            return fi ?? new TestFileInfo(fileName, _fileSystemMock);
        }

        public IFolderInfo GetFolder(string path)
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

        public IEnumerable<IDriveInfo> GetDrives(System.IO.DriveType? driveType)
        {
            throw new NotImplementedException();
        }

        public IDriveInfo GetDrive(string driveLetter)
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

        internal static T Instantiate<T>(string typeName
            , params object[] args)
        {
            var type = _assembly.GetType(typeName);

            var instance = (T)Activator.CreateInstance(type, args);

            return instance;
        }

        public IRenameQueue CreateRenameQueue(ILogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}