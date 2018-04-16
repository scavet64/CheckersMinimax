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
    public class WinningGenome : AbstractGenome
    {
        private static readonly Settings Settings = Settings.Default;
        private static readonly object Lock = new object();
        private static WinningGenome instance;
        private static string filepath = FileNameHelper.GetExecutingDirectory() + "WinningGenome.XML";

        /// <summary>
        /// Initializes a new instance of the <see cref="WinningGenome"/> class.
        /// </summary>
        public WinningGenome()
        {
            //By default, winning genome is based on settings file
            this.KingWorthGene = Settings.KingWorth;
            this.KingDangerValueGene = Settings.KingDangerValue;
            this.PawnDangerValueGene = Settings.PawnDangerValue;
            this.PawnWorthGene = Settings.PawnWorth;
        }

        /// <summary>
        /// Gets the winning genome instance.
        /// </summary>
        /// <returns>Return the winning genome instance</returns>
        public static WinningGenome GetWinningGenomeInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        if (File.Exists(filepath))
                        {
                            instance = XmlSerializationHelper.Deserialize<WinningGenome>(filepath);
                        }
                        else
                        {
                            //create new file and save it
                            instance = new WinningGenome();
                            instance.Serialize(filepath);
                        }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Sets the new winning genome.
        /// </summary>
        /// <param name="newWinner">The new winner.</param>
        public void SetNewWinningGenome(AbstractGenome newWinner)
        {
            this.KingWorthGene = newWinner.KingWorthGene;
            this.KingDangerValueGene = newWinner.KingDangerValueGene;
            this.PawnDangerValueGene = newWinner.PawnDangerValueGene;
            this.PawnWorthGene = newWinner.PawnWorthGene;

            this.Serialize(filepath);
        }
    }
}
