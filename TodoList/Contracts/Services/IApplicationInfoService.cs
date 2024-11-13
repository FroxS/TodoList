namespace TodoList.Contracts.Services;

public interface IApplicationInfoService
{
    Version GetVersion();
    string GetExePath();
}
