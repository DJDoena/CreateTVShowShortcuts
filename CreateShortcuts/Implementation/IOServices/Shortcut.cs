using IWshRuntimeLibrary;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations;

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
        get => _wshShortcut.Description;
        set => _wshShortcut.Description = value;
    }

    public string TargetPath
    {
        get => _wshShortcut.TargetPath;
        set => _wshShortcut.TargetPath = value;
    }

    public string WorkingFolder
    {
        get => _wshShortcut.WorkingDirectory;
        set => _wshShortcut.WorkingDirectory = value;
    }

    public string Arguments
    {
        get => _wshShortcut.Arguments;
        set => _wshShortcut.Arguments = value;
    }

    public string FullName
        => _wshShortcut.FullName;

    public string Hotkey
    {
        get => _wshShortcut.Hotkey;
        set => _wshShortcut.Hotkey = value;
    }

    public string IconLocation
    {
        get => _wshShortcut.IconLocation;
        set => _wshShortcut.IconLocation = value;
    }

    public int WindowStyle
    {
        get => _wshShortcut.WindowStyle;
        set => _wshShortcut.WindowStyle = value;
    }

    public void Save()
    {
        _logger?.WriteLine($"Create shortcut \"{_wshShortcut.FullName}\"", true);
        _logger?.WriteLine($"for             \"{_wshShortcut.TargetPath}\"");

        _wshShortcut.Save();
    }

    public void Load(string pathLink)
    {
        _logger?.WriteLine($"Load shortcut \"{_wshShortcut.FullName}\"", true);
        _logger?.WriteLine($"with target     \"{_wshShortcut.TargetPath}\"");

        _wshShortcut.Load(pathLink);
    }
}