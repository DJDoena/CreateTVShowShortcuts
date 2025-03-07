using System.Collections.Generic;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class ShortcutFolderProcessor : IProcessor
    {
        private readonly IObjectStorage _objectStorage;

        private List<string> Warnings { get; set; }

        public ShortcutFolderProcessor(IObjectStorage os)
        {
            this.Warnings = new List<string>();

            var wp = os.WarningsProcessor;

            wp.AddWarnings(this.Warnings);

            _objectStorage = os;
        }

        public void Process()
        {
            foreach (var videoFileFolder in _objectStorage.Program.VideoFileFolders)
            {
                this.IterateOverVideoFileFolder(videoFileFolder);
            }
        }

        private void IterateOverVideoFileFolder(string videoFileFolder)
        {
            var seriesFolders = _objectStorage.IOServices.Folder.GetFolderNames(videoFileFolder, _objectStorage.Program.SeriesNamePattern);

            foreach (var seriesFolder in seriesFolders)
            {
                this.ProcessSeriesFolder(seriesFolder);
            }
        }

        private void ProcessSeriesFolder(string seriesFolder)
        {
            var ioServices = _objectStorage.IOServices;

            var seriesFolderDI = ioServices.GetFolder(seriesFolder);

            var seasonFolders = ioServices.Folder.GetFolderNames(seriesFolder, _objectStorage.Program.SeasonFolderPattern)
                .Union(ioServices.Folder.GetFolderNames(seriesFolder, _objectStorage.Program.StaffelFolderPattern)).ToArray();

            if (seasonFolders.Length > 0)
            {
                this.CreateShortcutForSeasonFolder(seasonFolders, seriesFolderDI.Name);
            }
            else if (_objectStorage.Helper.IsSpecialFolder(seriesFolderDI))
            {
                this.CreateShortcut(_objectStorage.Program.RootFolderForShortcutFiles, seriesFolder);
            }
        }

        private void CreateShortcutForSeasonFolder(IEnumerable<string> seasonFolders, string seriesFolderName)
        {
            var articleProcessor = _objectStorage.GetArticleProcessor(seriesFolderName, true);

            articleProcessor.Process();

            seriesFolderName = articleProcessor.SeriesName;

            var ioServices = _objectStorage.IOServices;

            var seriesFolderForShortcutFiles = ioServices.Path.Combine(_objectStorage.Program.RootFolderForShortcutFiles, seriesFolderName);

            if (this.FolderDoesNotExist(seriesFolderForShortcutFiles))
            {
                ioServices.Folder.CreateFolder(seriesFolderForShortcutFiles);
            }

            foreach (var seasonFolder in seasonFolders)
            {
                this.CreateShortcut(seriesFolderForShortcutFiles, seasonFolder);
            }
        }

        private void CreateShortcut(string seriesFolderForShortcutFiles, string seasonFolder)
        {
            var warning = _objectStorage.ShortcutCreator.Create(seriesFolderForShortcutFiles, seasonFolder);

            if (HasWarning(warning))
            {
                this.Warnings.Add(warning);
            }
        }

        private static bool HasWarning(string warning)
        {
            var hasWarning = string.IsNullOrEmpty(warning) == false;

            return hasWarning;
        }

        private bool FolderDoesNotExist(string folder)
        {
            var exists = _objectStorage.IOServices.Folder.Exists(folder);

            return exists == false;
        }
    }
}