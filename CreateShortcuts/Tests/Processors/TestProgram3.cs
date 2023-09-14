using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal sealed class TestProgram3 : TestProgramBase
    {
        private readonly string TestRoot;

        private readonly string ShortcutRoot;

        public override string RootFolderForShortcutFiles
        {
            get
            {
                return ShortcutRoot;
            }
        }

        public override IEnumerable<string> VideoFileFolders
        {
            get
            {
                return Defaults.VideoFileFolders;
            }
        }

        public TestProgram3()
        {
            IIOServices ioServices;

            ioServices = _objectStorage.IOServices;

            TestRoot = ioServices.Path.Combine(ioServices.Path.GetTempPath(), "CreateShortcutTest");

            ShortcutRoot = ioServices.Path.Combine(TestRoot, "Shortcuts");

            if (ioServices.Folder.Exists(TestRoot))
            {
                ioServices.Folder.Delete(TestRoot);
            }
            ioServices.Folder.CreateFolder(TestRoot);

            foreach (var folder in this.VideoFileFolders)
            {
                AddVideoFolderToTest(ioServices, folder);
            }
        }

        private static void AddVideoFolderToTest(IIOServices ioServices
            , string folder)
        {
            if (System.IO.Directory.Exists(folder))
            {
                IEnumerable<string> folders;

                folders = System.IO.Directory.GetDirectories(folder, Defaults.SeriesNamePattern, System.IO.SearchOption.AllDirectories);

                AddSubFoldersToTest(ioServices, folders);
            }
        }

        private static void AddSubFoldersToTest(IIOServices ioServices
            , IEnumerable<string> folders)
        {
            foreach (var folder in folders)
            {
                ioServices.Folder.CreateFolder(folder);
            }
        }

        protected override void Assert()
        {
        }
    }
}