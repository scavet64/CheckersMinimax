using CheckersMinimax.Clone;
using CheckersMinimax.Pieces;
using CheckersMinimax.Properties;
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
        private static readonly Settings Settings = Settings.Default;

        /// <summary>
        /// Gets or sets the checkers point.
        /// </summary>
        /// <value>
        /// The checkers point.
        /// </value>
        public CheckersPoint CheckersPoint { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
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

        private Brush background;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersSquareUserControl"/> class.
        /// </summary>
        public CheckersSquareUserControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersSquareUserControl"/> class.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="checkersPoint">The checkers point.</param>
        /// <param name="routedEventHandler">The routed event handler.</param>
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

        /// <summary>
        /// Updates the square.
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance has checker.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has checker; otherwise, <c>false</c>.
        /// </returns>
        public bool HasChecker()
        {
            return CheckersPoint.Checker != null && !(CheckersPoint.Checker is NullCheckerPiece);
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>
        /// A clone to be used in the minimax algoritm
        /// </returns>
        public object GetMinimaxClone()
        {
            CheckersSquareUserControl clone = new CheckersSquareUserControl();
            clone.CheckersPoint = (CheckersPoint)this.CheckersPoint.GetMinimaxClone();

            return clone;
        }

        #region INotifiedProperty Block        
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed]. Used in data binding
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        /// Hides the checker.
        /// </summary>
        private void HideChecker()
        {
            checkerImage.Visibility = Visibility.Collapsed;
        }
    }
}
