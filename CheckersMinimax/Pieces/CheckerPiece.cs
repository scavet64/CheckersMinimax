using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    public abstract class CheckerPiece
    {
        //CheckersPoint Location { get; set; }

        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public CheckerPiece()
        {
            //this.Location = location;
        }
    }
}
