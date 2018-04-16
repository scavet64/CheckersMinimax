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
    public class WinningGenome: AbstractGenome
    {
        private static readonly Settings settings = Settings.Default;
        private static readonly object Lock = new object();
        private static WinningGenome instance;
        private static string filename = "WinningGenome.XML";

        public WinningGenome()
        {
            //By default, winning genome is based on settings file
            this.KingWorthGene = settings.KingWorth;
            this.KingDangerValueGene = settings.KingDangerValue;
            this.PawnDangerValueGene = settings.PawnDangerValue;
            this.PawnWorthGene = settings.PawnWorth;
        }


        public static WinningGenome GetWinningGenomeInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        if (File.Exists(filename))
                        {
                            instance = XmlSerializationHelper.Deserialize<WinningGenome>(filename);
                        }
                        else
                        {
                            //create new file and save it
                            instance = new WinningGenome();
                            instance.Serialize();
                        }
                    }
                }
            }

            return instance;
        }
    }
}
