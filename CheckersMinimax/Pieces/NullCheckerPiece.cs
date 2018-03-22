using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    class NullCheckerPiece : CheckerPiece
    {
        public NullCheckerPiece()
        {
            imageSource = null;
        }

        public override ImageSource BuildCheckerImageSource()
        {
            return null;
        }
    }
}
