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
            //root = new MinimaxNode(game);

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

        ///// <summary>
        ///// Get the next best move for the AI to make
        ///// </summary>
        ///// <returns>The AI's best move</returns>
        //public CheckersMove BestMove()
        //{
        //    if (root.NodeList.Count == 0)
        //    {
        //        return null;
        //    }
        //    MinimaxNode max = root.NodeList[0];
        //    if (root.Board.CurrentPlayerTurn == PlayerColor.Red)
        //    {
        //        foreach (MinimaxNode node in root.NodeList)
        //        {
        //            if (max.Score < node.Score)
        //            {
        //                max = node;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (MinimaxNode node in root.NodeList)
        //        {
        //            if (max.Score > node.Score)
        //            {
        //                max = node;
        //            }
        //        }
        //    }

        //    Console.WriteLine("Move to use: " + max.MoveUsed);

        //    root = max;
        //    populate(AI_DEPTH, root, root.Board.CurrentPlayerTurn == PlayerColor.Red ? int.MaxValue : int.MinValue);
        //    return max.MoveUsed;
        //}

        public CheckersMove MinimaxStart(CheckerBoard board, int depth, bool isMax)
        {
            double alpha = Double.NegativeInfinity;
            double beta = Double.PositiveInfinity;

            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);
            CheckerBoard tempBoard = null;
            List<double> values = new List<double>();

            if (possibleMoves.IsNullOrEmpty())
            {
                return null;
            }
            int temp = 0;
            foreach(CheckersMove move in possibleMoves)
            {
                temp++;
                Console.WriteLine("Evaluating Move : " + temp);
                tempBoard = (CheckerBoard) board.GetMinimaxClone();
                tempBoard.MakeMoveOnBoard((CheckersMove) move.GetMinimaxClone());
                values.Add(minimax(tempBoard, depth - 1, alpha, beta, !isMax));
            }

            double maxHeuristics = Double.NegativeInfinity;
            foreach (double value in values)
            {
                if(value >= maxHeuristics)
                {
                    maxHeuristics = value;
                }
            }

            //filter the list of moves based on max value
            foreach (double value in values)
            {
                if (value < maxHeuristics)
                {
                    possibleMoves.RemoveAt((int)value);
                }
            }

            Random random = new Random();
            return possibleMoves[random.Next(possibleMoves.Count)];
        }

        public double minimax(CheckerBoard board, int depth, double alpha, double beta, bool isMax)
        {
            if (depth == 0)
            {
                return score(board);
            }
            List<CheckersMove> possibleMoves = board.GetMovesForPlayer(board.CurrentPlayerTurn);

            double initial = 0;
            CheckerBoard tempBoard = null;
            if (isMax)
            {
                initial = Double.NegativeInfinity;
                foreach (CheckersMove move in possibleMoves)
                {
                    tempBoard = (CheckerBoard)board.GetMinimaxClone();
                    tempBoard.MakeMoveOnBoard((CheckersMove)move.GetMinimaxClone());

                    double result = minimax(tempBoard, depth - 1, alpha, beta, !isMax);

                    initial = Math.Max(result, initial);
                    alpha = Math.Max(alpha, initial);

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
            }
            else
            {
                initial = Double.PositiveInfinity;
                foreach (CheckersMove move in possibleMoves)
                {
                    tempBoard = (CheckerBoard)board.GetMinimaxClone();
                    tempBoard.MakeMoveOnBoard((CheckersMove)move.GetMinimaxClone());

                    double result = minimax(tempBoard, depth - 1, alpha, beta, !isMax);

                    initial = Math.Max(result, initial);
                    alpha = Math.Max(alpha, initial);

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
            }

            return initial;
        }

        ///// <summary>
        ///// Populate the gamestate tree
        ///// </summary>
        ///// <param name="layersDeep">How many layers the AI should look</param>
        ///// <param name="node">Minimax node that the algorithm is going to populate</param>
        ///// <param name="parentValue">The parent nodes value</param>
        //public void populate(int layersDeep, MinimaxNode node, int parentValue)
        //{
        //    if (node.Board.GetWinner() == null && layersDeep != 0)
        //    {
        //        if (node.NodeList.Count == 0)
        //        {
        //            foreach (CheckersMove move in node.Board.GetMovesForPlayer(node.Board.CurrentPlayerTurn))
        //            {
        //                CheckerBoard board = (CheckerBoard)node.Board.GetMinimaxClone();
        //                board.MakeMoveOnBoard((CheckersMove) move.GetMinimaxClone());

        //                MinimaxNode newNode = new MinimaxNode(board, move);
        //                node.NodeList.Add(newNode);
        //            }
        //            foreach (MinimaxNode newNode in node.NodeList)
        //            {
        //                if ((node.Board.CurrentPlayerTurn != PlayerColor.Red && parentValue <= node.Score) ||
        //                        (node.Board.CurrentPlayerTurn == PlayerColor.Red && parentValue >= node.Score))
        //                {
        //                    populate(layersDeep - 1, newNode, node.Score);
        //                }

        //                Console.WriteLine("PRUNED: " + node);



        //                if (node.Board.CurrentPlayerTurn == PlayerColor.Red)
        //                {
        //                    if (node.Score < newNode.Score)
        //                    {
        //                        node.Score = newNode.Score;
        //                    }
        //                }
        //                else
        //                {
        //                    if (node.Score > newNode.Score)
        //                    {
        //                        node.Score = newNode.Score;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        node.Score = score(node.Board);
        //    }
        //}

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
