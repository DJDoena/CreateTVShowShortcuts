using IWshRuntimeLibrary;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    public sealed class Shortcut : IShortcut
    {
        private readonly IWshShortcut WshShortcut;

        private static readonly WshShell WshShell;

        private readonly ILogger Logger;

        static Shortcut()
        {
            WshShell = new WshShell();
        }

        public Shortcut(string linkFileName, ILogger logger)
        {
            Logger = logger;

            WshShortcut = (IWshShortcut)(WshShell.CreateShortcut(linkFileName));
        }

        public Shortcut(string linkFileName) : this(linkFileName, null)
        {
        }

        public string Description
        {
            set
            {
                WshShortcut.Description = value;
            }
        }

        public string TargetPath
        {
            set
            {
                WshShortcut.TargetPath = value;
            }
        }

        public string WorkingFolder
        {
            set
            {
                WshShortcut.WorkingDirectory = value;
            }
        }

        public void Save()
        {
            Logger?.WriteLine($"Create shortcut \"{WshShortcut.FullName}\"", true);
            Logger?.WriteLine($"for             \"{WshShortcut.TargetPath}\"");

            WshShortcut.Save();
        }
    }
}