/**
 * Jameson Price
 * This is a class that encapsulates a list of blocks to maintain a set pattern. In this way we can ensure that the blocks will fall unanimously, and refrain
 * from falling should the rest of the pattern stop. Within this class I also deal with removing rows, which just removes blocks from a given row via the parameter.
 * Through this I can update the game board, and redraw it to show the player that they have completed a row.
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
using System.IO;

namespace Tetris
{
    class Pattern
    {

        private List<Block> blocks = new List<Block>();
        private int patternId;
        private int leftPosition;
        private int pivotBlock;
        MainWindow main;
        private Boolean stopped;

        public Pattern(MainWindow main, int pattId, Boolean isNew)
        {
            this.stopped = false;
            if(isNew)
            {
                this.patternId = pattId;
                leftPosition = 105;
            }
            this.main = main;
            if (isNew)
                InitializePattern();
            else
            {
                patternId = pattId;
                LoadPattern();
            }
        }

        public Boolean Empty
        {
            get
            {
                if (this.blocks.Count == 0)
                    return true;
                return false;
            }
        }

        public int Id
        {
            get { return this.patternId; }
        }
        
        public void PaintDisplay()
        {
            foreach (Block blk in blocks)
            {
                main.nextBlock.Children.Add(blk.Box);
            }
        }

        public Boolean Stopped
        {
            get { return this.stopped; }
            set { this.stopped = value; }
        }

        public Boolean IsTop()
        {
            foreach (Block blk in blocks)
            {
                if (blk.IsTop)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean CanMoveRight()
        {
            if (this.stopped)
                return false;
            foreach (Block blk in blocks)
            {
                if (!blk.CanMoveRight())
                {
                    return false;
                }
            }
            return true;
        }

        public Boolean CanMoveLeft()
        {
            if (this.stopped)
                return false;
            foreach (Block blk in blocks)
            {
                if (!blk.CanMoveLeft())
                {
                    return false;
                }
            }
            return true;
        }

        public Boolean CanFall()
        {
            foreach(Block blk in blocks)
            {
                if(!blk.CanFall())
                {
                    this.stopped = true;
                    return false;
                }
            }
            return true;
        }

        public Boolean CanPivotLeft()
        {
            if (!this.stopped)
            {
                int row, col, pivCol, pivRow, colDiff, rowDiff;
                if (this.patternId%10 == 6)
                { return false; }
                else
                {
                    for (int x = 0; x < blocks.Count; x++)
                    {
                        if (x == pivotBlock)
                        { }
                        else
                        {
                            row = blocks[x].Row;
                            col = blocks[x].Column;
                            pivRow = blocks[pivotBlock].Row;
                            pivCol = blocks[pivotBlock].Column;
                            colDiff = pivCol - col;
                            rowDiff = pivRow - row;
                            if (rowDiff == 0)
                            {
                                if (!blocks[x].CanMoveColZero(colDiff))
                                    return false;
                            }
                            else if (colDiff == 0)
                            {
                                if (!blocks[x].CanMoveColZeroPivotLeft(rowDiff))
                                    return false;
                            }
                            else
                            {
                                if (colDiff > 0 && rowDiff > 0)
                                {
                                    if (!blocks[x].CanMoveRowTwo(1))
                                        return false;
                                }
                                else if (colDiff < 0 && rowDiff < 0)
                                {
                                    if (!blocks[x].CanMoveRowTwo(-1))
                                        return false;
                                }
                                else if (colDiff < 0 && rowDiff > 0)
                                {
                                    if (!blocks[x].CanMoveColTwo(-1))
                                        return false;
                                }
                                else
                                {
                                    if (!blocks[x].CanMoveColTwo(1))
                                        return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public Boolean CanPivotRight()
        {
            if(!this.stopped)
            {
                int row, col, pivCol, pivRow, colDiff, rowDiff;
                if (this.patternId%10 == 6)
                {
                    return false;
                }
                else
                {
                    for (int x = 0; x < blocks.Count; x++)
                    {
                        if (x == pivotBlock)
                        { }
                        else
                        {
                            row = blocks[x].Row;
                            col = blocks[x].Column;
                            pivRow = blocks[pivotBlock].Row;
                            pivCol = blocks[pivotBlock].Column;
                            colDiff = pivCol - col;
                            rowDiff = pivRow - row;
                            if (rowDiff == 0)
                            {
                                if (!blocks[x].CanMoveRowZero(colDiff))
                                    return false;
                            }
                            else if (colDiff == 0)
                            {
                                if (!blocks[x].CanMoveColZero(rowDiff))
                                    return false;
                            }
                            else
                            {
                                if (colDiff > 0 && rowDiff > 0)
                                {
                                    if (!blocks[x].CanMoveColTwo(1))
                                        return false;
                                }
                                else if (colDiff < 0 && rowDiff < 0)
                                {
                                    if (!blocks[x].CanMoveColTwo(-1))
                                        return false;
                                }
                                else if (colDiff < 0 && rowDiff > 0)
                                {
                                    if (!blocks[x].CanMoveRowTwo(1))
                                        return false;
                                }
                                else
                                {
                                    if (!blocks[x].CanMoveRowTwo(-1))
                                        return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void MoveRight()
        {
            if (this.CanMoveRight())
            {
                foreach (Block blk in blocks)
                {
                    blk.MoveRight();
                }
                foreach (Block blk in blocks)
                {
                    blk.UpdateTable();
                }
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft())
            {
                foreach (Block blk in blocks)
                {
                    blk.MoveLeft();
                }
                foreach (Block blk in blocks)
                {
                    blk.UpdateTable();
                }
            }
        }

        public void MoveDown()
        {
            if(this.CanFall())
            {
                foreach(Block blk in blocks)
                {
                    blk.MoveDown();
                }
                foreach(Block blk in blocks)
                {
                    blk.UpdateTable();
                }
            }
        }

        public void PivotRight()
        {
            if(!this.stopped)
            {
                if(CanPivotRight())
                {
                    int row, col, pivCol, pivRow, colDiff, rowDiff;
                    if (this.patternId%10 == 6)
                    { }
                    else
                    {
                        for (int x = 0; x < blocks.Count; x++)
                        {
                            if (x == pivotBlock)
                            { }
                            else
                            {
                                row = blocks[x].Row;
                                col = blocks[x].Column;
                                pivRow = blocks[pivotBlock].Row;
                                pivCol = blocks[pivotBlock].Column;
                                colDiff = pivCol - col;
                                rowDiff = pivRow - row;
                                if (rowDiff == 0)
                                    blocks[x].MoveRowZero(colDiff);
                                else if (colDiff == 0)
                                    blocks[x].MoveColZero(rowDiff);
                                else
                                {
                                    if (colDiff > 0 && rowDiff > 0)
                                        blocks[x].MoveColTwo(1);
                                    else if (colDiff < 0 && rowDiff < 0)
                                        blocks[x].MoveColTwo(-1);
                                    else if (colDiff < 0 && rowDiff > 0)
                                        blocks[x].MoveRowTwo(1);
                                    else
                                        blocks[x].MoveRowTwo(-1);
                                }
                            }
                        }
                    }
                    foreach (Block blk in blocks)
                    {
                        blk.UpdateTable();
                    }
                }
            }
        }

        public void PivotLeft()
        {
            if (!this.stopped)
            {
                if (CanPivotLeft())
                {
                    int row, col, pivCol, pivRow, colDiff, rowDiff;
                    if (this.patternId%10 == 6)
                    { }
                    else
                    {
                        for (int x = 0; x < blocks.Count; x++)
                        {
                            if (x == pivotBlock)
                            { }
                            else
                            {
                                row = blocks[x].Row;
                                col = blocks[x].Column;
                                pivRow = blocks[pivotBlock].Row;
                                pivCol = blocks[pivotBlock].Column;
                                colDiff = pivCol - col;
                                rowDiff = pivRow - row;
                                if (rowDiff == 0)
                                    blocks[x].MoveColZero(colDiff);
                                else if (colDiff == 0)
                                    blocks[x].MoveColZeroPivotLeft(rowDiff);
                                else
                                {if (colDiff > 0 && rowDiff > 0)
                                        blocks[x].MoveRowTwo(1);
                                    else if (colDiff < 0 && rowDiff < 0)
                                        blocks[x].MoveRowTwo(-1);
                                    else if (colDiff < 0 && rowDiff > 0)
                                        blocks[x].MoveColTwo(-1);
                                    else
                                        blocks[x].MoveColTwo(1);
                                }
                            }
                        }
                    }
                    foreach (Block blk in blocks)
                    {
                        blk.UpdateTable();
                    }
                }
            }
        }

        public void paintPattern()
        {
            foreach(Block blk in blocks)
            {
                blk.UpdateTable();
                main.myGameCanvas.Children.Add(blk.Box);
            }
        }

        private void InitializePattern()
        {
            Random rand = new Random();
            int pattern = rand.Next(1, 8);
            string parentStr = "" + patternId + pattern;
            int parent = Int32.Parse(parentStr);
            patternId = parent;
            switch (pattern)
            {
                case 1:
                    SpawnLine();
                    break;
                case 2:
                    SpawnLLeft();
                    break;
                case 3:
                    SpawnLRight();
                    break;
                case 4:
                    SpawnSquiggleRight();
                    break;
                case 5:
                    SpawnSquiggleLeft();
                    break;
                case 6:
                    SpawnSquare();
                    break;
                case 7:
                    SpawnUnderwear();
                    break;
                default: break;
            }
        }

        public void LoadPattern()
        {
            for(int x = 0; x < 18; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    if(main[x,y] == patternId)
                    {
                        blocks.Add(new Block(x * 35, y * 35, patternId, main));
                    }
                }
            }
        }

        private void SpawnLine()
        {
            for(int x = 0; x < 4; x++)
            {
                blocks.Add(new Block(0, leftPosition + (x * 35), patternId, main));
            }
            this.pivotBlock = 1;
        }

        private void SpawnLLeft()
        {
            blocks.Add(new Block(0, leftPosition + 35, patternId, main));
            for (int x = 1; x < 4; x++)
            {
                blocks.Add(new Block(35, leftPosition + (x * 35), patternId, main));
            }
            this.pivotBlock = 2;
        }

        private void SpawnLRight()
        {
            blocks.Add(new Block(0, leftPosition + 105, patternId, main));
            for(int x = 1; x < 4; x++)
            {
                blocks.Add(new Block(35, leftPosition + (x * 35), patternId, main));
            }
            this.pivotBlock = 2;
        }

        private void SpawnSquiggleRight()
        {
            for(int x = 0; x < 2; x++)
            {
                blocks.Add(new Block(35, leftPosition + (x * 35), patternId, main));
            }
            blocks.Add(new Block(0, leftPosition + 70, patternId, main));
            blocks.Add(new Block(0, leftPosition + 35, patternId, main));
            this.pivotBlock = 1;
        }

        private void SpawnSquiggleLeft()
        {
            blocks.Add(new Block(0, leftPosition + 35, patternId, main));
            blocks.Add(new Block(0, leftPosition + 70, patternId, main));
            for (int x = 2; x < 4; x++)
            {
                blocks.Add(new Block(35, leftPosition + (x * 35), patternId, main));
            }
            this.pivotBlock = 1;
        }

        private void SpawnSquare()
        {
            for(int x = 0; x < 2; x++)
            {
                blocks.Add(new Block(0, leftPosition + (x * 35), patternId, main));
            }
            blocks.Add(new Block(35, leftPosition, patternId, main));
            blocks.Add(new Block(35, leftPosition + 35, patternId, main));
        }

        private void SpawnUnderwear()
        {
            for(int x = 0; x < 3; x++)
            {
                blocks.Add(new Block(0, leftPosition + (x * 35), patternId, main));
            }
            blocks.Add(new Block(35, leftPosition + 35, patternId, main));
            this.pivotBlock = 1;
        }

        public void removeElements(int row, int col)
        {
            for(int x = 0; x < blocks.Count; x++)
            {
                if(blocks[x].Row == row)
                {
                    blocks.RemoveAt(x);
                }
            }
        }
        public Boolean CanExist()
        {
            if (blocks.Count == 0)
                return false;
            return true;
        }
    }
}
