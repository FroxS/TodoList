using System.Diagnostics;
using System.IO;
using System.Reflection;

using TodoList.Contracts.Services;

namespace TodoList.Services;

public class ApplicationInfoService : IApplicationInfoService
{
    #region Constructor
    public ApplicationInfoService()
    {
    }

    #endregion

    #region Method

    public Version GetVersion()
    {
        string assemblyLocation = GetExePath();
        var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
        return new Version(version);
    }

    public string GetExePath()
    {
        string assemblyLocation = Path.Combine(AppContext.BaseDirectory, System.AppDomain.CurrentDomain.FriendlyName + ".exe");
        return assemblyLocation;
    }

    #endregion
}
