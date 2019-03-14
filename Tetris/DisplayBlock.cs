using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    class DisplayBlock
    {
        //private Line
        private Rectangle front;
        private Rectangle bottom;
        private Rectangle top;
        private Rectangle side;
        private int startLeft;
        private int maxTop;
        private int startSize;
        private int orientation;


        public DisplayBlock(int left, int maxTopPix, int size)
        {
            startLeft = left;
            maxTop = maxTopPix;
            startSize = size;
        }

        public void Iterate()
        {

        }
    }
}
