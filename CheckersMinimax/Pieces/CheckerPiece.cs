using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    public abstract class CheckerPiece
    {
        //CheckersPoint Location { get; set; }

        protected string imageSource;

        //public string ImageSource
        //{
        //    get { return imageSource; }
        //    set { imageSource = value; }
        //}

        public CheckerPiece()
        {
            //this.Location = location;
        }

        public virtual ImageSource BuildCheckerImageSource()
        {
            return new BitmapImage(new Uri(imageSource, UriKind.Relative));
        }

        public abstract List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation);
    }
}
