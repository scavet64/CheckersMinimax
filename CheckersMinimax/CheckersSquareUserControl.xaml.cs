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
        private CheckerPiece checker;

        public CheckerPiece Checker
        {
            get { return checker; }
            set { checker = value; }
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

        /// <summary>
        /// Probably wont need this
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="checkerPieceType"></param>
        //public CheckersSquareUserControl(Brush backgroundColor, int row, int col, CheckerPiece checker)
        //{
        //    InitializeComponent();
        //    //BackgroundColor = backgroundColor;
        //    this.Background = backgroundColor;
        //    button.Content = "row: " + row + " , col: " + col;
        //    this.checker = checker;
        //    UpdateSquare();
        //}

        public CheckersSquareUserControl(Brush backgroundColor, int row, int col, CheckerPieceType checkerPieceType)
        {
            InitializeComponent();
            //BackgroundColor = backgroundColor;
            this.Background = backgroundColor;
            this.checker = CheckerPieceFactory.GetCheckerPiece(checkerPieceType);
            UpdateSquare();

            //Debug TODO: Delete this when not needed anymore
            button.Content = "row: " + row + " , col: " + col;
        }

        public void UpdateSquare()
        {
            if(checker != null)
            {
                try
                {
                    checkerImage.Source = BuildCheckerImageSource(checker.ImageSource);
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

        private ImageSource BuildCheckerImageSource(string imageSource)
        {
            return new BitmapImage(new Uri(imageSource, UriKind.Relative));
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
