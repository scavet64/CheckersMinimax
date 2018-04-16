using CheckersMinimax.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckersMinimax.Genetic
{
    [XmlRoot]
    public class RandomGenome : AbstractGenome
    {
        private static readonly string Filepath = FileNameHelper.GetExecutingDirectory() + "RandomGenome.xml";
        private static readonly object Lock = new object();
        private static RandomGenome instance;
        
        private readonly Random rng = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomGenome"/> class.
        /// </summary>
        public RandomGenome()
        {
            MutateGenome();
        }

        /// <summary>
        /// Gets the random genome instance.
        /// </summary>
        /// <returns>Random genome singleton instance</returns>
        public static RandomGenome GetRandomGenomeInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        if (File.Exists(Filepath))
                        {
                            instance = XmlSerializationHelper.Deserialize<RandomGenome>(Filepath);
                        }
                        else
                        {
                            //create new file and save it
                            instance = new RandomGenome();
                            instance.Serialize(Filepath);
                        }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Mutates the genome. Bases the values on the winning genome
        /// </summary>
        public void MutateGenome()
        {
            //Base Random Genome off of the winning Genome with slight random modifications
            WinningGenome winningGenome = WinningGenome.GetWinningGenomeInstance();

            this.KingDangerValueGene = winningGenome.KingDangerValueGene + rng.Next(-3, 3);
            this.KingWorthGene = winningGenome.KingWorthGene + rng.Next(-3, 3);
            this.PawnDangerValueGene = winningGenome.PawnDangerValueGene + rng.Next(-3, 3);
            this.PawnWorthGene = winningGenome.PawnWorthGene + rng.Next(-3, 3);
        }

        /// <summary>
        /// Mutates the genome and saves it.
        /// </summary>
        public void MutateGenomeAndSave()
        {
            MutateGenome();
            this.Serialize(Filepath);
        }
    }
}
