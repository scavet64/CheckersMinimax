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
        private PlayerColor currentPlayersTurn = PlayerColor.Red;

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
            DisableAllButtons();
            if (currentMove == null)
            {
                currentMove = new CheckersMove();
            }
            if (currentMove.SourcePoint == null)
            {
                currentMove.SourcePoint = checkerSquareUC.CheckersPoint;
                checkerSquareUC.Background = Brushes.Green;

                //starting a move, enable spaces where a valid move is present
                CurrentAvailableMoves = checkerSquareUC.CheckersPoint.GetPotentialPointsForMove(checkerBoard);

                //Add self move to act as cancel
                CurrentAvailableMoves.Add(new CheckersMove(checkerSquareUC.CheckersPoint, checkerSquareUC.CheckersPoint));

                ColorBackgroundOfPoints(CurrentAvailableMoves, Brushes.Aqua);
                
                EnableButtonsWithPossibleMove(CurrentAvailableMoves);
            }
            else
            {
                currentMove.DestinationPoint = checkerSquareUC.CheckersPoint;
                checkerSquareUC.Background = Brushes.Green;

                //get move from the list that has this point as its destination
                MakeMove(GetMoveFromList(checkerSquareUC.CheckersPoint));

            }
            //if ((currentMove.SourcePoint != null) && (currentMove.DestinationPoint != null))
            //{
            //    MakeMove(currentMove);
            //    //if (CheckMove())
            //    //{
            //    //    MakeMove();
            //    //    //aiMakeMove();
            //    //}
            //}
        }

        private CheckersMove GetMoveFromList(CheckersPoint checkersPoint)
        {
            foreach(CheckersMove move in CurrentAvailableMoves)
            {
                if (move.DestinationPoint.Equals(checkersPoint))
                {
                    return move;
                }
            }
            return null;
        }

        private void MakeMove(CheckersMove moveToMake)
        {
            CheckersPoint source = moveToMake.SourcePoint;
            CheckersPoint destination = moveToMake.DestinationPoint;

            Console.WriteLine("Piece1 " + source.Row + ", " + source.Column);
            Console.WriteLine("Piece2 " + destination.Row + ", " + destination.Column);

            //was this a cancel?
            if (source != destination)
            {
                destination.Checker = source.Checker;
                source.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);

                //was this a jump move?
                CheckersPoint jumpedPoint = moveToMake.JumpedPoint;
                if (jumpedPoint != null)
                {
                    //delete the checker piece that was jumped
                    CheckersSquareUserControl jumpedSquareUserControl = checkerBoard.BoardArray[jumpedPoint.Row][jumpedPoint.Column];
                    jumpedSquareUserControl.CheckersPoint.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);
                    jumpedSquareUserControl.UpdateSquare();
                }

                //Check for win

                //Is this piece a king now?
                if(!(destination.Checker is KingCheckerPiece))
                {
                    if (destination.Row == 7 || destination.Row == 0)
                    {
                        //Should be a king now
                        if(destination.Checker is IRedPiece)
                        {
                            destination.Checker = new RedKingCheckerPiece();
                        }
                        else
                        {
                            destination.Checker = new BlackKingCheckerPiece();
                        }
                        
                    }
                }

                CheckersSquareUserControl sourceUC = checkerBoard.BoardArray[source.Row][source.Column];
                CheckersSquareUserControl destUC = checkerBoard.BoardArray[destination.Row][destination.Column];
                sourceUC.CheckersPoint = source;
                destUC.CheckersPoint = destination;
                sourceUC.UpdateSquare();
                destUC.UpdateSquare();

                //swap player turn
                SwapTurns();
            }

            ColorBackgroundOfPoints(CurrentAvailableMoves, Brushes.Black);
            if(currentPlayersTurn == PlayerColor.Red)
            {
                EnableButtons<IRedPiece>();
            }
            else
            {
                EnableButtons<IBlackPiece>();
            }
            //EnableAllButtons();
            currentMove = null;
        }

        private void SwapTurns()
        {
            if (currentPlayersTurn == PlayerColor.Red)
            {
                currentPlayersTurn = PlayerColor.Black;
            }
            else
            {
                currentPlayersTurn = PlayerColor.Red;
            }
        }

        private void EnableButtons<T>()
        {
            foreach (List<CheckersSquareUserControl> list in checkerBoard.BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    if (squareUC.CheckersPoint.Checker != null && squareUC.CheckersPoint.Checker is T)
                    {
                        squareUC.button.IsEnabled = true;
                    }
                }
            }
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

        private void EnableButtonsWithPossibleMove(List<CheckersMove> list)
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
