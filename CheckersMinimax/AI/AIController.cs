using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public class AIController
    {
        public static readonly int KING_WORTH = 30;
        public static readonly int PAWN_WORTH = 15;
        public static readonly int AI_DEPTH = 10;
        public static readonly int PAWN_DANGER_VALUE = 10;
        public static readonly int KING_DANGER_VALUE = 15;

        public static readonly Random rng = new Random();
        private static int counter = 0;

        private MinimaxNode root;
        private bool thinking;

        /// <summary>
        /// Score the passed in board
        /// </summary>
        /// <param name="board">Board to score</param>
        /// <returns>Score for the board</returns>
        private static int Score(CheckerBoard board)
        {
            int score = board.ScoreA() + board.ScoreB() + board.ScoreC();
            if (counter++ % 10000 == 0)
            {
                Console.WriteLine(string.Format("Evaluating Node number: {0} - Score: {1}", counter, score));
            }

            //Console.WriteLine(board.ToString());

            return score;
        }

        public static CheckersMove MinimaxStart(CheckerBoard board)
        {
            int alpha = int.MinValue;
            int beta = int.MaxValue;

            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);
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
                    //Console.WriteLine("Board Before");
                    //Console.WriteLine(boardToMakeMoveOn.ToString());
                    boardToMakeMoveOn = (CheckerBoard)boardToMakeMoveOn.GetMinimaxClone();
                    boardToMakeMoveOn.MakeMoveOnBoard((CheckersMove)moveToMake.GetMinimaxClone());
                    moveToMake = moveToMake.NextMove;
                    //Console.WriteLine("Board After");
                    //Console.WriteLine(boardToMakeMoveOn.ToString());
                } while (moveToMake != null);

                values.Add(Minimax(boardToMakeMoveOn, AI_DEPTH - 1, alpha, beta, false));
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

            counter = 0;
            Console.WriteLine("Node Values: " + string.Join(",", values.Select(x => x.ToString()).ToArray()));
            return bestMoves[rng.Next(bestMoves.Count)];
        }

        public static int Minimax(CheckerBoard board, int depth, int alpha, int beta, bool isMax)
        {
            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);

            if (depth == 0 || possibleMoves.Count == 0)
            {
                //return board.ScoreB();
                return Score(board);
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
                    int result = Minimax(boardToMakeMoveOn, depth - 1, alpha, beta, false);

                    value = Math.Max(result, value);
                    alpha = Math.Max(alpha, value);

                    if (alpha >= beta)
                    {
                        //Console.WriteLine("Branch was pruned");
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
                    beta = Math.Min(alpha, value);

                    if (alpha >= beta)
                    {
                        //Console.WriteLine("Branch was pruned");
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
