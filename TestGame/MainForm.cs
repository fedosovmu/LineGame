using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    public partial class MainForm : Form
    {
        public Timer MainTimer;
        private Bitmap Btm;
        public static Graphics G;
        public static Color BackgroundColor;
        private GameScene _gameScene;
        private ButtonsPanel _buttonsPanel;
        private Game _game;


        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(GameScene.CellSize * Game.SceneWindth + 100, GameScene.CellSize * Game.SceneHeight + ButtonsPanel.Height + 100);

            Btm = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            G = Graphics.FromImage(Btm);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.BackgroundImage = Btm;
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackgroundColor = Color.FromArgb(10, 10, 10);

            MainTimer = new Timer();
            MainTimer.Interval = 50;

            _game = new Game();
            _gameScene = new GameScene(this, _game);
            _buttonsPanel = new ButtonsPanel(this, _game);

            MainTimer.Tick += (s, e) => this.Refresh();          
        }



        private void MainForm_Shown(object sender, EventArgs e)
        {          
            MainTimer.Start();
            this.Refresh();
        }
    }
}
