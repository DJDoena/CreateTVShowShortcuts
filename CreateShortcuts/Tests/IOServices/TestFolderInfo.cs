using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFolderInfo : IFolderInfo
    {
        private readonly string _path;

        private readonly FileSystemMock _fileSystemMock;

        private DateTime _timestamp;

        public TestFolderInfo(string path
            , FileSystemMock fileSystemMock)
        {
            _path = path;
            _fileSystemMock = fileSystemMock;
            _timestamp = DateTime.Now;
        }

        public string Name => this.FullName.Split('\\').Last();

        public IFolderInfo Root => new TestFolderInfo(this.FullName.Split('\\').First(), _fileSystemMock);

        public bool Exists => _fileSystemMock.Folders.Any(f => f.FullName == this.FullName);

        public string FullName => _path;

        public DateTime LastWriteTime
        {
            get
            {
                if (this.Exists)
                {
                    return _timestamp;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            set
            {
                if (this.Exists)
                {
                    _timestamp = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public DateTime LastWriteTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime CreationTime
        {
            get
            {
                if (this.Exists)
                {
                    return _timestamp;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            set
            {
                if (this.Exists)
                {
                    _timestamp = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public DateTime CreationTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime LastAccessTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime LastAccessTimeUtc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IIOServices IFolderInfo.IOServices => throw new NotImplementedException();

        IDriveInfo IFolderInfo.Drive => throw new NotImplementedException();

        public void Create()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFolderInfo other)
            => this.FullName.Equals(other?.FullName);

        public IEnumerable<IFileInfo> GetFileInfos(string searchPattern, System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IFolderInfo> IFolderInfo.GetDirectories(string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IFileInfo> IFolderInfo.GetFiles(string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IFolderInfo> IFolderInfo.GetFolderInfos(string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }
    }
}