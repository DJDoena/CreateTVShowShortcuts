using System;
using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class FileSystemMock
    {
        private readonly List<IFileInfo> _files;

        private readonly List<IFolderInfo> _folders;

        public IEnumerable<IFileInfo> Files
        {
            get
            {
                return _files;
            }
        }

        public IEnumerable<IFolderInfo> Folders
        {
            get
            {
                return _folders;
            }
        }

        public FileSystemMock()
        {
            _files = new List<IFileInfo>();
            _folders = new List<IFolderInfo>();
        }

        internal void AddFolder(string path)
        {
            if (!_folders.Any(f => f.FullName == path))
            {
                _folders.Add(new TestFolderInfo(path, this));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        internal void AddFile(string fileName, bool overwrite)
        {
            if (!_files.Any(f => f.FullName == fileName))
            {
                _files.Add(new TestFileInfo(fileName, this));
            }
            else if (!overwrite)
            {
                throw new NotSupportedException();
            }
        }
    }
}