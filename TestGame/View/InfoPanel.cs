using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestGame
{
    class InfoPanel
    {
        private MainForm _mainForm;
        private Game _game;
        private const int _x = ButtonsPanel.Width - 2;
        private const int _y = GameScene.Height;
        public const int Height = ButtonsPanel.Height;
        public const int Width = GameScene.Width - ButtonsPanel.Width + 2;



        public InfoPanel(MainForm form, Game game, Timer timer)
        {
            _mainForm = form;
            _game = game;
            _mainForm.Shown += Form_Shown;
            timer.Tick += (s, e) => DrawPanel();
        }



        private void DrawPanel()
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), _x, _y, Width, Height);

            Font font = new Font("Arial", 16);
            SolidBrush fontBrush = new SolidBrush(Color.White);
            //String header = "Информация:";
            //MainForm.G.DrawString(header, font, fontBrush, _x + 10, _y + 10);


            if (CellSelector.IsCellSelected())
            {
                int selectedX = CellSelector.X;
                int selectedY = CellSelector.Y;
                var selectedBuilding = _game.Buildings[selectedX, selectedY];

                // Draw building name
                String header = selectedBuilding.Name;
                MainForm.G.DrawString(header, font, fontBrush, _x + 10, _y + 10);

                // Draw building (X, Y)
                String position =  "X: " + selectedBuilding.X + ", Y: " + selectedBuilding.Y;
                MainForm.G.DrawString(position, font, fontBrush, _x + 155, _y + 50);


                // Draw building
                const int windowX = _x + 16;
                const int windowY = _y + 50;
                const int windowSize = 130;
                int posX = (CellPainter.CellSize * selectedX) - 31;
                int posY = (CellPainter.CellSize * selectedY) - 31;
                MainForm.G.DrawImage(MainForm.Btm, windowX, windowY, new Rectangle(posX, posY, windowSize, windowSize), GraphicsUnit.Pixel);

                // Draw building window      
                var pen = new Pen(new SolidBrush(Color.White), 2);
                MainForm.G.DrawRectangle(pen, new Rectangle(windowX, windowY, windowSize, windowSize));
            }




            // Draw ping info    
            //var text = "раз раз";
            //MainForm.G.DrawString(text, font, fontBrush, _x + 170, _y + 40);
            //_tickNumber++;
            //var distance = (_tickNumber % 100) * 2 + _tickNumber % 2;
            //MainForm.G.FillRectangle(new SolidBrush(Color.Yellow), _x + 170 + distance, _y + 40, 30, 30);
        }



        private void Form_Shown(object sender, EventArgs e)
        {
            DrawPanel();
        }
    }
}
