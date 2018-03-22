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

        BitmapImage BLACK_CHECKER = new BitmapImage(new Uri("Resources/black60p.png", UriKind.Relative));
        BitmapImage BLACK_KING_CHECKER = new BitmapImage(new Uri("Resources/black60p_king.png", UriKind.Relative));
        BitmapImage RED_CHECKER = new BitmapImage(new Uri("Resources/red60p.png", UriKind.Relative));
        BitmapImage RED_KING_CHECKER = new BitmapImage(new Uri("Resources/red60p_king.png", UriKind.Relative));

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


        public CheckersSquareUserControl(Brush backgroundColor, int row, int col)
        {
            InitializeComponent();
            //BackgroundColor = backgroundColor;
            this.Background = backgroundColor;
            button.Content = "row: " + row + " , col: " + col;
        }

        public void ShowRed()
        {
            checkerImage.Source = RED_CHECKER;
        }

        public void ShowBlack()
        {
            checkerImage.Source = BLACK_CHECKER;
        }

        public void ShowBlackKing()
        {
            checkerImage.Source = BLACK_KING_CHECKER;
        }

        public void ShowRedKing()
        {
            checkerImage.Source = RED_KING_CHECKER;
        }

        public void hideChecker()
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
