using CheckersMinimax.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public static class AIController
    {
        private static readonly Settings Settings = Settings.Default;
        private static readonly SimpleLogger Logger = SimpleLogger.GetSimpleLogger();
        private static readonly Random Rng = new Random();

        private static int counter = 0;
        private static bool thinking;

        /// <summary>
        /// Return true of false depending on if the AI is currently working
        /// </summary>
        /// <returns>true if the AI is still thinking</returns>
        public static bool IsThinking()
        {
            return thinking;
        }

        /// <summary>
        /// Initial minimax starting method. This method kicks off the algoritm and finds the best move for the current player.
        /// If two or more moves have the same value, the best move is choosen randomly from the moves
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>Best move for the current player</returns>
        public static CheckersMove MinimaxStart(CheckerBoard board)
        {
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            thinking = true;

            List<CheckersMove> possibleMoves = board.GetMovesForPlayer();
            List<int> values = new List<int>();

            Logger.Info(string.Format("Max is {0}", board.CurrentPlayerTurn));

            if (possibleMoves.IsNullOrEmpty())
            {
                return null;
            }

            foreach (CheckersMove move in possibleMoves)
            {
                CheckersMove moveToMake = move;
                CheckerBoard boardToMakeMoveOn = board;
                do
                {
                    Logger.Debug("Board Before");
                    Logger.Debug(boardToMakeMoveOn.ToString());

                    boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                    boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                    moveToMake = moveToMake.NextMove;

                    Logger.Debug("Board After");
                    Logger.Debug(boardToMakeMoveOn.ToString());
                }
                while (moveToMake != null);

                values.Add(Minimax(boardToMakeMoveOn, Settings.AIDepth - 1, alpha, beta, false, board.CurrentPlayerTurn));
            }

            int maxHeuristics = int.MinValue;
            foreach (int value in values)
            {
                if (value >= maxHeuristics)
                {
                    maxHeuristics = value;
                }
            }

            //filter the list of moves based on max value
            List<CheckersMove> bestMoves = new List<CheckersMove>();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == maxHeuristics)
                {
                    bestMoves.Add(possibleMoves[i]);
                }
            }

            counter = 0;
            thinking = false;
            Logger.Info("Node Values: " + string.Join(",", values.Select(x => x.ToString()).ToArray()));
            return bestMoves[Rng.Next(bestMoves.Count)];
        }

        /// <summary>
        /// Runs the minimax algoritm with alpha beta pruning
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <param name="isMax">if set to <c>true</c> [is maximum].</param>
        /// <param name="rootPlayer">The root player.</param>
        /// <returns>Minimax value for this board state</returns>
        private static int Minimax(CheckerBoard board, int depth, int alpha, int beta, bool isMax, PlayerColor rootPlayer)
        {
            List<CheckersMove> possibleMoves = board.GetMovesForPlayer();

            if (depth == 0 || possibleMoves.Count == 0)
            {
                return Score(board, rootPlayer);
            }

            int value = 0;
            if (isMax)
            {
                value = int.MinValue;
                foreach (CheckersMove move in possibleMoves)
                {
                    CheckersMove moveToMake = move;
                    CheckerBoard boardToMakeMoveOn = board;
                    do
                    {
                        boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                        boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                        moveToMake = moveToMake.NextMove;
                    }
                    while (moveToMake != null);
                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, false, rootPlayer);

                    value = Math.Max(result, value);
                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        Logger.Debug("Branch was pruned");
                        break;
                    }
                }
            }
            else
            {
                value = int.MaxValue;
                foreach (CheckersMove move in possibleMoves)
                {
                    CheckersMove moveToMake = move;
                    CheckerBoard boardToMakeMoveOn = board;
                    do
                    {
                        boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                        boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                        moveToMake = moveToMake.NextMove;
                    }
                    while (moveToMake != null);

                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, true, rootPlayer);

                    value = Math.Min(result, value);
                    beta = Math.Min(alpha, value);

                    if (alpha >= beta)
                    {
                        Logger.Debug("Branch was pruned");
                        break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Score the passed in board. This method will change the heuristics to evaluate depending on the difficulty in the settings
        /// </summary>
        /// <param name="board">Board to score</param>
        /// <param name="rootPlayer">The root minimax player</param>
        /// <returns>Score for the board</returns>
        private static int Score(CheckerBoard board, PlayerColor rootPlayer)
        {
            int score = board.ScoreWin(rootPlayer);

            if (score == 0)
            {
                switch (Settings.Difficulty.ToLower())
                {
                    case "hard":
                        score += board.ScoreC(rootPlayer);
                        goto case "medium";
                    case "medium":
                        score += board.ScoreB(rootPlayer);
                        goto case "easy";
                    case "easy":
                        score += board.ScoreA(rootPlayer);
                        score += board.ScoreWin(rootPlayer);
                        break;
                    default:
                        //By default just set the difficulty to hard
                        goto case "hard";
                }
            }

            if (counter++ % 10000 == 0)
            {
                Logger.Info(string.Format("Evaluating Node number: {0} - Score: {1}", counter, score));
            }

            Logger.Debug(board.ToString());

            return score;
        }
    }
}
