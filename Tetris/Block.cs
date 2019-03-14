/**
 * Jameson Price
 * This is a class that deals with drawing the block and keeping track of its current position. Admittedly, blockRow and blockColumn are unnecessary,
 * since you can do math on the blockLeft and blockTop to discover these, but I was needing to deal with rows and columns so often, that I just entirely
 * created private variables that would hold onto that data for me. Unfortunately, a lot of methods in this class and the pattern class are made public
 * so that the rest of the program can call them and use their functionality.
 * */

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
    class Block
    {

        private int blockTop;
        private int blockRow;
        private int blockLeft;
        private int blockColumn;
        private int parent;
        private Rectangle block;
        private MainWindow main;

        public Block(int top, int left, int parentId, MainWindow main)
        {
            this.blockRow = top / 35;
            this.blockColumn = left / 35;
            this.main = main;
            this.parent = parentId;
            Brush color = ColorDirectory(parentId);
            InitializeBlock(top, left, color);
        }

        public int Row
        {
            get { return this.blockRow; }
        }

        public int Column
        {
            get { return this.blockColumn; }
        }

        public Boolean IsTop
        {
            get
            {
                if (this.blockTop == 0)
                    return true;
                else
                    return false;
            }
        }

        private Brush ColorDirectory(int id)
        {
            switch(id%10)
            {
                case 1: return Brushes.Red;
                case 2: return Brushes.Green;
                case 3: return Brushes.Yellow;
                case 4: return Brushes.Purple;
                case 5: return Brushes.DarkCyan;
                case 6: return Brushes.Aquamarine;
                case 7: return Brushes.HotPink;
                default: return Brushes.Black;
            }
        }

        public Rectangle Box
        {
            get { return this.block; }
        }

        private void InitializeBlock(int top, int left, Brush color)
        {
            block = new Rectangle();
            block.Width = 34;
            block.Height = 34;
            block.Opacity = 1;
            blockTop = top;
            blockLeft = left;
            block.Fill = color;
            Canvas.SetTop(block, blockTop);
            Canvas.SetLeft(block, blockLeft);
        }

        public Boolean CanMoveRight()
        {
            if(blockColumn < 10)
            {
                try
                {
                    if(main[blockRow, blockColumn+1] == 0 || main[blockRow, blockColumn + 1] == parent)
                    {
                        return true;
                    }
                }
                catch(Exception err)
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean CanMoveLeft()
        {
            if (blockColumn > 0)
            {
                try
                {
                    if (main[blockRow, blockColumn - 1] == 0 || main[blockRow, blockColumn - 1] == parent)
                    {
                        return true;
                    }
                }
                catch (Exception err)
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean CanFall()
        {
            if(blockRow < 18)
            {
                try
                {
                    if (main[blockRow + 1, blockColumn] == 0 || ((main[blockRow + 1, blockColumn] == parent)))
                    {
                        return true;
                    }
                }
                catch(Exception err)
                {
                    return false;
                }
            }
            return false;
        }

        public void MoveRight()
        {
            blockLeft += 35;
            main[blockRow, blockColumn] = 0;
            blockColumn++;
            Canvas.SetLeft(block, blockLeft);
        }

        public void MoveLeft()
        {
            blockLeft -= 35;
            main[blockRow, blockColumn] = 0;
            blockColumn--;
            Canvas.SetLeft(block, blockLeft);
        }

        public void MoveDown()
        {
            blockTop += 35;
            main[blockRow, blockColumn] = 0;
            blockRow ++;
            Canvas.SetTop(block, blockTop);
        }

        public Boolean CanMoveRowZero(int diff)
        {
            int checkRow = blockRow - diff;
            int checkCol = blockColumn + diff;
            if (checkRow > 17 || checkRow < 0 || checkCol > 9 || checkCol < 0)
                return false;
            if ((main[checkRow, checkCol] != 0 && main[checkRow, checkCol] != parent))
                return false;
            return true;
        }

        public Boolean CanMoveColZero(int diff)
        {
            int checkRow = blockRow + diff;
            int checkCol = blockColumn + diff;
            if (checkRow > 17 || checkRow < 0 || checkCol > 9 || checkCol < 0)
                return false;
            if ((main[checkRow, checkCol] != 0 && main[checkRow, checkCol] != parent))
                return false;
            return true;
        }

        public Boolean CanMoveColTwo(int modifier)
        {
            int amt = 2;
            amt = amt * modifier;
            if (blockColumn + amt < 0 || blockColumn + amt > 9)
                return false;
            if (main[blockRow, blockColumn + amt] != 0 && main[blockRow, blockColumn - 2] != parent)
                return false;
            return true;
        }

        public Boolean CanMoveRowTwo(int modifier)
        {
            int amt = 2;
            amt = amt * modifier;
            if (blockRow + amt > 17 || blockRow + amt < 0)
                return false;
            if (main[blockRow + amt, blockColumn] != 0 && main[blockRow + amt, blockColumn] != parent)
                return false;
            return true;
        }

        public Boolean CanMoveColZeroPivotLeft(int diff)
        {
            int checkRow = blockRow + diff;
            int checkCol = blockColumn - diff;
            if (checkRow > 17 || checkRow < 0 || checkCol > 9 || checkCol < 0)
                return false;
            if ((main[checkRow, checkCol] != 0 && main[checkRow, checkCol] != parent))
                return false;
            return true;
        }

        public void MoveColZeroPivotLeft(int diff)
        {
            main[blockRow, blockColumn] = 0;
            blockRow += diff;
            blockColumn -= diff;
            blockLeft = blockColumn * 35;
            blockTop = blockRow * 35;
            Canvas.SetTop(block, blockTop);
            Canvas.SetLeft(block, blockLeft);
        }

        public void MoveRowZero(int diff)
        {
            main[blockRow, blockColumn] = 0;
            blockRow -= diff;
            blockColumn += diff;
            blockLeft = blockColumn * 35;
            blockTop = blockRow * 35;
            Canvas.SetTop(block, blockTop);
            Canvas.SetLeft(block, blockLeft);
        }

        public void MoveColZero(int diff)
        {
            main[blockRow, blockColumn] = 0;
            blockRow += diff;
            blockColumn += diff;
            blockLeft = blockColumn * 35;
            blockTop = blockRow * 35;
            Canvas.SetTop(block, blockTop);
            Canvas.SetLeft(block, blockLeft);
        }

        public void MoveRowTwo(int modifier)
        {
            int amt = 2;
            amt = amt * modifier;
            main[blockRow, blockColumn] = 0;
            blockRow += amt;
            blockTop = blockRow * 35;
            Canvas.SetTop(block, blockTop);
        }

        public void MoveColTwo(int modifier)
        {
            int amt = 2;
            amt = amt * modifier;
            main[blockRow, blockColumn] = 0;
            blockColumn += amt;
            blockLeft = blockColumn * 35;
            Canvas.SetLeft(block, blockLeft);
        }

        public void UpdateTable()
        {
            main[blockRow, blockColumn] = parent;
        }

    }
}
