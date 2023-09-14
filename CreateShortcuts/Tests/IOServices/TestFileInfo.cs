using System;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFileInfo : IFileInfo
    {
        private readonly string _fullName;

        private readonly FileSystemMock _fileSystemMock;

        private DateTime _timestamp;

        public TestFileInfo(string fullName
            , FileSystemMock fileSystemMock)
        {
            _fullName = fullName;
            _fileSystemMock = fileSystemMock;
            _timestamp = DateTime.Now;
        }

        public string Name => this.FullName.Split('\\').Last();

        public string Extension
        {
            get
            {
                var split = this.FullName.Split('.');

                var extension = split.Length > 1
                    ? $".{split.Last()}"
                    : string.Empty;

                return extension;
            }
        }

        public string FullName => _fullName;

        public IFolderInfo Folder => new TestFolderInfo(this.FolderName, _fileSystemMock);

        public string FolderName
        {
            get
            {
                var split = this.FullName.Split('\\');

                var folderName = split.Length > 1
                    ? this.FullName.Substring(0, this.FullName.LastIndexOf("\\"))
                    : string.Empty;

                return folderName;
            }
        }

        public string NameWithoutExtension
        {
            get
            {
                var split = this.FullName.Split('.');

                var nameWithoutExtension = split.Length > 1
                    ? this.Name.Substring(0, this.Name.LastIndexOf("."))
                    : string.Empty;

                return nameWithoutExtension;
            }
        }

        public bool Exists => _fileSystemMock.Files.Any(f => f.FullName == this.FullName);

        public ulong Length => throw new NotImplementedException();

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

        public bool Equals(IFileInfo other)
            => this.FullName.Equals(other?.FullName);
    }
}