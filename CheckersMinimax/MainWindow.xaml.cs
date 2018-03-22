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
            MakeBoard();
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
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp));
                        }
                        else
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn));
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn));
                            }
                            else
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp));
                            }
                        }
                    }
                    else
                    {
                        if (Column % 2 == 0)
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn));
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn));
                            }
                            else
                            {
                                //empty middle spot
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp));
                            }
                        }
                        else
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp));
                        }

                    }
                    count++;
                    boardArray[Row].Add(checkerSquareUC);
                }
            }
            lst.ItemsSource = boardArray;
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
        //    CheckersSquareUserControl checkerSquareUC = (CheckersSquareUserControl)button.Parent;
        //    Console.WriteLine("Row: " + checkerSquareUC.Row + " Column: " + checkerSquareUC.Col);
        //    if (currentMove == null)
        //    {
        //        currentMove = new CheckersMove();
        //    }
        //    if (currentMove.PieceMoving == null)
        //    {
        //        currentMove.Source = checkerSquareUC.check
        //        currentMove.PieceMoving = checkerSquareUC.Checker;
        //        checkerSquareUC.Background = Brushes.Green;
        //    }
        //    else
        //    {
        //        currentMove.Destination = new CheckersPoint(checkerSquareUC.Row, checkerSquareUC.Col);
        //        checkerSquareUC.Background = Brushes.Green;
        //    }
        //    if ((currentMove.PieceMoving != null) && (currentMove.Destination != null))
        //    {
        //        MakeMove();
        //        //if (CheckMove())
        //        //{
        //        //    MakeMove();
        //        //    //aiMakeMove();
        //        //}
        //    }
        //}

        //private void MakeMove()
        //{
        //    Console.WriteLine("Piece1 " + currentMove + ", " + currentMove.piece1.Column);
        //    Console.WriteLine("Piece2 " + currentMove.piece2.Row + ", " + currentMove.piece2.Column);
        //    StackPanel stackPanel1 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece1.Row, currentMove.piece1.Column);
        //    StackPanel stackPanel2 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece2.Row, currentMove.piece2.Column);
        //    CheckersGrid.Children.Remove(stackPanel1);
        //    CheckersGrid.Children.Remove(stackPanel2);
        //    Grid.SetRow(stackPanel1, currentMove.piece2.Row);
        //    Grid.SetColumn(stackPanel1, currentMove.piece2.Column);
        //    CheckersGrid.Children.Add(stackPanel1);
        //    Grid.SetRow(stackPanel2, currentMove.piece1.Row);
        //    Grid.SetColumn(stackPanel2, currentMove.piece1.Column);
        //    CheckersGrid.Children.Add(stackPanel2);
        //    checkKing(currentMove.piece2);
        //    currentMove = null;
        //    if (turn == "Black")
        //    {
        //        this.Title = "Checkers! Reds turn!";
        //        turn = "Red";
        //    }
        //    else if (turn == "Red")
        //    {
        //        this.Title = "Checkers! Blacks turn!";
        //        turn = "Black";
        //    }
        //    checkWin();
        //}
    }
}
