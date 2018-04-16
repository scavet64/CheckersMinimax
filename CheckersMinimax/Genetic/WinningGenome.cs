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
        private static string filepath = FileNameHelper.GetExecutingDirectory() +  "WinningGenome.XML";

        public WinningGenome()
        {
            //By default, winning genome is based on settings file
            this.KingWorthGene = Settings.KingWorth;
            this.KingDangerValueGene = Settings.KingDangerValue;
            this.PawnDangerValueGene = Settings.PawnDangerValue;
            this.PawnWorthGene = Settings.PawnWorth;
        }

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
