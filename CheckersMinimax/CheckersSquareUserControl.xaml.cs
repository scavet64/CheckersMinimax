using CheckersMinimax.Clone;
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
using System.Windows.Threading;

namespace CheckersMinimax
{
    /// <summary>
    /// Interaction logic for CheckersSquareUserControl.xaml
    /// </summary>
    public partial class CheckersSquareUserControl : UserControl, INotifyPropertyChanged, IMinimaxClonable
    {
        public CheckersPoint CheckersPoint { get; set; }

        private Brush background;

        public Brush BackgroundColor
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public CheckersSquareUserControl()
        {
        }

        public CheckersSquareUserControl(Brush backgroundColor, CheckersPoint checkersPoint, RoutedEventHandler routedEventHandler)
        {
            InitializeComponent();
            this.Background = backgroundColor;
            this.CheckersPoint = checkersPoint;
            this.button.Click += routedEventHandler;

            UpdateSquare();

            //Debug TODO: Delete this when not needed anymore
            button.Content = "row: " + checkersPoint.Row + " , col: " + checkersPoint.Column;
        }

        public void UpdateSquare()
        {
            if (CheckersPoint != null && CheckersPoint.Checker != null)
            {
                try
                {
                    if (checkerImage != null)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            checkerImage.Source = CheckersPoint.Checker.BuildCheckerImageSource();
                        });
                    }
                }
                catch (Exception ex)
                {
                    //Show error message to user
                    MessageBox.Show(string.Format("Error Updating Square Row: Message = {0}", ex.Message));
                }
            }
            else
            {
                HideChecker();
            }
        }

        public bool HasChecker()
        {
            return (CheckersPoint.Checker != null && !(CheckersPoint.Checker is NullCheckerPiece));
        }

        public object GetMinimaxClone()
        {
            CheckersSquareUserControl clone = new CheckersSquareUserControl();
            clone.CheckersPoint = (CheckersPoint)this.CheckersPoint.GetMinimaxClone();

            return clone;
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

        private void HideChecker()
        {
            checkerImage.Visibility = Visibility.Collapsed;
        }
    }
}
