using CheckersMinimax.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CheckersMinimax.Genetic
{
    /// <summary>
    /// Abstract Genome Class. Holds information about the Genome
    /// </summary>
    [XmlRoot]
    public class AbstractGenome
    {
        /// <summary>
        /// Gets or sets the king worth gene.
        /// </summary>
        /// <value>
        /// The king worth gene.
        /// </value>
        [XmlElement]
        public int KingWorthGene { get; set; }

        /// <summary>
        /// Gets or sets the pawn worth gene.
        /// </summary>
        /// <value>
        /// The pawn worth gene.
        /// </value>
        [XmlElement]
        public int PawnWorthGene { get; set; }

        /// <summary>
        /// Gets or sets the pawn danger value gene.
        /// </summary>
        /// <value>
        /// The pawn danger value gene.
        /// </value>
        [XmlElement]
        public int PawnDangerValueGene { get; set; }

        /// <summary>
        /// Gets or sets the king danger value gene.
        /// </summary>
        /// <value>
        /// The king danger value gene.
        /// </value>
        [XmlElement]
        public int KingDangerValueGene { get; set; }
    }
}
