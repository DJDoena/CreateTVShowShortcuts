using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class VideoFolderProcessor : IProcessor
    {
        private readonly IObjectStorage _objectStorage;

        public VideoFolderProcessor(IObjectStorage os)
        {
            _objectStorage = os;
        }

        public void Process()
        {
            foreach (var videoFileFolder in _objectStorage.Program.VideoFileFolders)
            {
                this.IterateOverVideoFolder(videoFileFolder);
            }
        }

        private void IterateOverVideoFolder(string videoFileFolder)
        {
            this.RemoveExistingShortcuts(videoFileFolder);

            var ioServices = _objectStorage.IOServices;

            var seriesFoldersInShortcutFolder = ioServices.Folder.GetFolderNames(_objectStorage.Program.RootFolderForShortcutFiles
                , _objectStorage.Program.SeriesNamePattern);

            foreach (var seriesFolderInShortcutFolder in seriesFoldersInShortcutFolder)
            {
                var seriesFolderDI = ioServices.GetFolderInfo(seriesFolderInShortcutFolder);

                if (_objectStorage.Helper.IsSpecialFolder(seriesFolderDI))
                {
                    continue;
                }

                this.ProcessSeriesFolder(videoFileFolder, seriesFolderInShortcutFolder, seriesFolderDI.Name);
            }
        }

        private void ProcessSeriesFolder(string videoFileFolder, string seriesFolderInShortcutFolder, string seriesFolderName)
        {
            var articleProcessor = _objectStorage.GetArticleProcessor(seriesFolderName, true);

            articleProcessor.Process();

            seriesFolderName = articleProcessor.SeriesName;

            var seriesFolderInVideoFolder = _objectStorage.IOServices.Folder.GetFolderNames(videoFileFolder, seriesFolderName).FirstOrDefault();

            if (FolderNameIsValid(seriesFolderInVideoFolder))
            {
                var seasonFolderNames = this.GetSeasonFolderNames(seriesFolderInVideoFolder).ToList();

                if (seasonFolderNames.Count > 0)
                {
                    this.CopyShortcutsToSeriesFolder(seriesFolderInShortcutFolder, seriesFolderInVideoFolder, seasonFolderNames);
                }
            }
        }

        private void RemoveExistingShortcuts(string videoFileFolder)
        {
            var existingShortcuts = _objectStorage.IOServices.Folder.GetFileNames(videoFileFolder, "*.lnk", SearchOption.AllDirectories);

            foreach (var file in existingShortcuts)
            {
                _objectStorage.IOServices.File.Delete(file);
            }
        }

        private static bool FolderNameIsValid(string folderName)
            => string.IsNullOrEmpty(folderName) == false;

        private IEnumerable<string> GetSeasonFolderNames(string seriesFolder)
        {
            var ioServices = _objectStorage.IOServices;

            var seasonFolderNames = ioServices.Folder.GetFolderNames(seriesFolder, _objectStorage.Program.SeasonFolderPattern)
                .Union(ioServices.Folder.GetFolderNames(seriesFolder, _objectStorage.Program.StaffelFolderPattern));

            seasonFolderNames = seasonFolderNames.Select(directory => ioServices.GetFolderInfo(directory).Name);

            return seasonFolderNames;
        }

        private void CopyShortcutsToSeriesFolder(string seriesFolderInShortcutFolder, string seriesFolderInVideoFolder, IEnumerable<string> seasonFolderNames)
        {
            var searchPattern = "*" + _objectStorage.Program.ShortcutExtension;

            var shortcutFiles = _objectStorage.IOServices.Folder.GetFileNames(seriesFolderInShortcutFolder, searchPattern);

            foreach (var shortcutFile in shortcutFiles)
            {
                this.CopyShortcutToSeriesFolder(seriesFolderInVideoFolder, seasonFolderNames, shortcutFile);
            }
        }

        private void CopyShortcutToSeriesFolder(string seriesFolderInVideoFolder, IEnumerable<string> seasonFolderNames, string shortcutFile)
        {
            var ioServices = _objectStorage.IOServices;

            var shortcutFileFI = ioServices.GetFileInfo(shortcutFile);

            var shortcutName = GetShortcutName(shortcutFileFI);

            if (SeasonFolderDoesNotExist(seasonFolderNames, shortcutName))
            {
                var targetFile = ioServices.Path.Combine(seriesFolderInVideoFolder, shortcutFileFI.Name);

                ioServices.File.Copy(shortcutFile, targetFile);

                var targetFileFI = ioServices.GetFileInfo(targetFile);

                var folderInfo = targetFileFI.Folder;

                targetFileFI.CreationTime = folderInfo.CreationTime;
                targetFileFI.LastWriteTime = folderInfo.CreationTime;

                var subFolderInfos = ioServices.Folder.GetFolderNames(folderInfo.FullName).Select(fi => ioServices.GetFolderInfo(fi)).ToList();

                var fileInfos = ioServices.Folder.GetFileNames(folderInfo.FullName).Select(fi => ioServices.GetFileInfo(fi)).ToList();

                try
                {
                    folderInfo.CreationTime = subFolderInfos.Select(fi => fi.CreationTime).Concat(fileInfos.Select(fi => fi.CreationTime)).Min();
                    folderInfo.LastWriteTime = subFolderInfos.Select(fi => fi.LastWriteTime).Concat(fileInfos.Select(fi => fi.LastWriteTime)).Max();
                }
                catch
                {
                }
            }
        }

        private static string GetShortcutName(IFileInfo shortcutFileFI)
        {
            var shortcutName = shortcutFileFI.Name;

            shortcutName = shortcutName.Substring(0, shortcutName.Length - shortcutFileFI.Extension.Length);

            return shortcutName;
        }

        private static bool SeasonFolderDoesNotExist(IEnumerable<string> seasonFolders, string shortcutName)
            => seasonFolders.Contains(shortcutName) == false;
    }
}