using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Clone
{
    /// <summary>
    /// Interface for a clonable object
    /// </summary>
    public interface IMinimaxClonable
    {
        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>A clone to be used in the minimax algoritm</returns>
        object GetMinimaxClone();
    }
}
