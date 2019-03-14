/**
 * Jameson Price
 * This is my Main menu that gives the About, Details, Classic / Cascade game modes, controls, as well as the cheat button to increase the level.
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        /**
         * Archived use for the 3D intro screen, all associated code, including GameTimer is commented out.
         * 
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private DisplayLine line1;
        private DisplayLine line2;
        private DisplayLine line3;*/
        private int cheatCode = 0;
        public MainMenu()
        {
            InitializeComponent();

            /*
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            this.line1 = new DisplayLine("center", this);
            this.line2 = new DisplayLine("left", this);
            this.line3 = new DisplayLine("right", this);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.IsEnabled = true;
            */
        }
        /*
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.displayGame.Children.Clear();
            this.line1.Iterate();
            this.line2.Iterate();
            this.line3.Iterate();
        }

        public void Resume()
        {
            this.gameTimer.IsEnabled = true;
        }
        */
        private void mnuFileClassic_Click(object sender, EventArgs e)
        {
            ClassicGame();
        }

        private void mnuFileCascade_Click(object sender, EventArgs e)
        {
            CascadeGame();
        }
        

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox mybox = new AboutBox();
            mybox.ShowDialog();
        }

        private void mnuHelpDetails_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a Tetris game with two game modes. The first game mode, labled Classic, is the original gravity rules of normal tetris, where blocks will" +
                " only fall equal distance to rows that have been completed below them, if they are capable of falling. This is also what I believe the base assignment expected. " +
                "As it stands, choose this mode and you will encounter classic \'naive\' gravity, as one would call it, along with all of the Controls listed on the Main menu in working order. " +
                "\nThe other game mode, Cascade mode, has full gravity effects, in this way blocks will fall as far as possible if they are able to fall. That being said, blocks will not separate" +
                "from their patterns unless they are the only blocks left alive! So be careful, and have fun! \nFor more information regarding the game, completion amounts, extra credit, and controls - Please read the README.txt found with this solution.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassicGame();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CascadeGame();
        }
        private void ClassicGame()
        {
            string name;
            if (this.pName.Text != "Type your name here. If left blank, defaults to Anonymous!")
            {
                name = this.pName.Text;
            }
            else
                name = "Anonymous";
            this.Hide();
            //this.gameTimer.IsEnabled = false;
            Window main = new MainWindow(true, name, this.cheatCode, this);
            main.ShowDialog();
        }

        private void CascadeGame()
        {
            string name;
            if (this.pName.Text != "Type your name here. If left blank, defaults to Anonymous!")
            {
                name = this.pName.Text;
            }
            else
                name = "Anonymous";
            this.Hide();
            //this.gameTimer.IsEnabled = false;
            Window main = new MainWindow(false, name, this.cheatCode, this);
            main.ShowDialog();
        }
        private void pName_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.pName.Text = "";
            this.pName.Opacity = 1;
        }

        private void LogoLevelUp_Click(object sender, RoutedEventArgs e)
        {
            if(this.cheatCode < 11)
                this.cheatCode++;
        }
    }
}
