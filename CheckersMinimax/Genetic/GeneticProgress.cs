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
        private static readonly object Lock = new object();
        private static readonly string Filepath = FileNameHelper.GetExecutingDirectory() + @"GeneticProgress.xml";

        private static GeneticProgress instance;

        /// <summary>
        /// Gets or sets the number of random genome wins.
        /// </summary>
        /// <value>
        /// The number of random genome wins.
        /// </value>
        [XmlElement]
        public int NumberOfRandomGenomeWins
        {
            get
            {
                return numberOfRandomGenomeWins;
            }

            set
            {
                numberOfRandomGenomeWins = value;
                this.Serialize(Filepath);
            }
        }

        /// <summary>
        /// Gets or sets the number of games.
        /// </summary>
        /// <value>
        /// The number of games.
        /// </value>
        [XmlElement]
        public int NumberOfGames
        {
            get
            {
                return numberOfGames;
            }

            set
            {
                numberOfGames = value;
                this.Serialize(Filepath);
            }
        }

        /// <summary>
        /// Gets or sets the number of rounds.
        /// </summary>
        /// <value>
        /// The number of rounds.
        /// </value>
        public int NumberOfRounds
        {
            get
            {
                return numberOfRounds;
            }

            set
            {
                numberOfRounds = value;
                this.Serialize(Filepath);
            }
        }


        private int numberOfGames;
        private int numberOfRandomGenomeWins;
        private int numberOfRounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticProgress"/> class.
        /// </summary>
        public GeneticProgress()
        {
        }

        /// <summary>
        /// Gets the genetic progress instance.
        /// </summary>
        /// <returns>Genetic progress singleton</returns>
        public static GeneticProgress GetGeneticProgressInstance()
        {
            if (instance == null)
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        if (File.Exists(Filepath))
                        {
                            try
                            {
                                instance = XmlSerializationHelper.Deserialize<GeneticProgress>(Filepath);
                            }
                            catch (Exception ex)
                            {
                                //If there is a problem with the progress file, just make a new one
                                instance = new GeneticProgress();
                                instance.Serialize(Filepath);
                            }
                        }
                        else
                        {
                            //create new file and save it
                            instance = new GeneticProgress();
                            instance.Serialize(Filepath);
                        }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Resets the values for this singleton.
        /// </summary>
        public void ResetValues()
        {
            numberOfGames = 0;
            numberOfRandomGenomeWins = 0;
            this.Serialize(Filepath);
        }

        /// <summary>
        /// Deletes the file from the disk
        /// </summary>
        public void Delete()
        {
            if (File.Exists(Filepath))
            {
                File.Delete(Filepath);
            }
        }
    }
}
