using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts
{
    public sealed class ActualProgram : IProgram
    {
        private readonly IEnumerable<string> Arguments;

        public ActualProgram(IEnumerable<string> arguments)
        {
            Arguments = arguments;
        }

        public string RootFolderForShortcutFiles
            => (Defaults.RootFolderForShortcutFiles);

        public string SeasonFolderPattern
            => (Defaults.SeasonFolderPattern);

        public string StaffelFolderPattern
            => (Defaults.StaffelFolderPattern);

        public string SeriesNamePattern
            => (Defaults.SeriesNamePattern);

        public string ShortcutExtension
            => (Defaults.ShortcutExtension);

        public IEnumerable<string> VideoFileFolders
            => (Defaults.VideoFileFolders);

        public void Process()
        {
            var of = new ObjectFactory();

            using (var os = of.CreateObjectStorage(this, Arguments))
            {
                var ap = os.ArgumentsProcessor;

                ap.Process();

                if (ap.ShowHelp)
                {
                    ap.PrintArguments();

                    return;
                }

                var wp = os.WarningsProcessor;

                wp.Process();

                if (os.IOServices.Folder.Exists(RootFolderForShortcutFiles))
                {
                    os.IOServices.Folder.Delete(RootFolderForShortcutFiles);
                }

                os.IOServices.Folder.CreateFolder(RootFolderForShortcutFiles);

                var sfp = os.ShortcutFolderProcessor;

                sfp.Process();

                var vfp = os.VideoFolderProcessor;

                vfp.Process();

                wp.Process();
            }
        }

        public void Dispose()
        {
        }
    }
}