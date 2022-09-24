using System;
using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal sealed class TestProgram3 : TestProgramBase
    {
        private readonly String TestRoot;

        private readonly String ShortcutRoot;

        public override String RootFolderForShortcutFiles
        {
            get
            {
                return (ShortcutRoot);
            }
        }

        public override IEnumerable<String> VideoFileFolders
        {
            get
            {
                return (Defaults.VideoFileFolders);
            }
        }

        protected override String LogFileName
        {
            get
            {
                return ("TestProgram3.log");
            }
        }

        public TestProgram3()
        {
            IIOServices ioServices;

            ioServices = ObjectStorage.IOServices;

            TestRoot = ioServices.Path.Combine(ioServices.Path.GetTempPath(), "CreateShortcutTest");

            ShortcutRoot = ioServices.Path.Combine(TestRoot, "Shortcuts");

            if (ioServices.Folder.Exists(TestRoot))
            {
                ioServices.Folder.Delete(TestRoot);
            }
            ioServices.Folder.CreateFolder(TestRoot);

            foreach (String folder in VideoFileFolders)
            {
                AddVideoFolderToTest(ioServices, folder);
            }
        }

        private static void AddVideoFolderToTest(IIOServices ioServices
            , String folder)
        {
            if (System.IO.Directory.Exists(folder))
            {
                IEnumerable<String> folders;

                folders = System.IO.Directory.GetDirectories(folder, Defaults.SeriesNamePattern, System.IO.SearchOption.AllDirectories);

                AddSubFoldersToTest(ioServices, folders);
            }
        }

        private static void AddSubFoldersToTest(IIOServices ioServices
            , IEnumerable<String> folders)
        {
            foreach (String folder in folders)
            {
                ioServices.Folder.CreateFolder(folder);
            }
        }

        protected override void Assert()
        {
        }
    }
}