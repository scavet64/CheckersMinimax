using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CheckersSquareUserControl.xaml
    /// </summary>
    public partial class CheckersSquareUserControl : UserControl, INotifyPropertyChanged
    {

        private CheckersPoint checkersPoint;

        public CheckersPoint CheckersPoint
        {
            get { return checkersPoint; }
            set { checkersPoint = value; }
        }

        private Brush _background;
        public Brush BackgroundColor
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public CheckersSquareUserControl(Brush backgroundColor, CheckersPoint checkersPoint, RoutedEventHandler routedEventHandler)
        {
            InitializeComponent();
            //BackgroundColor = backgroundColor;
            this.Background = backgroundColor;
            this.checkersPoint = checkersPoint;
            this.button.Click += routedEventHandler;

            UpdateSquare();

            //Debug TODO: Delete this when not needed anymore
            button.Content = "row: " + checkersPoint.Row + " , col: " + checkersPoint.Column;
        }

        public void UpdateSquare()
        {
            if(checkersPoint != null && checkersPoint.Checker != null)
            {
                try
                {
                    checkerImage.Source = checkersPoint.Checker.BuildCheckerImageSource();
                }
                catch (Exception ex)
                {
                    //Show error message to user
                }
            }
            else
            {
                HideChecker();
            }
        }

        private void HideChecker()
        {
            checkerImage.Visibility = Visibility.Collapsed;
        }

        #region INotifiedProperty Block
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
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
