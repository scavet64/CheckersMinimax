using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class MakeMoveReturnModel
    {
        private bool wasMoveMade;

        public bool WasMoveMade
        {
            get { return wasMoveMade; }
            set { wasMoveMade = value; }
        }

        private bool isTurnOver;

        public bool IsTurnOver
        {
            get { return isTurnOver; }
            set { isTurnOver = value; }
        }
    }
}
