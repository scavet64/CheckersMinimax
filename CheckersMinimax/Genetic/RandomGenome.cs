using CheckersMinimax.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Genetic
{
    public class RandomGenome: AbstractGenome
    {
        private readonly object Lock = new object();
        private RandomGenome instance;
        private static string filename = "RandomGenome.xml";
        private readonly Random rng = new Random();

        public RandomGenome()
        {
            //Base Random Genome off of the winning Genome with slight random modifications
            WinningGenome winningGenome = WinningGenome.GetWinningGenomeInstance();

            this.KingDangerValueGene = winningGenome.KingDangerValueGene * rng.Next(-3,3);
            this.KingWorthGene = winningGenome.KingWorthGene * rng.Next(-3, 3);
            this.PawnDangerValueGene = winningGenome.PawnDangerValueGene * rng.Next(-3, 3);
            this.PawnWorthGene = winningGenome.PawnWorthGene * rng.Next(-3, 3);
        }

        public RandomGenome GetRandomGenomeInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        if (File.Exists(filename))
                        {
                            instance = XmlSerializationHelper.Deserialize<RandomGenome>(filename);
                        }
                        else
                        {
                            //create new file and save it
                            instance = new RandomGenome();
                            instance.Serialize();
                        }
                    }
                }
            }

            return instance;
        }


    }
}
