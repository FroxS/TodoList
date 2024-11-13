using System.Diagnostics;
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
        string assemblyLocation = AppContext.BaseDirectory;// Assembly.GetExecutingAssembly().Location;
        var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
        return new Version(version);
    }

    #endregion
}
