using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class MakeMoveReturnModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [was move made].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was move made]; otherwise, <c>false</c>.
        /// </value>
        public bool WasMoveMade { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is turn over.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is turn over; otherwise, <c>false</c>.
        /// </value>
        public bool IsTurnOver { get; set; }
    }
}
