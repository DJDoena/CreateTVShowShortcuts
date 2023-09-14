using IWshRuntimeLibrary;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    public sealed class Shortcut : IShortcut
    {
        private readonly IWshShortcut _wshShortcut;

        private static readonly WshShell _wshShell;

        private readonly ILogger _logger;

        static Shortcut()
        {
            _wshShell = new WshShell();
        }

        public Shortcut(string linkFileName, ILogger logger)
        {
            _logger = logger;

            _wshShortcut = (IWshShortcut)_wshShell.CreateShortcut(linkFileName);
        }

        public Shortcut(string linkFileName) : this(linkFileName, null)
        {
        }

        public string Description
        {
            set => _wshShortcut.Description = value;
        }

        public string TargetPath
        {
            set => _wshShortcut.TargetPath = value;
        }

        public string WorkingFolder
        {
            set => _wshShortcut.WorkingDirectory = value;
        }

        public void Save()
        {
            _logger?.WriteLine($"Create shortcut \"{_wshShortcut.FullName}\"", true);
            _logger?.WriteLine($"for             \"{_wshShortcut.TargetPath}\"");

            _wshShortcut.Save();
        }
    }
}