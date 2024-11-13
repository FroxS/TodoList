using System.Collections;
using System.Globalization;
using System.Windows.Data;
using TodoList.Core.Models;

namespace TodoList.Helpers;

public class TaskGroupComplited : PropertyGroupDescription
{
    public TaskGroupComplited()
    {
        CustomSort = new TaskGroupComparer();
    }

    public override object GroupNameFromItem(object item, int level, CultureInfo culture)
    {
        if (item is TaskItem task)
        {
            if (task.IsCompleted) return Properties.Resources.Completed;
            
        }
        return Properties.Resources.NotCompleted;
    }

    public override bool NamesMatch(object groupName, object itemName)
    {
        return base.NamesMatch(groupName, itemName);
    }
}

public class TaskGroupComparer : IComparer
{
    public int Compare(object x, object y)
    {
        if (x is CollectionViewGroup groupX && y is CollectionViewGroup groupY)
        {
            if (groupX.Name.ToString() == Properties.Resources.NotCompleted) return -1;
            if (groupY.Name.ToString() == Properties.Resources.NotCompleted) return 1;
        }

        return 0;
    }
}

