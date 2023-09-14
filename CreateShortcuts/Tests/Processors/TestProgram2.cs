using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal sealed class TestProgram2 : TestProgramBase
    {
        private readonly string TestRoot;

        private readonly string ShortcutRoot;

        private readonly string VideoFolderRoot1;
        private readonly string VideoFolderRoot2;

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
                yield return VideoFolderRoot1;
                yield return VideoFolderRoot2;
            }
        }

        protected override IEnumerable<string> ExpectedWarnings
        {
            get
            {
                string path;

                path = _objectStorage.IOServices.Path.Combine(ShortcutRoot, "Hurz", "Season 2.lnk");

                yield return string.Format("Remove \"{0}\"", path);
            }
        }

        public TestProgram2()
        {
            string path;
            ushort count;
            IIOServices ioServices;

            ioServices = _objectStorage.IOServices;

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

            foreach (var videoFolderRoot in this.VideoFileFolders)
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
            string path;
            IIOServices ioServices;

            ioServices = _objectStorage.IOServices;

            path = ioServices.Path.Combine(ShortcutRoot, "Hurz");
            Debug.Assert(ioServices.Folder.Exists(path));

            this.AssertFileExists(_objectStorage, path, "Season 1.lnk");
            this.AssertFileExists(_objectStorage, path, "Season 2.lnk");

            this.AssertFileExists(_objectStorage, VideoFolderRoot2, "Hurz", "Season 1.lnk");
        }
    }
}