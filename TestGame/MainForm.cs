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
        int x = 0;


        public MainForm()
        {
            InitializeComponent();

            Btm = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            G = Graphics.FromImage(Btm);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.BackgroundImage = Btm;
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackgroundColor = Color.FromArgb(10, 10, 10);

            MainTimer = new Timer();
            MainTimer.Interval = 50;

            _gameScene = new GameScene(this);
            _buttonsPanel = new ButtonsPanel(this);

            MainTimer.Tick += (s, e) => this.Refresh();        
        }



        private void MainForm_Shown(object sender, EventArgs e)
        {          
            MainTimer.Start();
            this.Refresh();
        }
    }
}
