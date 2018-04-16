using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckersMinimax
{
    /// <summary>
    /// Interaction logic for LogViewerUserControl.xaml
    /// </summary>
    public partial class LogViewerUserControl : UserControl, INotifyPropertyChanged
    {
        private static LogViewerUserControl instance;

        private ObservableCollection<string> logList = new ObservableCollection<string>();

        public ObservableCollection<string> LogList
        {
            get { return logList; }
            set
            {
                logList = value;
                if (PropertyChanged != null)
                {
                    OnPropertyChanged("LogList");
                }
            }
        }

        public LogViewerUserControl()
        {
            InitializeComponent();
            DataContext = this;
            //ListView.Items.Add("testing");
            ListView.ItemsSource = LogList;
            //LogList.Add("testing");
        }

        public void AddToList(string message)
        {
            //LogList.Add(message);
            OnPropertyChanged("LogList");
            this.Dispatcher.Invoke(() =>
            {
                LogList.Add(message);
                //ListView.Items.Add(message);
            });
        }

        public static LogViewerUserControl GetInstance()
        {
            if (instance == null)
            {
                instance = new LogViewerUserControl();
            }

            //instance.OnPropertyChanged("LogList");
            return instance;
        }

        #region INotifiedProperty Block
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
