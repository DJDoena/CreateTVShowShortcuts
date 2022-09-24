using System.Collections.Generic;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class ShortcutFolderProcessor : IProcessor
    {
        private List<string> Warnings { get; set; }

        private readonly IObjectStorage ObjectStorage;

        public ShortcutFolderProcessor(IObjectStorage os)
        {
            Warnings = new List<string>();

            var wp = os.WarningsProcessor;
            wp.AddWarnings(Warnings);

            ObjectStorage = os;
        }

        public void Process()
        {
            foreach (var videoFileFolder in ObjectStorage.Program.VideoFileFolders)
            {
                IterateOverVideoFileFolder(videoFileFolder);
            }
        }

        private void IterateOverVideoFileFolder(string videoFileFolder)
        {
            var seriesFolders = ObjectStorage.IOServices.Folder.GetFolderNames(videoFileFolder, ObjectStorage.Program.SeriesNamePattern);

            foreach (var seriesFolder in seriesFolders)
            {
                ProcessSeriesFolder(seriesFolder);
            }
        }

        private void ProcessSeriesFolder(string seriesFolder)
        {
            var ioServices = ObjectStorage.IOServices;

            var seriesFolderDI = ioServices.GetFolderInfo(seriesFolder);

            var seasonFolders = ioServices.Folder.GetFolderNames(seriesFolder, ObjectStorage.Program.SeasonFolderPattern)
                .Union(ioServices.Folder.GetFolderNames(seriesFolder, ObjectStorage.Program.StaffelFolderPattern)).ToArray();

            if (seasonFolders.Length > 0)
            {
                CreateShortcutForSeasonFolder(seasonFolders, seriesFolderDI.Name);
            }
            else if (ObjectStorage.Helper.IsSpecialFolder(seriesFolderDI))
            {
                CreateShortcut(ObjectStorage.Program.RootFolderForShortcutFiles, seriesFolder);
            }
        }

        private void CreateShortcutForSeasonFolder(IEnumerable<string> seasonFolders, string seriesFolderName)
        {
            var articleProcessor = ObjectStorage.GetArticleProcessor(seriesFolderName, true);

            articleProcessor.Process();

            seriesFolderName = articleProcessor.SeriesName;

            var ioServices = ObjectStorage.IOServices;

            var seriesFolderForShortcutFiles = ioServices.Path.Combine(ObjectStorage.Program.RootFolderForShortcutFiles, seriesFolderName);

            if (FolderDoesNotExist(seriesFolderForShortcutFiles))
            {
                ioServices.Folder.CreateFolder(seriesFolderForShortcutFiles);
            }

            foreach (var seasonFolder in seasonFolders)
            {
                CreateShortcut(seriesFolderForShortcutFiles, seasonFolder);
            }
        }

        private void CreateShortcut(string seriesFolderForShortcutFiles, string seasonFolder)
        {
            var warning = ObjectStorage.ShortcutCreator.Create(seriesFolderForShortcutFiles, seasonFolder);

            if (HasWarning(warning))
            {
                Warnings.Add(warning);
            }
        }

        private static bool HasWarning(string warning)
        {
            var hasWarning = string.IsNullOrEmpty(warning) == false;

            return hasWarning;
        }

        private bool FolderDoesNotExist(string folder)
        {
            var exists = ObjectStorage.IOServices.Folder.Exists(folder);

            return exists == false;
        }
    }
}