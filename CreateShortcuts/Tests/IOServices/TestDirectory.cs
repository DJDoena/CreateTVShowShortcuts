using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.IOServices.Implementations;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class TestDirectory : IFolder
    {
        private readonly FileSystemMock FileSystemMock;

        private readonly IEnumerable<SearchPatternMatch> SearchPatternMatches;

        private readonly IObjectStorage ObjectStorage;

        public TestDirectory(FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectStorage os)
        {
            FileSystemMock = fileSystemMock;
            SearchPatternMatches = searchPatternMatches;
            ObjectStorage = os;
        }

        public String WorkingFolder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Boolean Exists(String path)
        {
            Boolean exists;

            exists = (FileSystemMock.Folders.Where(item => (item == path)).HasItems());

            return (exists);
        }

        public void Delete(String path)
        {
            throw new NotImplementedException();
        }

        public IFolderInfo CreateFolder(String path)
        {
            ObjectStorage.Logger.WriteLine("Create virtual folder \"{0}\"", path);

            FileSystemMock.AddFolder(path);

            return new FolderInfo(path);
        }

        public IEnumerable<String> GetFolderNames(String path
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered;
            String[] folders;

            path = path + @"\";

            filtered = FileSystemMock.Folders;
            filtered = filtered.Where(item => item.StartsWith(path));
            filtered = filtered.Where(item => MatchesSearchPattern(item, path, searchPattern));

            filtered = filtered.OrderBy(item => item);

            folders = filtered.ToArray();

            return (folders);
        }

        private Boolean MatchesSearchPattern(String item
            , String path
            , String searchPattern)
        {
            item = item.Replace(path, String.Empty);

            if (item.Contains(@"\"))
            {
                return (false);
            }

            foreach (SearchPatternMatch match in SearchPatternMatches)
            {
                if (match.Pattern == searchPattern)
                {
                    Boolean isMatch;

                    isMatch = MatchesSearchPattern(item, match);

                    return (isMatch);
                }
            }

            return (item == searchPattern);
        }

        private static Boolean MatchesSearchPattern(String item
            , SearchPatternMatch match)
        {
            Boolean isMatch;

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

            return (isMatch);
        }

        public IEnumerable<String> GetFileNames(String path
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered;
            String[] files;

            path = path + @"\";

            filtered = FileSystemMock.Files;
            filtered = filtered.Where(item => item.StartsWith(path));
            filtered = filtered = filtered.Where(item => MatchesSearchPattern(item, path, searchPattern));

            filtered = filtered.OrderBy(item => item);

            files = filtered.ToArray();

            return (files);
        }

        public string GetFullPath(string folder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFolderInfo> GetFolderInfos(string folder, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFileInfo> GetFileInfos(string folder, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }
    }
}