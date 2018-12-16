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
        private static Bitmap Btm;
        public static Graphics G;
        public static Color BackgroundColor;



        public MainForm()
        {
            InitializeComponent();

            Btm = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            G = Graphics.FromImage(Btm);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.BackgroundImage = Btm;

            BackgroundColor = Color.FromArgb(10, 10, 10);
        }



        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
