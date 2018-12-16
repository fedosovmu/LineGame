using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestGame
{
    class ButtonsPanel
    {
        private MainForm _mainForm;
        private const int _panelHeight = 150;
        private const int _panelWidth = 500;


        public ButtonsPanel (MainForm form)
        {
            _mainForm = form;
            _mainForm.Shown += Form_Shown;
            _mainForm.MouseClick += Form_MouseClick;
            _mainForm.MouseMove += Form_MouseMove;
        }



        private void Clear()
        {
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), 0, GameScene.PixelHeight, _panelWidth, _panelHeight);
        }



        private void DrawPanel (int mouseX = -1, int mouseY = -1)
        {
            this.Clear();

            if ((mouseY > GameScene.PixelHeight && mouseX < _panelWidth) && (mouseX >= 0 && mouseY >= 0))
            {
                const int bright = 120;
                Color selectedItemColor = Color.FromArgb(bright, bright, bright);
                MainForm.G.FillRectangle(new SolidBrush(selectedItemColor), 0, GameScene.PixelHeight, _panelWidth, _panelHeight);
            }
        }




        private void Form_Shown (object sender, EventArgs e)
        {
            DrawPanel();
            _mainForm.Refresh();
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form_MouseMove (object sender, MouseEventArgs e)
        {
            if (e.Y >= GameScene.SceneHeight)
            {
                DrawPanel(e.X, e.Y);
            }
        }
    }
}
