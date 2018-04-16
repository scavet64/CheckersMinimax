using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class CheckersGame
    {
        public CheckerBoard Board { get; set; }

        public PlayerColor CurrentPlayerTurn { get; set; }
    }
}
