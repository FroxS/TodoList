using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TodoList.Contracts.Services;
using TodoList.Models;

namespace TodoList.Controls
{
    public class MessageControl : UserControl
    {
        #region Private properties

        private Grid _grid;
        private TextBlock _text;
        private DispatcherTimer timer;

        #endregion

        #region Public properties

        public IMessagesViewModel Message
        {
            get { return (IMessagesViewModel)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }


        /// <summary>
        /// In Second
        /// </summary>
        public int TimeToShow
        {
            get { return (int)GetValue(TimeToShowProperty); }
            set { SetValue(TimeToShowProperty, value); }
        }

        #endregion

        #region Dependecy

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register(nameof(Message), typeof(IMessagesViewModel), typeof(MessageControl), new PropertyMetadata(null, (e, s) => { if (e is MessageControl ctrl) ctrl.AttatchMessage(); }));

        public static readonly DependencyProperty TimeToShowProperty =
            DependencyProperty.Register(nameof(TimeToShow), typeof(int), typeof(MessageControl), new PropertyMetadata(5, (e, s) => { if (e is MessageControl ctrl) ctrl.AttatchMessage(); }));

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageControl()
        {
            Content = _grid = new Grid();
            _grid.Children.Add(_text = new TextBlock() { Margin = new Thickness(5) });
            this.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        private void AttatchMessage()
        {
            if(Message != null)
            {
                Message.OnSetMessage -= Message_OnSetMessage;
                Message.OnSetMessage += Message_OnSetMessage;
            }
            
        }

        private void Message_OnSetMessage(string message, Models.EMessageType type)
        {
            _text.Text = message;
            if (message == null)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }
            
            switch (type)
            {
                case EMessageType.Message:
                    _text.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    _grid.Background = new SolidColorBrush(Color.FromRgb(5, 133, 237));
                    break;
                case EMessageType.Error:
                    _text.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    _grid.Background = new SolidColorBrush(Color.FromRgb(189, 0, 0));
                    break;
                case EMessageType.Warning:
                    _text.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    _grid.Background = new SolidColorBrush(Color.FromRgb(230, 134, 0));
                    break;
            }

            this.Visibility = Visibility.Visible;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(TimeToShow);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Visibility = Visibility.Collapsed;
            timer.Tick -= Timer_Tick;
        }


        #endregion
    }
}