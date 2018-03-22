using CheckersMinimax.Pieces;
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
            currentMove = null;
            //winner = null;
            //turn = "Black";
            MakeBoard();
        }

        private void ClearBoard()
        {
            List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();
            lst.ItemsSource = boardArray;
        }

        private void MakeBoard()
        {
            int count = 0;
            for (int Row = 0; Row < 8; Row++)
            {
                boardArray.Add(new List<CheckersSquareUserControl>());
                for (int Column = 0; Column < 8; Column++)
                {
                    CheckersSquareUserControl checkerSquareUC;
                    if (Row % 2 == 0)
                    {
                        if (Column % 2 == 0)
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, Row, Column, CheckerPieceType.nullp);
                        }
                        else
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.BlackPawn);
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.RedPawn);
                            }
                            else
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.nullp);
                            }
                        }
                    }
                    else
                    {
                        if (Column % 2 == 0)
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.BlackPawn);
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.RedPawn);
                            }
                            else
                            {
                                //empty middle spot
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, Row, Column, CheckerPieceType.nullp);
                            }
                        }
                        else
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, Row, Column, CheckerPieceType.nullp);
                        }
                            
                    }
                    count++;
                    boardArray[Row].Add(checkerSquareUC);
                }
            }
            lst.ItemsSource = boardArray;
            //MakeButtons();
        }

        

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
