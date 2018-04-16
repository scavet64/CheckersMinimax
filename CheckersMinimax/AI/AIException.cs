using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.AI
{
    [Serializable]
    public class AIException : Exception
    {
        public AIException(string message) : base(message)
        {
        }
    }
}
