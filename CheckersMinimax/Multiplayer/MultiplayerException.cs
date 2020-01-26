using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Multiplayer
{
    public class MultiplayerException : Exception
    {
        public MultiplayerException(string message) : base (message) {}
    }
}
