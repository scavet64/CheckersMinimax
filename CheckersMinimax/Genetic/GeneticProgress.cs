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
    public class GeneticProgress
    {
        private static GeneticProgress instance;
        private static readonly object Lock = new object();
        private static readonly string filepath = FileNameHelper.GetExecutingDirectory() + @"GeneticProgress.xml";

        public int NumberOfRandomGenomeWins {
            get
            {
                return numberOfRandomGenomeWins;
            }
            set {
                numberOfRandomGenomeWins = value;
                this.Serialize(filepath);
            }
        }

        public int NumberOfGames
        {
            get { return numberOfGames; }
            set
            {
                numberOfGames = value;
                this.Serialize(filepath);
            }
        }

        private int numberOfGames;
        private int numberOfRandomGenomeWins;
        

        public GeneticProgress()
        {

        }

        public static GeneticProgress GetGeneticProgressInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        //if (File.Exists(filepath))
                        //{
                        //    instance = XmlSerializationHelper.Deserialize<GeneticProgress>(filepath);
                        //}
                        //else
                        //{
                        //    //create new file and save it
                        //    instance = new GeneticProgress();
                        //    instance.Serialize(filepath);
                        //}
                        instance = new GeneticProgress();
                        instance.Serialize(filepath);
                    }
                }
            }

            return instance;
        }

        public void ResetValues()
        {
            numberOfGames = 0;
            numberOfRandomGenomeWins = 0;
            this.Serialize(filepath);
        }
    }
}
