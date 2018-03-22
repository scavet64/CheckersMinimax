using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    class CheckerPiece
    {
        CheckersPoint Location { get; set; }

        public CheckerPiece(CheckersPoint location)
        {
            this.Location = location;
        }
    }
}
