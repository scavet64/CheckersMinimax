using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CheckersMove currentMove;
        private List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Checkers! Blacks turn!";
            //currentMove = null;
            //winner = null;
            //turn = "Black";
            MakeBoard();
        }

        private void ClearBoard()
        {
            List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();
            lst.ItemsSource = boardArray;

            //for (int r = 1; r < 9; r++)
            //{
            //    for (int c = 0; c < 8; c++)
            //    {
            //        StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
            //        //CheckersGrid.Children.Remove(stackPanel);
            //    }
            //}
        }

        private void MakeBoard()
        {
            int count = 0;
            for (int Row = 0; Row < 8; Row++)
            {
                boardArray.Add(new List<CheckersSquareUserControl>());
                for (int Column = 0; Column < 8; Column++)
                {
                    CheckersSquareUserControl stackPanel;
                    if (Row % 2 == 0)
                    {
                        if (Column % 2 == 0)
                        {
                            stackPanel = new CheckersSquareUserControl(Brushes.White, Row, Column);
                        }
                        else
                        {
                            stackPanel = new CheckersSquareUserControl(Brushes.Black, Row, Column);
                            if (Row < 3)
                            {
                                stackPanel.ShowBlack();
                            }
                            else if (Row > 4)
                            {
                                stackPanel.ShowRed();
                            }
                        }
                    }
                    else
                    {
                        if (Column % 2 == 0)
                        {
                            stackPanel = new CheckersSquareUserControl(Brushes.Black, Row, Column);
                            if (Row < 3)
                            {
                                stackPanel.ShowBlack();
                            }
                            else if (Row > 4)
                            {
                                stackPanel.ShowRed();
                            }
                        }
                        else
                        {
                            stackPanel = new CheckersSquareUserControl(Brushes.White, Row, Column);
                        }
                            
                    }
                    count++;
                    boardArray[Row].Add(stackPanel);
                    //CheckersGrid.Children.Add(stackPanel);
                }
            }
            lst.ItemsSource = boardArray;
            //MakeButtons();
        }

        //private void MakeButtons()
        //{
        //    for (int r = 1; r < 9; r++)
        //    {
        //        for (int c = 0; c < 8; c++)
        //        {
        //            StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
        //            Button button = new Button();
        //            button.Click += new RoutedEventHandler(button_Click);
        //            button.Height = 60;
        //            button.Width = 60;
        //            button.HorizontalAlignment = HorizontalAlignment.Center;
        //            button.VerticalAlignment = VerticalAlignment.Center;
        //            var redBrush = new ImageBrush();
        //            redBrush.ImageSource = new BitmapImage(new Uri("Resources/red60p.png", UriKind.Relative));
        //            var blackBrush = new ImageBrush();
        //            blackBrush.ImageSource = new BitmapImage(new Uri("Resources/black60p.png", UriKind.Relative));
        //            switch (r)
        //            {
        //                case 1:
        //                    if (c % 2 == 1)
        //                    {

        //                        button.Background = redBrush;
        //                        button.Name = "buttonRed" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 2:
        //                    if (c % 2 == 0)
        //                    {
        //                        button.Background = redBrush;
        //                        button.Name = "buttonRed" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 3:
        //                    if (c % 2 == 1)
        //                    {
        //                        button.Background = redBrush;
        //                        button.Name = "buttonRed" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 4:
        //                    if (c % 2 == 0)
        //                    {
        //                        button.Background = Brushes.Black;
        //                        button.Name = "button" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 5:
        //                    if (c % 2 == 1)
        //                    {
        //                        button.Background = Brushes.Black;
        //                        button.Name = "button" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 6:
        //                    if (c % 2 == 0)
        //                    {
        //                        button.Background = blackBrush;
        //                        button.Name = "buttonBlack" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 7:
        //                    if (c % 2 == 1)
        //                    {
        //                        button.Background = blackBrush;
        //                        button.Name = "buttonBlack" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                case 8:
        //                    if (c % 2 == 0)
        //                    {
        //                        button.Background = blackBrush;
        //                        button.Name = "buttonBlack" + r + c;
        //                        stackPanel.Children.Add(button);
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}

        UIElement GetGridElement(Grid g, int r, int c)
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                UIElement e = g.Children[i];
                if (Grid.GetRow(e) == r && Grid.GetColumn(e) == c)
                    return e;
            }
            return null;
        }

        //public void button_Click(Object sender, RoutedEventArgs e)
        //{
        //    Button button = (Button)sender;
        //    StackPanel stackPanel = (StackPanel)button.Parent;
        //    int row = Grid.GetRow(stackPanel);
        //    int col = Grid.GetColumn(stackPanel);
        //    Console.WriteLine("Row: " + row + " Column: " + col);
        //    if (currentMove == null)
        //        currentMove = new CheckersMove();
        //    if (currentMove.piece1 == null)
        //    {
        //        currentMove.piece1 = new CheckerPiece(new CheckersPoint(row, col));
        //        stackPanel.Background = Brushes.Green;
        //    }
        //    else
        //    {
        //        currentMove.piece2 = new CheckerPiece(row, col);
        //        stackPanel.Background = Brushes.Green;
        //    }
        //    if ((currentMove.piece1 != null) && (currentMove.piece2 != null))
        //    {
        //        if (CheckMove())
        //        {
        //            MakeMove();
        //            aiMakeMove();
        //        }
        //    }
        //}
    }
}
