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
        private readonly IObjectStorage ObjectStorage;

        public VideoFolderProcessor(IObjectStorage os)
        {
            ObjectStorage = os;
        }

        public void Process()
        {
            foreach (var videoFileFolder in ObjectStorage.Program.VideoFileFolders)
            {
                IterateOverVideoFolder(videoFileFolder);
            }
        }

        private void IterateOverVideoFolder(string videoFileFolder)
        {
            RemoveExistingShortcuts(videoFileFolder);

            var ioServices = ObjectStorage.IOServices;

            var seriesFoldersInShortcutFolder = ioServices.Folder.GetFolderNames(ObjectStorage.Program.RootFolderForShortcutFiles
                , ObjectStorage.Program.SeriesNamePattern);

            foreach (var seriesFolderInShortcutFolder in seriesFoldersInShortcutFolder)
            {
                var seriesFolderDI = ioServices.GetFolderInfo(seriesFolderInShortcutFolder);

                if (ObjectStorage.Helper.IsSpecialFolder(seriesFolderDI))
                {
                    continue;
                }

                ProcessSeriesFolder(videoFileFolder, seriesFolderInShortcutFolder, seriesFolderDI.Name);
            }
        }

        private void ProcessSeriesFolder(string videoFileFolder, string seriesFolderInShortcutFolder, string seriesFolderName)
        {
            var articleProcessor = ObjectStorage.GetArticleProcessor(seriesFolderName, true);

            articleProcessor.Process();

            seriesFolderName = articleProcessor.SeriesName;

            var seriesFolderInVideoFolder = ObjectStorage.IOServices.Folder.GetFolderNames(videoFileFolder, seriesFolderName).FirstOrDefault();

            if (FolderNameIsValid(seriesFolderInVideoFolder))
            {
                var seasonFolderNames = GetSeasonFolderNames(seriesFolderInVideoFolder).ToList();

                if (seasonFolderNames.Count > 0)
                {
                    CopyShortcutsToSeriesFolder(seriesFolderInShortcutFolder, seriesFolderInVideoFolder, seasonFolderNames);
                }
            }
        }

        private void RemoveExistingShortcuts(string videoFileFolder)
        {
            var existingShortcuts = ObjectStorage.IOServices.Folder.GetFileNames(videoFileFolder, "*.lnk", SearchOption.AllDirectories);

            foreach (var file in existingShortcuts)
            {
                ObjectStorage.IOServices.File.Delete(file);
            }
        }

        private static bool FolderNameIsValid(string folderName)
            => string.IsNullOrEmpty(folderName) == false;

        private IEnumerable<string> GetSeasonFolderNames(string seriesFolder)
        {
            var ioServices = ObjectStorage.IOServices;

            var seasonFolderNames = ioServices.Folder.GetFolderNames(seriesFolder, ObjectStorage.Program.SeasonFolderPattern)
                .Union(ioServices.Folder.GetFolderNames(seriesFolder, ObjectStorage.Program.StaffelFolderPattern));

            seasonFolderNames = seasonFolderNames.Select(directory => ioServices.GetFolderInfo(directory).Name);

            return seasonFolderNames;
        }

        private void CopyShortcutsToSeriesFolder(string seriesFolderInShortcutFolder, string seriesFolderInVideoFolder, IEnumerable<string> seasonFolderNames)
        {
            var searchPattern = "*" + ObjectStorage.Program.ShortcutExtension;

            var shortcutFiles = ObjectStorage.IOServices.Folder.GetFileNames(seriesFolderInShortcutFolder, searchPattern);

            foreach (var shortcutFile in shortcutFiles)
            {
                CopyShortcutToSeriesFolder(seriesFolderInVideoFolder, seasonFolderNames, shortcutFile);
            }
        }

        private void CopyShortcutToSeriesFolder(string seriesFolderInVideoFolder, IEnumerable<string> seasonFolderNames, string shortcutFile)
        {
            var ioServices = ObjectStorage.IOServices;

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