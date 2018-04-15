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
        private static readonly Settings settings = Settings.Default;
        private static readonly SimpleLogger logger = SimpleLogger.GetSimpleLogger();

        public static readonly Random rng = new Random();
        private static int counter = 0;

        private static bool thinking;

        /// <summary>
        /// Score the passed in board
        /// </summary>
        /// <param name="board">Board to score</param>
        /// <returns>Score for the board</returns>
        private static int Score(CheckerBoard board, PlayerColor rootPlayer)
        {
            int score = board.ScoreWin(rootPlayer);

            if (score == 0)
            {
                switch (settings.Difficulty.ToLower())
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
                logger.Info(string.Format("Evaluating Node number: {0} - Score: {1}", counter, score));
            }

            logger.Debug(board.ToString());

            return score;
        }

        public static CheckersMove MinimaxStart(CheckerBoard board)
        {
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            thinking = true;

            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);
            List<int> values = new List<int>();

            logger.Info(string.Format("Max is {0}", board.CurrentPlayerTurn));

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
                    logger.Debug("Board Before");
                    logger.Debug(boardToMakeMoveOn.ToString());

                    boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                    boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                    moveToMake = moveToMake.NextMove;

                    logger.Debug("Board After");
                    logger.Debug(boardToMakeMoveOn.ToString());
                } while (moveToMake != null);

                values.Add(Minimax(boardToMakeMoveOn, settings.AIDepth - 1, alpha, beta, false, board.CurrentPlayerTurn));
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
            logger.Info("Node Values: " + string.Join(",", values.Select(x => x.ToString()).ToArray()));
            return bestMoves[rng.Next(bestMoves.Count)];
        }

        public static int Minimax(CheckerBoard board, int depth, int alpha, int beta, bool isMax, PlayerColor rootPlayer)
        {
            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);

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
                    } while (moveToMake != null);
                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, false, rootPlayer);

                    value = Math.Max(result, value);
                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        logger.Debug("Branch was pruned");
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
                    } while (moveToMake != null);

                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, true, rootPlayer);

                    value = Math.Min(result, value);
                    beta = Math.Min(alpha, value);

                    if (alpha >= beta)
                    {
                        logger.Debug("Branch was pruned");
                        break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Return true of false depending on if the AI is currently working
        /// </summary>
        /// <returns></returns>
        public static bool IsThinking()
        {
            return thinking;
        }
    }
}
