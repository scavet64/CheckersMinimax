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
            if (root.nodeList.Count == 0)
            {
                return null;
            }
            MinimaxNode max = root.nodeList[0];
            if (root.board.CurrentPlayerTurn == PlayerColor.Red)
            {
                foreach (MinimaxNode node in root.nodeList)
                {
                    if (max.score < node.score)
                    {
                        max = node;
                    }
                }
            }
            else
            {
                foreach (MinimaxNode node in root.nodeList)
                {
                    if (max.score > node.score)
                    {
                        max = node;
                    }
                }
            }

            Console.WriteLine("Move to use: " + max.moveUsed);

            root = max;
            populate(AI_DEPTH, root, root.board.CurrentPlayerTurn == PlayerColor.Red ? int.MaxValue : int.MinValue);
            return max.moveUsed;
        }

        /// <summary>
        /// Populate the gamestate tree
        /// </summary>
        /// <param name="layersDeep">How many layers the AI should look</param>
        /// <param name="node">Minimax node that the algorithm is going to populate</param>
        /// <param name="parentValue">The parent nodes value</param>
        public void populate(int layersDeep, MinimaxNode node, int parentValue)
        {
            if (node.board.GetWinner() == null && layersDeep != 0)
            {
                if (node.nodeList.Count == 0)
                {
                    foreach (CheckersMove move in node.board.getMovesForPlayer(node.board.CurrentPlayerTurn))
                    {
                        CheckerBoard board = node.board.Copy();
                        board.MakeMoveOnBoard(move);

                        MinimaxNode newNode = new MinimaxNode(board, move);
                        node.nodeList.Add(newNode);
                    }
                    foreach (MinimaxNode newNode in node.nodeList)
                    {
                        if ((node.board.CurrentPlayerTurn != PlayerColor.Red && parentValue <= node.score) ||
                                (node.board.CurrentPlayerTurn == PlayerColor.Red && parentValue >= node.score))
                        {
                            populate(layersDeep - 1, newNode, node.score);
                        }

                        Console.WriteLine("PRUNED: " + node);



                        if (node.board.CurrentPlayerTurn == PlayerColor.Red)
                        {
                            if (node.score < newNode.score)
                            {
                                node.score = newNode.score;
                            }
                        }
                        else
                        {
                            if (node.score > newNode.score)
                            {
                                node.score = newNode.score;
                            }
                        }
                    }
                }
            }
            else
            {
                node.score = score(node.board);
            }
        }

        /// <summary>
        /// Return true of false depending on if the AI is currently working
        /// </summary>
        /// <returns></returns>
        public bool isThinking()
        {
            return thinking;
        }
    }
}
