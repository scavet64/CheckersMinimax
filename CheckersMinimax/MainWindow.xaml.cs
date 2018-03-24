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
        private CheckerBoard checkerBoard = new CheckerBoard();

        private List<CheckersMove> CurrentAvailableMoves;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Checkers! Blacks turn!";
            currentMove = null;
            //winner = null;
            //turn = "Black";
            checkerBoard.MakeBoard(new RoutedEventHandler(Button_Click));

            //todo try to use databinding here
            lst.ItemsSource = checkerBoard.BoardArray;
        }

        private void ClearBoard()
        {
            List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();
            checkerBoard.MakeBoard(new RoutedEventHandler(Button_Click));

            //todo try to use databinding here
            lst.ItemsSource = checkerBoard.BoardArray;
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

                //starting a move, enable spaces where a valid move is present
                CurrentAvailableMoves = checkerSquareUC.CheckersPoint.GetPotentialPointsForMove(checkerBoard);

                //Add self move to act as cancel
                CurrentAvailableMoves.Add(new CheckersMove(checkerSquareUC.CheckersPoint, checkerSquareUC.CheckersPoint, false));

                ColorBackgroundOfPoints(CurrentAvailableMoves, Brushes.Aqua);
                DisableAllButtons();
                EnableButtons(CurrentAvailableMoves);
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

            //was this a cancel?
            if (source != destination)
            {
                destination.CheckersPoint.Checker = currentMove.Source.CheckersPoint.Checker;
                source.CheckersPoint.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullp);

                //was this a jump move?
                if (currentMove.IsJumpMove)
                {
                    //delete the checker piece that was jumped
                }

                //Check for win

                source.UpdateSquare();
                destination.UpdateSquare();
            }

            ColorBackgroundOfPoints(CurrentAvailableMoves, Brushes.Black);
            EnableAllButtons();
            currentMove = null;
        }

        private void DisableAllButtons()
        {
            foreach (List<CheckersSquareUserControl> list in checkerBoard.BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    squareUC.button.IsEnabled = false;
                }
            }
        }

        private void EnableAllButtons()
        {
            foreach (List<CheckersSquareUserControl> list in checkerBoard.BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    squareUC.button.IsEnabled = true;
                }
            }
        }

        private void ColorBackgroundOfPoints(List<CheckersMove> list, Brush backgroundColor)
        {
            foreach (CheckersMove checkerPoint in list)
            {
                checkerBoard.BoardArray[checkerPoint.DestinationPoint.Row][checkerPoint.DestinationPoint.Column].Background = backgroundColor;
            }
        }

        private void EnableButtons(List<CheckersMove> list)
        {
            foreach (CheckersMove checkerPoint in list)
            {
                checkerBoard.BoardArray[checkerPoint.DestinationPoint.Row][checkerPoint.DestinationPoint.Column].button.IsEnabled = true;
            }
        }

        private void UpdateSquares()
        {
            foreach(List<CheckersSquareUserControl> list in checkerBoard.BoardArray)
            {
                foreach(CheckersSquareUserControl squareUC in list)
                {
                    squareUC.UpdateSquare();
                }
            }
        }
    }
}
