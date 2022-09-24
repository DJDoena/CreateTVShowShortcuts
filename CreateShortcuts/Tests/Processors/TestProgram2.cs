using System;
using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal sealed class TestProgram2 : TestProgramBase
    {
        private readonly String TestRoot;

        private readonly String ShortcutRoot;

        private readonly String VideoFolderRoot1;
        private readonly String VideoFolderRoot2;

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
                yield return (VideoFolderRoot1);
                yield return (VideoFolderRoot2);
            }
        }

        protected override String LogFileName
        {
            get
            {
                return ("TestProgram2.log");
            }
        }

        protected override IEnumerable<String> ExpectedWarnings
        {
            get
            {
                String path;

                path = ObjectStorage.IOServices.Path.Combine(ShortcutRoot, "Hurz", "Season 2.lnk");

                yield return (String.Format("Remove \"{0}\"", path));
            }
        }

        public TestProgram2()
        {
            String path;
            UInt16 count;
            IIOServices ioServices;

            ioServices = ObjectStorage.IOServices;

            TestRoot = ioServices.Path.Combine(ioServices.Path.GetTempPath(), "CreateShortcutTest");

            ShortcutRoot = ioServices.Path.Combine(TestRoot, "Shortcuts");

            VideoFolderRoot1 = ioServices.Path.Combine(TestRoot, "Videos1");
            VideoFolderRoot2 = ioServices.Path.Combine(TestRoot, "Videos2");

            if (ioServices.Folder.Exists(TestRoot))
            {
                ioServices.Folder.Delete(TestRoot);
            }
            ioServices.Folder.CreateFolder(TestRoot);

            count = 1;

            foreach (String videoFolderRoot in VideoFileFolders)
            {
                ioServices.Folder.CreateFolder(videoFolderRoot);

                path = ioServices.Path.Combine(videoFolderRoot, "Hurz");
                ioServices.Folder.CreateFolder(path);

                path = ioServices.Path.Combine(path, "Season " + count);
                ioServices.Folder.CreateFolder(path);
                if (count == 1)
                {
                    path = ioServices.Path.Combine(videoFolderRoot, "Hurz", "Season " + (count + 1));
                    ioServices.Folder.CreateFolder(path);
                }
                count++;
            }
        }

        protected override void Assert()
        {
            String path;
            IIOServices ioServices;

            ioServices = ObjectStorage.IOServices;

            path = ioServices.Path.Combine(ShortcutRoot, "Hurz");
            Debug.Assert(ioServices.Folder.Exists(path));

            AssertFileExists(ObjectStorage, path, "Season 1.lnk");
            AssertFileExists(ObjectStorage, path, "Season 2.lnk");

            AssertFileExists(ObjectStorage, VideoFolderRoot2, "Hurz", "Season 1.lnk");
        }
    }
}