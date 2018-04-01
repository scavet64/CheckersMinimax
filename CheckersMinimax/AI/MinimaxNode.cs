using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public class MinimaxNode
    {
        public List<MinimaxNode> NodeList { get; set; }
        public CheckerBoard Board { get; set; }
        public CheckersMove MoveUsed { get; set; }
        public int Score { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimaxNode"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public MinimaxNode(CheckerBoard board)
        {
            this.Board = board;
            NodeList = new List<MinimaxNode>();
            MoveUsed = null;
            Score = board.CurrentPlayerTurn == PlayerColor.Red ? int.MinValue : int.MaxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimaxNode"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="move">The move.</param>
        public MinimaxNode(CheckerBoard board, CheckersMove move)
        {
            this.Board = board;
            NodeList = new List<MinimaxNode>();
            MoveUsed = move;
            Score = board.CurrentPlayerTurn == PlayerColor.Red ? int.MinValue : int.MaxValue;

        }
    }
}
