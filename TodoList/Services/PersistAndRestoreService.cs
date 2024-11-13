using System.Collections;
using System.IO;

using TodoList.Contracts.Services;
using TodoList.Core.Contracts;
using TodoList.Models;

namespace TodoList.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
    #region Fields

    private readonly IFileService _fileService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    #endregion

    #region Constructor
    public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
    {
        _fileService = fileService;
        _appConfig = appConfig;
    }

    #endregion

    #region Methods

    public void PersistData()
    {
        if (App.Current.Properties != null)
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            _fileService.Save(folderPath, fileName, App.Current.Properties);
        }
    }

    public void RestoreData()
    {
        var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        var fileName = _appConfig.AppPropertiesFileName;
        var properties = _fileService.Read<IDictionary>(folderPath, fileName);
        if (properties != null)
        {
            foreach (DictionaryEntry property in properties)
            {
                App.Current.Properties.Add(property.Key, property.Value);
            }
        }
    }

    #endregion
}
