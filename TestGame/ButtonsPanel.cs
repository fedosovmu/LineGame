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
        private MouseHoverZone _mouseHoverZone;
        private const int _x = 0;
        private const int _y = GameScene.PixelHeight;
        private const int _panelHeight = 150;
        private const int _panelWidth = 500;


        public ButtonsPanel (MainForm form)
        {
            _mainForm = form;
            _mainForm.Shown += Form_Shown;
            _mainForm.MouseClick += Form_MouseClick;
            _mainForm.MainTimer.Tick += Timer_Tick;
            _mouseHoverZone = new MouseHoverZone(_mainForm, _x, _y, _panelWidth, _panelHeight);
        }



        private void DrawPanel (int mouseX = -1, int mouseY = -1)
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), _x, _y, _panelWidth, _panelHeight);

            Font font = new Font("Arial", 16);
            SolidBrush fontBrush = new SolidBrush(Color.White);
            String header = "Здания и преобразователи:";
            MainForm.G.DrawString(header, font, fontBrush, _x + 10, _y + 10);

            // Draw selected items
            if (_mouseHoverZone.IsMouseHover())
            {
                const int bright = 120;
                Color selectedItemColor = Color.FromArgb(bright, bright, bright);
                MainForm.G.FillRectangle(new SolidBrush(selectedItemColor), 0, GameScene.PixelHeight, _panelWidth, _panelHeight);
            }
        }



        private void Form_Shown (object sender, EventArgs e)
        {
            DrawPanel();
        }



        private void Form_MouseClick(object sender, MouseEventArgs e)
        {

        }



        private void Timer_Tick (object sender, EventArgs e)
        {
            DrawPanel();
        }
    }
}
