using System;
using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestFolder : IFolder
    {
        private readonly FileSystemMock _fileSystemMock;

        private readonly IEnumerable<SearchPatternMatch> _searchPatternMatches;

        private readonly IObjectStorage _objectStorage;

        public TestFolder(FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectStorage os)
        {
            _fileSystemMock = fileSystemMock;
            _searchPatternMatches = searchPatternMatches;
            _objectStorage = os;
        }

        public string WorkingFolder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Exists(string path)
        {
            bool exists;

            exists = _fileSystemMock.Folders.Where(item => item.FullName == path).HasItems();

            return exists;
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public IFolderInfo CreateFolder(string path)
        {
            _objectStorage.Logger.WriteLine("Create virtual folder \"{0}\"", path);

            _fileSystemMock.AddFolder(path);

            return new FolderInfo(path);
        }

        public IEnumerable<string> GetFolderNames(string path
            , string searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<string> filtered;
            string[] folders;

            path = path + @"\";

            filtered = _fileSystemMock.Folders.Select(f => f.FullName);
            filtered = filtered.Where(item => item.StartsWith(path));
            filtered = filtered.Where(item => this.MatchesSearchPattern(item, path, searchPattern));

            filtered = filtered.OrderBy(item => item);

            folders = filtered.ToArray();

            return folders;
        }

        private bool MatchesSearchPattern(string item
            , string path
            , string searchPattern)
        {
            item = item.Replace(path, string.Empty);

            if (item.Contains(@"\"))
            {
                return false;
            }

            foreach (var match in _searchPatternMatches)
            {
                if (match.Pattern == searchPattern)
                {
                    bool isMatch;

                    isMatch = MatchesSearchPattern(item, match);

                    return isMatch;
                }
            }

            return item == searchPattern;
        }

        private static bool MatchesSearchPattern(string item
            , SearchPatternMatch match)
        {
            bool isMatch;

            if (match.Position == SearchPatternMatch.StringPosition.Start)
            {
                isMatch = item.StartsWith(match.Text);
            }
            else if (match.Position == SearchPatternMatch.StringPosition.End)
            {
                isMatch = item.EndsWith(match.Text);
            }
            else
            {
                isMatch = item.Contains(match.Text);
            }

            return isMatch;
        }

        public IEnumerable<string> GetFileNames(string path
            , string searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<string> filtered;
            string[] files;

            path = path + @"\";

            filtered = _fileSystemMock.Files.Select(f => f.FullName);
            filtered = filtered.Where(item => item.StartsWith(path));
            filtered = filtered = filtered.Where(item => this.MatchesSearchPattern(item, path, searchPattern));

            filtered = filtered.OrderBy(item => item);

            files = filtered.ToArray();

            return files;
        }

        public string GetFullPath(string folder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFolderInfo> GetFolderInfos(string folder, string searchPattern = "*.*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFileInfo> GetFileInfos(string folder, string searchPattern = "*.*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }
    }
}