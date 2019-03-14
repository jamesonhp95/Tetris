/**
 * This was a small class to define the 3D falling 4 length line of a tetris block. Unfortunately I was unable to complete the 3D game board in time for this assignment, so
 * I am archiving this file for later use and completion.
 * Jameson Price
 **/ 

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
    class DisplayLine
    {
        /*
        private Polygon topFront = new Polygon();
        private Polygon topSide;
        private Polygon mid1Front = new Polygon();
        private Polygon mid1Side;
        private Polygon mid2Front = new Polygon();
        private Polygon mid2Side;
        private Polygon botFront = new Polygon();
        private Polygon botSide;
        private Polygon botBottom = new Polygon();

        private string sector;
        private double blkSize;
        private double maxTop;
        private double maxLeft;
        private double center;
        private double maxRight;

        private double top;
        private double left;
        private double right;
        private double topBot;
        private double mid1Bot;
        private double mid2Bot;
        private double botBot;
        private double sectorOffset;

        private double topSizeConvert = .6;
        private double mid1SizeConvert = .6;
        private double mid2SizeConvert = .82;
        private double botSizeConvert = 1.1;

        private MainMenu main;

        public DisplayLine(string sector, MainMenu mainMenu)
        {
            this.main = mainMenu;
            sector.ToLower();
            double sectorSize = main.displayGame.Width / 3;
            this.center = sectorSize / 2;
            this.maxLeft = sectorSize / 4;
            this.maxRight = sectorSize - this.maxLeft;
            this.maxTop = main.displayGame.Height;
            if (sector.Equals("left"))
                this.sectorOffset = 0;
            else if (sector.Equals("center"))
                this.sectorOffset = sectorSize;
            else if (sector.Equals("right"))
                this.sectorOffset = 2 * sectorSize;
            else
                throw new FormatException();
            this.sector = sector;
            InitializeLine();
        }

        private void InitializeLine()
        {
            this.top = 10;
            this.left = (this.center - (this.maxLeft / 2));
            this.right = (this.center + (this.maxLeft / 2));
            this.topBot = ((this.top + (this.right - this.left)) * this.topSizeConvert);
            this.mid1Bot = this.topBot + ((this.top + (this.right - this.left)) * this.mid1SizeConvert);
            this.mid2Bot = this.mid1Bot + ((this.top + (this.right - this.left)) * this.mid2SizeConvert);
            this.botBot = this.mid2Bot + ((this.top + (this.right - this.left)) * this.botSizeConvert);
            InitializeTop();
        }

        private void InitializeTop()
        {

            PointCollection frontPoints = new PointCollection();
            PointCollection mid1Points = new PointCollection();
            PointCollection mid2Points = new PointCollection();
            PointCollection botPoints = new PointCollection();
            PointCollection bot2Points = new PointCollection();
            frontPoints.Add(new Point((this.left * 1.05)+this.sectorOffset, this.top));
            frontPoints.Add(new Point((this.right * .95)+this.sectorOffset, this.top));
            frontPoints.Add(new Point(this.right+this.sectorOffset, this.topBot));
            frontPoints.Add(new Point(this.left+this.sectorOffset, this.topBot));

            mid1Points.Add(new Point(this.left + this.sectorOffset, this.topBot));
            mid1Points.Add(new Point(this.right + this.sectorOffset, this.topBot));
            mid1Points.Add(new Point((this.right * 1.08) + this.sectorOffset, this.mid1Bot));
            mid1Points.Add(new Point((this.left * .92) + this.sectorOffset, this.mid1Bot));

            mid2Points.Add(new Point((this.left * .92) + this.sectorOffset, this.mid1Bot));
            mid2Points.Add(new Point((this.right * 1.08) + this.sectorOffset, this.mid1Bot));
            mid2Points.Add(new Point((this.right * 1.18) + this.sectorOffset, this.mid2Bot));
            mid2Points.Add(new Point((this.left * .82) + this.sectorOffset, this.mid2Bot));
            
            botPoints.Add(new Point((this.left * .82) + this.sectorOffset, this.mid2Bot));
            botPoints.Add(new Point((this.right * 1.18) + this.sectorOffset, this.mid2Bot));
            botPoints.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
            botPoints.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));

            bot2Points.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));
            bot2Points.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
            bot2Points.Add(new Point((this.right * 1.20) + this.sectorOffset, this.botBot + 40));
            bot2Points.Add(new Point((this.left * .8) + this.sectorOffset, this.botBot + 40));
            
            if (this.sector.Equals("center"))
            {
                bot2Points.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.20) + this.sectorOffset, this.botBot + 40));
                bot2Points.Add(new Point((this.left * .8) + this.sectorOffset, this.botBot + 40));
            }
            else if(this.sector.Equals("left"))
            {
                bot2Points.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.20) + this.sectorOffset, this.botBot + 40));
                bot2Points.Add(new Point((this.left * .8) + this.sectorOffset, this.botBot + 40));
            }
            else
            {
                bot2Points.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
                bot2Points.Add(new Point((this.right * 1.5) + this.sectorOffset, this.botBot + 40));
                bot2Points.Add(new Point((this.left * .8) + this.sectorOffset, this.botBot + 40));
            }

            topFront.Points = frontPoints;
            topFront.Stroke = Brushes.Black;
            topFront.Fill = Brushes.Red;

            mid1Front.Points = mid1Points;
            mid1Front.Stroke = Brushes.Black;
            mid1Front.Fill = Brushes.Red;

            mid2Front.Points = mid2Points;
            mid2Front.Stroke = Brushes.Black;
            mid2Front.Fill = Brushes.Red;

            botFront.Points = botPoints;
            botFront.Stroke = Brushes.Black;
            botFront.Fill = Brushes.Red;

            botBottom.Points = bot2Points;
            botBottom.Stroke = Brushes.Black;
            botBottom.Fill = Brushes.Red;

            this.main.displayGame.Children.Add(topFront);
            this.main.displayGame.Children.Add(mid1Front);
            this.main.displayGame.Children.Add(mid2Front);
            this.main.displayGame.Children.Add(botFront);
            this.main.displayGame.Children.Add(botBottom);
        }

        public void Iterate()
        {
            IterateWidth();
            IterateDown();
        }

        private void IterateWidth()
        {
            if (this.right < this.maxRight)
                this.right = this.right + .06;
            if (this.left < this.maxLeft)
            {
                this.left = this.left - .06;
                this.top = this.top - .04;
                this.topBot = this.topBot + .04;
                this.mid1Bot = this.mid1Bot + .04;
                this.mid2Bot = this.mid2Bot + .04;
                this.botBot = this.botBot + .04;
            }
        }

        private void IterateDown()
        {
            this.top++;
            this.topBot++;
            this.mid1Bot++;
            this.mid2Bot++;
            this.botBot++;
            changeFrontPoints();
        }
        
        private void changeFrontPoints()
        {
            PointCollection frontPoints = new PointCollection();
            PointCollection mid1Points = new PointCollection();
            PointCollection mid2Points = new PointCollection();
            PointCollection botPoints = new PointCollection();
            PointCollection bot2Points = new PointCollection();
            frontPoints.Add(new Point((this.left * 1.05) + this.sectorOffset, this.top));
            frontPoints.Add(new Point((this.right * .95) + this.sectorOffset, this.top));
            frontPoints.Add(new Point(this.right + this.sectorOffset, this.topBot));
            frontPoints.Add(new Point(this.left + this.sectorOffset, this.topBot));

            mid1Points.Add(new Point(this.left + this.sectorOffset, this.topBot));
            mid1Points.Add(new Point(this.right + this.sectorOffset, this.topBot));
            mid1Points.Add(new Point((this.right * 1.08) + this.sectorOffset, this.mid1Bot));
            mid1Points.Add(new Point((this.left * .92) + this.sectorOffset, this.mid1Bot));

            mid2Points.Add(new Point((this.left * .92) + this.sectorOffset, this.mid1Bot));
            mid2Points.Add(new Point((this.right * 1.08) + this.sectorOffset, this.mid1Bot));
            mid2Points.Add(new Point((this.right * 1.18) + this.sectorOffset, this.mid2Bot));
            mid2Points.Add(new Point((this.left * .82) + this.sectorOffset, this.mid2Bot));

            botPoints.Add(new Point((this.left * .82) + this.sectorOffset, this.mid2Bot));
            botPoints.Add(new Point((this.right * 1.18) + this.sectorOffset, this.mid2Bot));
            botPoints.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
            botPoints.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));

            bot2Points.Add(new Point((this.left * .68) + this.sectorOffset, this.botBot));
            bot2Points.Add(new Point((this.right * 1.32) + this.sectorOffset, this.botBot));
            bot2Points.Add(new Point((this.right * 1.20) + this.sectorOffset, this.botBot + 40));
            bot2Points.Add(new Point((this.left * .8) + this.sectorOffset, this.botBot + 40));

            topFront.Points = frontPoints;
            topFront.Stroke = Brushes.Black;
            topFront.Fill = Brushes.Red;

            mid1Front.Points = mid1Points;
            mid1Front.Stroke = Brushes.Black;
            mid1Front.Fill = Brushes.Red;

            mid2Front.Points = mid2Points;
            mid2Front.Stroke = Brushes.Black;
            mid2Front.Fill = Brushes.Red;

            botFront.Points = botPoints;
            botFront.Stroke = Brushes.Black;
            botFront.Fill = Brushes.Red;

            botBottom.Points = bot2Points;
            botBottom.Stroke = Brushes.Black;
            botBottom.Fill = Brushes.Red;
            
            this.main.displayGame.Children.Add(topFront);
            this.main.displayGame.Children.Add(mid1Front);
            this.main.displayGame.Children.Add(mid2Front);
            this.main.displayGame.Children.Add(botFront);
            this.main.displayGame.Children.Add(botBottom);
        }*/
    }
}
