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
            root = new MinimaxNode(game);

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

        /// <summary>
        /// Get the next best move for the AI to make
        /// </summary>
        /// <returns>The AI's best move</returns>
        public CheckersMove BestMove()
        {
            if (root.NodeList.Count == 0)
            {
                return null;
            }
            MinimaxNode max = root.NodeList[0];
            if (root.Board.CurrentPlayerTurn == PlayerColor.Red)
            {
                foreach (MinimaxNode node in root.NodeList)
                {
                    if (max.Score < node.Score)
                    {
                        max = node;
                    }
                }
            }
            else
            {
                foreach (MinimaxNode node in root.NodeList)
                {
                    if (max.Score > node.Score)
                    {
                        max = node;
                    }
                }
            }

            Console.WriteLine("Move to use: " + max.MoveUsed);

            root = max;
            populate(AI_DEPTH, root, root.Board.CurrentPlayerTurn == PlayerColor.Red ? int.MaxValue : int.MinValue);
            return max.MoveUsed;
        }

        /// <summary>
        /// Populate the gamestate tree
        /// </summary>
        /// <param name="layersDeep">How many layers the AI should look</param>
        /// <param name="node">Minimax node that the algorithm is going to populate</param>
        /// <param name="parentValue">The parent nodes value</param>
        public void populate(int layersDeep, MinimaxNode node, int parentValue)
        {
            if (node.Board.GetWinner() == null && layersDeep != 0)
            {
                if (node.NodeList.Count == 0)
                {
                    foreach (CheckersMove move in node.Board.getMovesForPlayer(node.Board.CurrentPlayerTurn))
                    {
                        CheckerBoard board = node.Board.Copy();
                        board.MakeMoveOnBoard(move);

                        MinimaxNode newNode = new MinimaxNode(board, move);
                        node.NodeList.Add(newNode);
                    }
                    foreach (MinimaxNode newNode in node.NodeList)
                    {
                        if ((node.Board.CurrentPlayerTurn != PlayerColor.Red && parentValue <= node.Score) ||
                                (node.Board.CurrentPlayerTurn == PlayerColor.Red && parentValue >= node.Score))
                        {
                            populate(layersDeep - 1, newNode, node.Score);
                        }

                        Console.WriteLine("PRUNED: " + node);



                        if (node.Board.CurrentPlayerTurn == PlayerColor.Red)
                        {
                            if (node.Score < newNode.Score)
                            {
                                node.Score = newNode.Score;
                            }
                        }
                        else
                        {
                            if (node.Score > newNode.Score)
                            {
                                node.Score = newNode.Score;
                            }
                        }
                    }
                }
            }
            else
            {
                node.Score = score(node.Board);
            }
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
