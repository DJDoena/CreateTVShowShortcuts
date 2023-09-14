using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Implementation.IOServices
{
    internal sealed class ShortcutCreator : IShortcutCreator
    {
        private readonly IObjectStorage _objectStorage;

        private readonly IObjectFactory _objectFactory;

        public ShortcutCreator(IObjectStorage objectStorage, IObjectFactory objectFactory)
        {
            _objectStorage = objectStorage;
            _objectFactory = objectFactory;
        }

        public string Create(string seriesFolderForShortcutFiles, string seasonFolder)
        {
            var ioServices = _objectStorage.IOServices;

            string warning = null;

            var seasonFolderDI = ioServices.GetFolderInfo(seasonFolder);

            var linkFileName = seasonFolderDI.Name + _objectStorage.Program.ShortcutExtension;

            linkFileName = ioServices.Path.Combine(seriesFolderForShortcutFiles, linkFileName);

            if (ioServices.File.Exists(linkFileName))
            {
                warning = string.Format("Remove \"{0}\"", linkFileName);

                _objectStorage.Logger.WriteLine(warning);
            }

            var shortcut = _objectFactory.CreateShortcut(linkFileName, _objectStorage);

            shortcut.TargetPath = seasonFolder;
            shortcut.WorkingFolder = seasonFolder;
            shortcut.Description = seasonFolderDI.Name;
            shortcut.Save();

            return warning;
        }
    }
}