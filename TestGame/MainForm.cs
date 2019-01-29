using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineGame
{
    public partial class MainForm : Form
    {
        public static Bitmap Btm;
        public static Graphics G;
        public static Color BackgroundColor;
        private Timer _mainTimer;
        private GameScene _gameScene;
        private ButtonsPanel _buttonsPanel;
        private InfoPanel _infoPanel;
        private Game _game;       



        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(GameScene.Width + 100, GameScene.Height + ButtonsPanel.Height + 100);

            Btm = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            G = Graphics.FromImage(Btm);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.BackgroundImage = Btm;
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackgroundColor = Color.FromArgb(10, 10, 10);           
            
            _mainTimer = new Timer();
            _mainTimer.Interval = 25;

            _game = new Game();
            _buttonsPanel = new ButtonsPanel(this, _game, _mainTimer);          
            _gameScene = new GameScene(this, _game, _mainTimer);
            _infoPanel = new InfoPanel(this, _game, _mainTimer);

            _mainTimer.Tick += (s, e) => this.Refresh();          
        }



        private void MainForm_Shown(object sender, EventArgs e)
        {
            _mainTimer.Start();
            this.Refresh();
        }
    }
}
