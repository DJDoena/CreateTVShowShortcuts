using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Logger;

internal sealed class DebugLogger : ILogger
{
    public void Dispose()
    {
    }

    public string ReadLine()
    {
        throw new System.NotImplementedException();
    }

    public void WriteLine(string message, params object[] parameters)
        => Debug.WriteLine(string.Format(message, parameters));

    public void WriteLine(string message, bool suppressFreeLine, params object[] parameters)
        => Debug.WriteLine(string.Format(message, parameters));

    public void WriteLineForInput(string message, params object[] parameters)
        => Debug.WriteLine(string.Format(message, parameters));
}
