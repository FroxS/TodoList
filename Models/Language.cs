using System.Globalization;

namespace TodoList.Models;

public class Languages : List<Lang>
{
    #region  properties

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public Languages(){
        Add(new Lang("pl-PL", "Polski"));
        Add(new Lang("", "English"));
    }

    #endregion

    public Lang GetDef()
    {
        return Find(x => x.Key == "");
    }

    public Lang Find(CultureInfo cult)
    {
        if(cult == null)
            return GetDef();
        else
        return Find(x => x.Key == cult.Name);
    }
}

public class Lang
{
    #region Public properties

    public string Key { get; set; }
    public string Name { get; set; }

    #endregion

    #region Constructors

    public Lang(string key, string name)
    {
        Key = key;
        Name = name;
    }

    #endregion
}



