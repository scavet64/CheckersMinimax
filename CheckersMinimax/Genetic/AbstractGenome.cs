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
    [XmlRoot]
    public class AbstractGenome
    {
        [XmlElement]
        public int KingWorthGene { get; set; }

        [XmlElement]
        public int PawnWorthGene { get; set; }

        [XmlElement]
        public int PawnDangerValueGene { get; set; }

        [XmlElement]
        public int KingDangerValueGene { get; set; }
    }
}
