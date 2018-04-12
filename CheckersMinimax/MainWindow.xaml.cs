using CheckersMinimax.AI;
using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            DisableAllButtons();
            EnableButtonsWithMove();

            //Thread aiThread = new Thread(new ThreadStart(runAIGame));
            //aiThread.SetApartmentState(ApartmentState.STA);
            //aiThread.Start();
        }

        private void runAIGame()
        {
            while (checkerBoard.GetWinner() == null)
            {
                //AI vs AI
                AIController AI = new AIController(checkerBoard);
                CheckersMove aiMove = AI.MinimaxStart(checkerBoard, 20, true);
                if (aiMove != null)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate {

                        MakeMove(aiMove);

                    });
                    
                }
                else
                {
                    //AI could not find a valid move. Is the game over? are we in a dead lock?
                    //Show error to user
                }
                Thread.Sleep(500);
            }
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
                CurrentAvailableMoves = checkerSquareUC.CheckersPoint.GetPossibleMoves(checkerBoard);

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
                bool wasMoveMade = MakeMove(GetMoveFromList(checkerSquareUC.CheckersPoint));
                if (wasMoveMade)
                {
                    //AI needs to make a move now
                    AIController AI = new AIController(checkerBoard);
                    CheckersMove aiMove = AI.MinimaxStart(checkerBoard, 3, true);
                    if (aiMove != null)
                    {
                        MakeMove(aiMove);
                    }
                    else
                    {
                        //AI could not find a valid move. Is the game over? are we in a dead lock?
                        //Show error to user
                    }

                }
            }
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

        private bool MakeMove(CheckersMove moveToMake)
        {
            bool moveWasMade = false;
            CheckersPoint source = moveToMake.SourcePoint;
            CheckersPoint destination = moveToMake.DestinationPoint;

            Console.WriteLine("Piece1 " + source.Row + ", " + source.Column);
            Console.WriteLine("Piece2 " + destination.Row + ", " + destination.Column);

            //was this a cancel?
            if (source != destination)
            {
                checkerBoard.MakeMoveOnBoard(moveToMake);

                CheckersSquareUserControl sourceUC = checkerBoard.BoardArray[source.Row][source.Column];
                CheckersSquareUserControl destUC = checkerBoard.BoardArray[destination.Row][destination.Column];
                sourceUC.CheckersPoint = source;
                destUC.CheckersPoint = destination;
                sourceUC.UpdateSquare();
                destUC.UpdateSquare();
                moveWasMade = true;

                //check for a winner
                object winner = checkerBoard.GetWinner();
                if(winner != null)
                {
                    if(winner is PlayerColor winnerColor)
                    {
                        MessageBox.Show("Winner Winner Chicken Dinner: " + winnerColor);
                    }
                }
            }

            ColorBackgroundOfPoints(CurrentAvailableMoves, Brushes.Black);

            EnableButtonsWithMove();
            currentMove = null;
            return moveWasMade;
        }

        private void EnableButtonsWithMove()
        {
            List<CheckersMove> totalPossibleMoves = checkerBoard.GetMovesForPlayer(checkerBoard.CurrentPlayerTurn);

            foreach (CheckersMove move in totalPossibleMoves)
            {
                CheckersPoint source = move.SourcePoint;
                int col = source.Column;
                int row = source.Row;

                CheckersSquareUserControl sourceUserControl = checkerBoard.BoardArray[row][col];
                sourceUserControl.button.IsEnabled = true;
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
            if (list != null)
            {
                foreach (CheckersMove checkerPoint in list)
                {
                    checkerBoard.BoardArray[checkerPoint.DestinationPoint.Row][checkerPoint.DestinationPoint.Column].Background = backgroundColor;
                }
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
