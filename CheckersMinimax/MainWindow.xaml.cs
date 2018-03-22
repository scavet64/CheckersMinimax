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
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp), new RoutedEventHandler(Button_Click));
                        }
                        else
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn), new RoutedEventHandler(Button_Click));
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn), new RoutedEventHandler(Button_Click));
                            }
                            else
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp), new RoutedEventHandler(Button_Click));
                            }
                        }
                    }
                    else
                    {
                        if (Column % 2 == 0)
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn), new RoutedEventHandler(Button_Click));
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn), new RoutedEventHandler(Button_Click));
                            }
                            else
                            {
                                //empty middle spot
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp), new RoutedEventHandler(Button_Click));
                            }
                        }
                        else
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp), new RoutedEventHandler(Button_Click));
                        }

                    }
                    count++;
                    boardArray[Row].Add(checkerSquareUC);
                }
            }
            lst.ItemsSource = boardArray;
        }

        public void Button_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            CheckersSquareUserControl checkerSquareUC = (CheckersSquareUserControl)((Grid)button.Parent).Parent;
            Console.WriteLine("Row: " + checkerSquareUC.CheckersPoint.Row + " Column: " + checkerSquareUC.CheckersPoint.Column);
            if (currentMove == null)
            {
                currentMove = new CheckersMove();
            }
            if (currentMove.Source == null)
            {
                currentMove.Source = checkerSquareUC;
                checkerSquareUC.Background = Brushes.Green;
            }
            else
            {
                currentMove.Destination = checkerSquareUC;
                checkerSquareUC.Background = Brushes.Green;
            }
            if ((currentMove.Source != null) && (currentMove.Destination != null))
            {
                MakeMove();
                //if (CheckMove())
                //{
                //    MakeMove();
                //    //aiMakeMove();
                //}
            }
        }

        private void MakeMove()
        {
            CheckersSquareUserControl source = currentMove.Source;
            CheckersSquareUserControl destination = currentMove.Destination;

            Console.WriteLine("Piece1 " + source.CheckersPoint.Row + ", " + source.CheckersPoint.Column);
            Console.WriteLine("Piece2 " + destination.CheckersPoint.Row + ", " + destination.CheckersPoint.Column);

            destination.CheckersPoint.Checker = currentMove.Source.CheckersPoint.Checker;
            source.CheckersPoint.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullp);

            source.UpdateSquare();
            destination.UpdateSquare();

            source.Background = Brushes.Black;
            destination.Background = Brushes.Black;

            currentMove = null;
        }

        private void UpdateSquares()
        {
            foreach(List<CheckersSquareUserControl> list in boardArray)
            {
                foreach(CheckersSquareUserControl squareUC in list)
                {
                    squareUC.UpdateSquare();
                }
            }
        }
    }
}
