#nullable enable
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoList.Core.Models
{
    
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public event PropertyChangingEventHandler PropertyChanging = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (names != null)
                names.ToList().ForEach((o) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(o)));
        }

        protected void OnPropertyChanging([CallerMemberName] string? name = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
    
}