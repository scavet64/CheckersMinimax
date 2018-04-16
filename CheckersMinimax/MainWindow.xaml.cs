﻿using CheckersMinimax.AI;
using CheckersMinimax.Genetic;
using CheckersMinimax.Pieces;
using CheckersMinimax.Properties;
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
using System.Windows.Threading;

namespace CheckersMinimax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Settings Settings = Settings.Default;
        private static readonly SimpleLogger Logger = SimpleLogger.GetSimpleLogger();

        private static readonly int maxTurns = 500;

        private CheckersMove currentMove;
        private CheckerBoard checkerBoard;

        private Thread aiThread;

        private List<CheckersMove> currentAvailableMoves;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCheckers();

            if (Settings.RunningGeneticAlgo)
            {
                aiThread = new Thread(new ThreadStart(GeneticAlgoLoop));
                aiThread.SetApartmentState(ApartmentState.STA);
                aiThread.Start();
            }
            else if (Settings.IsAIDuel)
            {
                aiThread = new Thread(new ThreadStart(RunAIGame));
                aiThread.SetApartmentState(ApartmentState.STA);
                aiThread.Start();
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread myNewThread = new Thread(() => ButtonClickWork(sender));
            myNewThread.SetApartmentState(ApartmentState.STA);
            myNewThread.Start();
        }

        private void InitializeCheckers()
        {
            this.Dispatcher.Invoke(() =>
            {
                checkerBoard = new CheckerBoard();
                checkerBoard.MakeBoard(new RoutedEventHandler(Button_Click));

                lst.ItemsSource = checkerBoard.BoardArray;

                currentMove = null;
                SetTitle(string.Format("Checkers! {0}'s turn!", checkerBoard.CurrentPlayerTurn));

                DisableAllButtons();
                EnableButtonsWithMove();
            });
        }

        private void GeneticAlgoLoop()
        {
            int roundNumber = 0;
            while (roundNumber < Settings.NumberOfRounds)
            {
                DoGeneticAlgo();
                Logger.Info("Finished Round Number: " + roundNumber++);
            }
        }

        private void DoGeneticAlgo()
        {
            GeneticProgress currentProgress = GeneticProgress.GetGeneticProgressInstance();
            WinningGenome winningGenome = WinningGenome.GetWinningGenomeInstance();
            RandomGenome randomGenome = RandomGenome.GetRandomGenomeInstance();

            while (currentProgress.NumberOfGames < Settings.NumberOfSimulations)
            {
                try
                {
                    RunAIGame();
                }
                catch (AIException ex)
                {
                    //Dont count this game
                    Logger.Error("AI Exception was caught: " + ex.Message);
                    //reset game
                    InitializeCheckers();
                    continue;
                }
                currentProgress.NumberOfGames++;
                object winner = checkerBoard.GetWinner();
                if (winner != null && winner is PlayerColor winningPlayer && winningPlayer == PlayerColor.Red)
                {
                    currentProgress.NumberOfRandomGenomeWins++;
                }

                Logger.Info("AI Game Finished, Winner was " + winner);
                Logger.Info(string.Format(
                    "Current Stats -- NumberOfGamesPlayed: {0}, NumberOfRandomGenomeWins {1}",
                    currentProgress.NumberOfGames,
                    currentProgress.NumberOfRandomGenomeWins));

                //reset game
                InitializeCheckers();
            }

            //Simulations on this genetic variation finished. If more losses than wins, the new random genome was better
            if (currentProgress.NumberOfRandomGenomeWins > currentProgress.NumberOfGames / 2)
            {
                //Random Genome won more than half the games. This means we have a new winner
                winningGenome.SetNewWinningGenome(randomGenome);
                randomGenome.MutateGenomeAndSave();
            }

            //reset progress and game
            currentProgress.ResetValues();
        }

        private void RunAIGame()
        {
            int numberOfTurns = 0;
            while (checkerBoard.GetWinner() == null)
            {
                //AI vs AI
                CheckersMove aiMove = AIController.MinimaxStart(checkerBoard);
                if (aiMove != null && numberOfTurns++ < maxTurns)
                {
                    while (aiMove != null)
                    {
                        MakeMove(aiMove);
                        aiMove = aiMove.NextMove;
                        Thread.Sleep(Settings.TimeToSleeepBetweenMoves);
                    }
                }
                else
                {
                    //AI could not find a valid move. Is the game over? are we in a dead lock?
                    //Show error to user?
                    throw new AIException("The AI could not find any valid moves or the AI are just going back and forth, This must have ended in a tie");
                }
            }
        }

        private void ButtonClickWork(object sender)
        {
            Button button = (Button)sender;
            CheckersSquareUserControl checkerSquareUC = (CheckersSquareUserControl)((Grid)button.Parent).Parent;
            Logger.Info("Row: " + checkerSquareUC.CheckersPoint.Row + " Column: " + checkerSquareUC.CheckersPoint.Column);
            DisableAllButtons();
            if (currentMove == null)
            {
                currentMove = new CheckersMove();
            }

            if (currentMove.SourcePoint == null)
            {
                currentMove.SourcePoint = checkerSquareUC.CheckersPoint;
                SetBackgroundColor(checkerSquareUC, Brushes.Green);

                //starting a move, enable spaces where a valid move is present
                currentAvailableMoves = checkerSquareUC.CheckersPoint.GetPossibleMoves(checkerBoard);

                //Add self move to act as cancel
                currentAvailableMoves.Add(new CheckersMove(checkerSquareUC.CheckersPoint, checkerSquareUC.CheckersPoint));

                ColorBackgroundOfPoints(currentAvailableMoves, Brushes.Aqua);

                EnableButtonsWithPossibleMove(currentAvailableMoves);
            }
            else
            {
                currentMove.DestinationPoint = checkerSquareUC.CheckersPoint;
                SetBackgroundColor(checkerSquareUC, Brushes.Green);

                //get move from the list that has this point as its destination
                MakeMoveReturnModel returnModel = MakeMove(GetMoveFromList(checkerSquareUC.CheckersPoint));
                if (returnModel.WasMoveMade && returnModel.IsTurnOver && Settings.IsAIGame)
                {
                    //Disable buttons so the user cant click anything while the AI is thinking
                    DisableAllButtons();

                    //AI needs to make a move now
                    CheckersMove aiMove = AIController.MinimaxStart(checkerBoard);
                    if (aiMove != null)
                    {
                        while (aiMove != null)
                        {
                            MakeMove(aiMove);
                            aiMove = aiMove.NextMove;
                            Thread.Sleep(Settings.TimeToSleeepBetweenMoves);
                        }
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
            foreach (CheckersMove move in currentAvailableMoves)
            {
                if (move.DestinationPoint.Equals(checkersPoint))
                {
                    return move;
                }
            }

            return null;
        }

        private MakeMoveReturnModel MakeMove(CheckersMove moveToMake)
        {
            bool moveWasMade = false;
            bool isTurnOver = false;
            CheckersPoint source = moveToMake.SourcePoint;
            CheckersPoint destination = moveToMake.DestinationPoint;

            Logger.Info("Piece1 " + source.Row + ", " + source.Column);
            Logger.Info("Piece2 " + destination.Row + ", " + destination.Column);

            //was this a cancel?
            if (source != destination)
            {
                isTurnOver = checkerBoard.MakeMoveOnBoard(moveToMake);

                CheckersSquareUserControl sourceUC = checkerBoard.BoardArray[source.Row][source.Column];
                CheckersSquareUserControl destUC = checkerBoard.BoardArray[destination.Row][destination.Column];

                //Use dispatcher to run the Update method on the UI thread
                sourceUC.UpdateSquare();
                destUC.UpdateSquare();

                moveWasMade = true;

                //check for a winner
                object winner = checkerBoard.GetWinner();
                if (winner != null && winner is PlayerColor winnerColor && !Settings.RunningGeneticAlgo)
                {
                    MessageBox.Show("Winner Winner Chicken Dinner: " + winnerColor);
                }
            }

            ColorBackgroundOfPoints(currentAvailableMoves, Brushes.Black);
            SetTitle(string.Format("Checkers! {0}'s turn!", checkerBoard.CurrentPlayerTurn));
            EnableButtonsWithMove();
            currentMove = null;

            return new MakeMoveReturnModel()
            {
                IsTurnOver = isTurnOver,
                WasMoveMade = moveWasMade
            };
        }

        private void SetBackgroundColor(UserControl control, Brush colorToSet)
        {
            Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => control.Background = colorToSet));
        }

        private void SetTitle(string titleToSet)
        {
            Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => this.Title = titleToSet));
        }

        private void EnableButtonsWithMove()
        {
            List<CheckersMove> totalPossibleMoves = checkerBoard.GetMovesForPlayer();

            foreach (CheckersMove move in totalPossibleMoves)
            {
                CheckersPoint source = move.SourcePoint;
                int col = source.Column;
                int row = source.Row;

                CheckersSquareUserControl sourceUserControl = checkerBoard.BoardArray[row][col];

                Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => sourceUserControl.button.IsEnabled = true));
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
                    Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => squareUC.button.IsEnabled = false));
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
                    Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => checkerBoard.BoardArray[checkerPoint.DestinationPoint.Row][checkerPoint.DestinationPoint.Column].Background = backgroundColor));
                }
            }
        }

        private void EnableButtonsWithPossibleMove(List<CheckersMove> list)
        {
            foreach (CheckersMove checkerPoint in list)
            {
                Application.Current.Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(() => checkerBoard.BoardArray[checkerPoint.DestinationPoint.Row][checkerPoint.DestinationPoint.Column].button.IsEnabled = true));
            }
        }

        private void UpdateSquares()
        {
            foreach (List<CheckersSquareUserControl> list in checkerBoard.BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    squareUC.UpdateSquare();
                }
            }
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            if (aiThread != null)
            {
                aiThread.Abort();
            }

            InitializeCheckers();
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
