using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public class AIController
    {
        public static readonly int KING_WORTH = 5;
        public static readonly int PAWN_WORTH = 1;
        public static readonly int AI_DEPTH = 3;

        private MinimaxNode root;
        private bool thinking;

        /// <summary>
        /// Construtor for the AI Controller.
        /// </summary>
        /// <param name="game">Current checkerboard to initilize the state</param>
        public AIController(CheckerBoard game)
        {
            //Spool off a new thread and start algorithm
        }

        /// <summary>
        /// Score the passed in board
        /// </summary>
        /// <param name="board">Board to score</param>
        /// <returns>Score for the board</returns>
        private static int score(CheckerBoard board)
        {
            return board.ScoreA();
        }

        public CheckersMove MinimaxStart(CheckerBoard board, int depth, bool isMax)
        {
            int alpha = int.MinValue;
            int beta = int.MaxValue;

            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);
            CheckerBoard tempBoard = null;
            List<int> values = new List<int>();

            if (possibleMoves.IsNullOrEmpty())
            {
                return null;
            }
            int temp = 0;
            foreach(CheckersMove move in possibleMoves)
            {
                //tempBoard = (CheckerBoard) board.GetMinimaxClone();
                //tempBoard.MakeMoveOnBoard((CheckersMove) move.GetMinimaxClone());

                CheckersMove moveToMake = move;
                CheckerBoard boardToMakeMoveOn = board;
                do
                {
                    boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                    boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                    moveToMake = moveToMake.NextMove;
                } while (moveToMake != null);

                values.Add(Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, false));
            }

            int maxHeuristics = int.MinValue;
            foreach (int value in values)
            {
                if(value >= maxHeuristics)
                {
                    maxHeuristics = value;
                }
            }

            //filter the list of moves based on max value
            List<CheckersMove> bestMoves = new List<CheckersMove>();
            for (int i = 0; i < values.Count; i++) {
                if (values[i] == maxHeuristics)
                {
                    bestMoves.Add(possibleMoves[i]);
                }
            }

            Console.WriteLine("Node Values: " + string.Join(",", values.Select(x => x.ToString()).ToArray()));

            Random random = new Random();
            return bestMoves[random.Next(bestMoves.Count)];
        }

        public int Minimax(CheckerBoard board, int depth, int alpha, int beta, bool isMax)
        {
            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);

            if (depth == 0 || possibleMoves.Count == 0)
            {
                //return board.ScoreB();
                return score(board);
            }
            

            int value = 0;
            CheckerBoard tempBoard = null;
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
                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, false);

                    value = Math.Max(result, value);
                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
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

                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, true);

                    value = Math.Min(result, value);
                    alpha = Math.Min(alpha, value);

                    if (alpha >= beta)
                    {
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
        public bool IsThinking()
        {
            return thinking;
        }
    }
}
