using System;
using System.ArrayExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    public class Minimax
    {
        public static int INFINITY { get; set; } = int.MaxValue;
        public static int MINUS_INFINITY { get; set; } = int.MinValue;
        //public int Score { get; set; } = -1;

        public static int AlphaBetaMinimax(MinimaxNode node, int depth, int alpha, int beta, bool isMax)
        {
            int value;
            if (depth == 0 || node.NodeList.IsNullOrEmpty())
            {
                return node.Score;
            }
            if (isMax)
            {
                value = MINUS_INFINITY;
                foreach (MinimaxNode childNode in node.NodeList)
                {
                    value = Math.Max(value, AlphaBetaMinimax(childNode, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, value);
                    if(beta <= alpha)
                    {
                        //beta cutoff
                        break;
                    }
                }
                return value;
            }
            else
            {
                value = INFINITY;
                foreach (MinimaxNode childNode in node.NodeList)
                {
                    value = Math.Min(value, AlphaBetaMinimax(childNode, depth - 1, alpha, beta, true));
                    beta = Math.Min(beta, value);
                    if (beta <= alpha)
                    {
                        //alpha cutoff
                        break;
                    }
                }
                return value;
            }
        }
    }
}
