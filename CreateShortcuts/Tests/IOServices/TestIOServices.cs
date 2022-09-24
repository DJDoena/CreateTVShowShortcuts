using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestIOServices : IIOServices
    {
        private readonly Assembly Assembly;

        public TestIOServices(FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectStorage os)
        {
            Assembly = typeof(IIOServices).Assembly;

            Path = Instantiate<IPath>("DoenaSoft.AbstractionLayer.IOServices.Implementations.Path");

            Folder = new TestDirectory(fileSystemMock, searchPatternMatches, os);

            File = new TestFile(fileSystemMock, os);
        }

        public IPath Path { get; private set; }

        public IFolder Folder { get; private set; }

        public IFile File { get; private set; }

        public IFileInfo GetFileInfo(String fileName)
            => (Instantiate<IFileInfo>("DoenaSoft.AbstractionLayer.IOServices.Implementations.FileInfo", fileName));

        public IFolderInfo GetFolderInfo(String path)
            => (Instantiate<IFolderInfo>("DoenaSoft.AbstractionLayer.IOServices.Implementations.FolderInfo", path));

        public System.IO.Stream GetFileStream(String fileName
            , System.IO.FileMode mode
            , System.IO.FileAccess access
            , System.IO.FileShare share)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDriveInfo> GetDriveInfos(Nullable<System.IO.DriveType> driveType)
        {
            throw new NotImplementedException();
        }

        public IDriveInfo GetDriveInfo(String driveLetter)
        {
            throw new NotImplementedException();
        }

        public System.IO.StreamWriter GetStreamWriter(System.IO.Stream stream
            , Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public IFileSystemWatcher GetFileSystemWatcher(String path
            , String filter = null)
        {
            throw new NotImplementedException();
        }

        private T Instantiate<T>(String typeName
            , params Object[] args)
        {
            Type type = Assembly.GetType(typeName);

            T instance = (T)(Activator.CreateInstance(type, args));

            return (instance);
        }
    }
}