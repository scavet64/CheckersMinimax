using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    public class NullCheckerPiece : CheckerPiece
    {
        public NullCheckerPiece()
        {
            imageSource = null;
        }

        public override ImageSource BuildCheckerImageSource()
        {
            return null;
        }

        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation)
        {
            return new List<CheckersPoint>();
        }
    }
}
