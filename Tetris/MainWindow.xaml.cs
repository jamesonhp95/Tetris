/**
 * Jameson Price
 * This is where all the magic happens, and this is where the game board is cleared and repainted upon completion of a row,
 * as well as many other things dealing with the mechanical parts of keeping the game flowing. However, all functionality of 
 * how the blocks move and are painted are found within Block.cs and Pattern.cs.
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
using System.Windows.Threading;
using System.IO;


namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] gameBoard = new int[18,10];
        private List<Pattern> patterns = new List<Pattern>();
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private Pattern fallingPattern;
        private Pattern nextPattern;
        private int totalCompletedRows = 0;
        private int curScore = 0;
        private int lvl = 1;
        private int waitTime = 3;
        private Boolean lost = false;
        private MainMenu myMenu;

        private string playerName;
        private Boolean classic;

        public MainWindow(Boolean isClassic, string name, int startLevel, MainMenu aMenu)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            initializeBoard();
            gameTimer.Interval = getTimeSpan();
            gameTimer.Tick += GameTimer_Tick;
            SpawnFirst();
            classic = isClassic;
            playerName = name;
            totalCompletedRows = startLevel * 10;
            gameTimer.IsEnabled = true;
            myMenu = aMenu;
            this.mnuFileGo.IsEnabled = false;
        }
        
        private TimeSpan getTimeSpan()
        {
            if (this.lvl == 1)
                return new TimeSpan(0, 0, 0, 0, 500);
            double time = 500;
            for(int x = 1; x < this.lvl; x++)
            {
                time = (time / 5) * 4;
            }
            int myTimeSpan = (int)time;
            return new TimeSpan(0, 0, 0, 0, myTimeSpan);
        }

        public Boolean isClassic
        {
            get { return this.classic; }
        }

        public int this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x > 17 || y > 9)
                    throw new IndexOutOfRangeException();
                else
                    return this.gameBoard[x, y];
            }
            set
            {
                if (x < 0 || y < 0 || x > 17 || y > 9)
                     throw new IndexOutOfRangeException();
                else
                    this.gameBoard[x, y] = value;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.lvl = (this.totalCompletedRows/10) +1;
            this.level.Content = "" + lvl;
            if (this.lvl >= 10)
                this.startColor.Color = Colors.Red;
            else if (this.lvl >= 5)
                this.startColor.Color = Colors.OrangeRed;
            if(waitTime == 0)
            {
                this.myGameCanvas.Opacity = 1;
                this.gradient.Opacity = .6;
                this.grid.Opacity = .6;
                this.status.Content = "";
                this.status.Opacity = 0;
                this.wait.Content = "";
                this.wait.Opacity = 0;
                Boolean spawnAnother = true;
                fallingPattern.MoveDown();
                if (!fallingPattern.Stopped)
                    spawnAnother = false;
                if (fallingPattern.IsTop())
                    lost = true;
                if (lost)
                {
                    if (Int32.Parse(bestScore.Content.ToString()) < this.curScore)
                        this.bestScore.Content = "" + this.curScore;
                    this.gameTimer.IsEnabled = false;
                    this.myGameCanvas.Opacity = .6;
                    this.gradient.Opacity = .3;
                    this.grid.Opacity = .3;
                    this.status.Content = "Game Over! \nPress R to \nplay again.";
                    this.status.Opacity = 1;
                }
                if (spawnAnother && !lost)
                {
                    UpdateGame();
                    this.gameTimer.Interval = getTimeSpan();
                    SpawnNext();
                }
            }
            else
            {
                this.myGameCanvas.Opacity = .6;
                this.gradient.Opacity = .3;
                this.grid.Opacity = .3;
                this.wait.Opacity = 1;
                this.status.Opacity = 0;
                this.wait.Content = "" + this.waitTime;
                this.waitTime--;
            }
            
        }

        private void UpdateGame()
        {
            this.gameTimer.Interval = getTimeSpan();
            int rowsCompleted = 0;
            for(int x = 0; x < 18; x++)
            {
                if (IsRowFull(x))
                {
                    rowsCompleted++;
                    removeRow(x);
                }
            }
            if(rowsCompleted > 0)
            {
                int total = 0;
                int baseBonus = rowsCompleted * 100;
                int bonusScore = (rowsCompleted - 1) * 50;
                total = baseBonus + bonusScore;
                total = total * lvl;
                curScore += total;
                this.score.Content = "" + curScore;
                this.totalCompletedRows += rowsCompleted;
                foreach(Pattern patt in patterns)
                {
                    if (patt.CanExist() && !classic)
                    {
                        while (patt.CanFall())
                            patt.MoveDown();
                    }
                    else if (patt.CanExist())
                    {
                        for(int z = 0; z < rowsCompleted; z++)
                        {
                            if (patt.CanFall())
                                patt.MoveDown();
                        }
                    }
                }
            }
        }

        private void removeRow(int row)
        {
            for (int x = 0; x < 10; x++)
            {
                foreach (Pattern patt in patterns)
                {
                    if (patt.Id == gameBoard[row, x] && patt.CanExist())
                    {
                        patt.removeElements(row, x);
                    }
                }
            }
            this.myGameCanvas.Children.Clear();
            initializeBoard();
            foreach (Pattern patt in patterns)
            {
                patt.paintPattern();
            }
        }
        
        private Boolean IsRowFull(int row)
        {
            for(int x = 0; x < 10; x++)
            {
                if(gameBoard[row, x] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void initializeBoard()
        {
            for(int x = 0; x < 18; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    gameBoard[x, y] = 0;
                }
            }
        }
        private void SpawnFirst()
        {
            Pattern patt = new Pattern(this, patterns.Count+1, true);
            fallingPattern = patt;
            patterns.Add(patt);
            patt.paintPattern();
            nextPattern = new Pattern(this, patterns.Count + 1, true);
            nextPattern.PaintDisplay();
        }

        private void SpawnNext()
        {
            this.nextBlock.Children.Clear();
            fallingPattern = nextPattern;
            patterns.Add(fallingPattern);
            fallingPattern.paintPattern();
            Pattern patt = new Pattern(this, patterns.Count + 1, true);
            nextPattern = patt;
            nextPattern.PaintDisplay();
        }

        private void Reset()
        {
            this.patterns.Clear();
            this.myGameCanvas.Children.Clear();
            this.score.Content = "0";
            this.curScore = 0;
            this.level.Content = "1";
            this.lvl = 1;
            this.fallingPattern = null;
            this.nextPattern = null;
            this.nextBlock.Children.Clear();
            this.totalCompletedRows = 0;
            this.lost = false;
            initializeBoard();
            this.status.Opacity = 0;
            this.myGameCanvas.Opacity = 1;
            this.grid.Opacity = .6;
            this.gradient.Opacity = .6;
            this.gameTimer.Interval = getTimeSpan();
            this.gameTimer.IsEnabled = true;
            SpawnFirst();
        }
        
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            ExitCurrentGame();
        }

        private void ExitCurrentGame()
        {
            MessageBoxResult res = MessageBox.Show("Do you wish to return to the Main Menu?", "Leave game?", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                this.Close();
                //this.myMenu.Resume();
                this.myMenu.ShowDialog();
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            this.gameTimer.IsEnabled = false;
            var folderBrowserDialog = new System.Windows.Forms.SaveFileDialog();
            System.Windows.Forms.DialogResult res = folderBrowserDialog.ShowDialog();
            if(res == System.Windows.Forms.DialogResult.OK)
            {
                Boolean create = true;
                string txt = "";
                Stream stream;
                txt += this.playerName + "\n";
                txt += this.classic + "\n";
                txt += this.score.Content + "\n";
                txt += this.totalCompletedRows + "\n";
                if (folderBrowserDialog.CheckFileExists)
                {
                    MessageBoxResult result = MessageBox.Show("The save file for " + folderBrowserDialog.FileName + ".txt already exists. \nWould you like to Overwrite the save file with this one?", "Overwrite", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                        create = false;
                }
                try
                {
                    if(create)
                    {
                        for (int x = 0; x < 18; x++)
                        {
                            for (int y = 0; y < 10; y++)
                            {
                                txt += gameBoard[x, y] + "\n";
                            }
                        }
                        stream = folderBrowserDialog.OpenFile();
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(txt);
                        }
                    }
                }
                catch(Exception err)
                {
                    MessageBox.Show("Unable to Save file.");
                }
            }
            this.gameTimer.IsEnabled = true;
        }
        
        private void mnuFileLoad_Click(object sender, EventArgs e)
        {
            this.gameTimer.IsEnabled = false;
            MessageBoxResult res = MessageBox.Show("You will lose all progress on your current game.\nIs that ok?", "Load previous game", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                fallingPattern = null;
                patterns.Clear();
                initializeBoard();
                myGameCanvas.Children.Clear();
                Stream sr;
                var folderBrowserDialog = new System.Windows.Forms.OpenFileDialog();
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    sr = folderBrowserDialog.OpenFile();
                    List<int> board = new List<int>();
                    try
                    {
                        using (StreamReader stream = new StreamReader(sr))
                        {
                            string txt = "";
                            this.playerName = stream.ReadLine();
                            string boolean = stream.ReadLine();
                            if (boolean.Equals("True"))
                                this.classic = true;
                            else
                                this.classic = false;
                            this.curScore = Int32.Parse(stream.ReadLine());
                            this.score.Content = "" + curScore;
                            this.totalCompletedRows = Int32.Parse(stream.ReadLine());
                            while ((txt = stream.ReadLine()) != null)
                            {
                                if (!txt.Equals(""))
                                    board.Add(Int32.Parse(txt));
                            }
                        }
                        int count = 0;
                        string str = "";
                        for (int x = 0; x < 18; x++)
                        {
                            for (int y = 0; y < 10; y++)
                            {
                                gameBoard[x, y] = board[count];
                                str += board[count] + " ";
                                count++;
                            }
                            str += "\n";
                        }
                        Pattern patt;
                        Boolean Empty = false;
                        for (int a = 17; a > -1; a--)
                        {
                            if (!Empty)
                            {
                                Empty = true;
                                for (int b = 0; b < 10; b++)
                                {
                                    if (gameBoard[a, b] != 0 && CanCreate(gameBoard[a, b]))
                                    {
                                        Empty = false;
                                        patt = new Pattern(this, gameBoard[a, b], false);
                                        patterns.Add(patt);
                                    }
                                    if (gameBoard[a, b] != 0)
                                        Empty = false;
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Unable to Load File.");
                    }
                }
                initializeBoard();
                foreach (Pattern item in patterns)
                {
                    item.paintPattern();
                }
                this.gameTimer.IsEnabled = true;
                SpawnFirst();
            }
            else
            {
                this.gameTimer.IsEnabled = true;
            }
        }

        private Boolean CanCreate(int pattId)
        {
            foreach(Pattern patt in patterns)
            {
                if (patt.Id == pattId)
                    return false;
            }
            return true;
        }

        private void mnuFilePause_Click(object sender, EventArgs e)
        {
            PauseGame();
        }

        private void mnuFileGo_Click(object sender, EventArgs e)
        {
            GameGo();
        }

        private void PauseGame()
        {
            this.gameTimer.IsEnabled = false;
            this.mnuFilePause.IsEnabled = false;
            this.mnuFileGo.IsEnabled = true;
            this.myGameCanvas.Opacity = .6;
            this.gradient.Opacity = .3;
            this.grid.Opacity = .3;
            this.status.Content = "Game is Paused\nPress 'G' \nto resume!";
            this.status.Opacity = 1;
        }

        private void GameGo()
        {
            this.mnuFileGo.IsEnabled = false;
            this.mnuFilePause.IsEnabled = true;
            this.waitTime = 3;
            this.gameTimer.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(gameTimer.IsEnabled && this.wait.Opacity == 0)
            {
                if (e.Key == Key.Right)
                    fallingPattern.MoveRight();
                if (e.Key == Key.Left)
                    fallingPattern.MoveLeft();
                if (e.Key == Key.Down)
                    fallingPattern.PivotRight();
                if (e.Key == Key.Up)
                    fallingPattern.PivotLeft();
                if (e.Key == Key.Space)
                    this.gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            }
            if(lost && e.Key == Key.R)
            {
                Reset();
            }
            if(e.Key == Key.X)
                ExitCurrentGame();
            if (e.Key == Key.P && this.gameTimer.IsEnabled == true && this.lost != true)
                PauseGame();
            if (e.Key == Key.G && this.gameTimer.IsEnabled == false && this.lost != true)
                GameGo();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.myMenu.ShowDialog();
        }
    }
}
