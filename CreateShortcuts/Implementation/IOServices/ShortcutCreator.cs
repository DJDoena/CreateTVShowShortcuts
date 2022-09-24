using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Implementation.IOServices
{
    internal sealed class ShortcutCreator : IShortcutCreator
    {
        private readonly IObjectStorage ObjectStorage;

        private readonly IObjectFactory ObjectFactory;

        public ShortcutCreator(IObjectStorage objectStorage, IObjectFactory objectFactory)
        {
            ObjectStorage = objectStorage;
            ObjectFactory = objectFactory;
        }

        public string Create(string seriesFolderForShortcutFiles, string seasonFolder)
        {
            var ioServices = ObjectStorage.IOServices;

            string warning = null;

            var seasonFolderDI = ioServices.GetFolderInfo(seasonFolder);

            var linkFileName = seasonFolderDI.Name + ObjectStorage.Program.ShortcutExtension;
            linkFileName = ioServices.Path.Combine(seriesFolderForShortcutFiles, linkFileName);

            if (ioServices.File.Exists(linkFileName))
            {
                warning = string.Format("Remove \"{0}\"", linkFileName);

                ObjectStorage.Logger.WriteLine(warning);
            }

            var shortcut = ObjectFactory.CreateShortcut(linkFileName, ObjectStorage);
            shortcut.TargetPath = seasonFolder;
            shortcut.WorkingFolder = seasonFolder;
            shortcut.Description = seasonFolderDI.Name;
            shortcut.Save();

            return warning;
        }
    }
}