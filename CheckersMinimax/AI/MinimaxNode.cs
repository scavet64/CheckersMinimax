using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public class MinimaxNode
    {
        public List<MinimaxNode> nodeList { get; set; }
        public CheckerBoard board { get; set; }
        public CheckersMove moveUsed { get; set; }
        public int score { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimaxNode"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public MinimaxNode(CheckerBoard board)
        {
            this.board = board;
            nodeList = new List<MinimaxNode>();
            moveUsed = null;
            score = board.CurrentPlayerTurn == PlayerColor.Red ? int.MinValue : int.MaxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimaxNode"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="move">The move.</param>
        public MinimaxNode(CheckerBoard board, CheckersMove move)
        {
            this.board = board;
            nodeList = new List<MinimaxNode>();
            moveUsed = move;
            score = board.CurrentPlayerTurn == PlayerColor.Red ? int.MinValue : int.MaxValue;

        }
    }
}
