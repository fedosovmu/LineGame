using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestGame
{
    class GameScene
    {
        private MainForm _mainForm;
        private MouseHoverZone _mouseHoverZone;

        public const int SceneWindth = 15;
        public const int SceneHeight = 9;
        public const int CellSize = 50;
        public const int InnerCellSize = CellSize - 4;
        public const int PixelHeight = SceneHeight * CellSize;
        public const int PixelWidth = SceneWindth * CellSize;

        private int _selectedCellX = -1;
        private int _selectedCellY = -1;

        public static Building[,] Buildings;



        public GameScene(MainForm form)
        {
            _mainForm = form;

            _mainForm.Shown += Form_Shown;
            _mainForm.MouseClick += Form_MouseClick;
            _mainForm.MainTimer.Tick += Timer_Tick;

            Buildings = new Building[SceneWindth, SceneHeight];     
            Buildings[3, 4] = new Building(); //test
            Buildings[2, 2] = new Building("extractor"); //test

            const int buttonsPanelHeight = 150;
            _mainForm.ClientSize = new Size(SceneWindth * CellSize, SceneHeight * CellSize + buttonsPanelHeight);
            _mouseHoverZone = new MouseHoverZone(_mainForm, 0, 0, SceneWindth * CellSize, SceneHeight * CellSize);
        }



        private void Form_Shown(object sender, EventArgs e)
        {
            DrawScene();
        }



        private void DrawScene()
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), 0, 0, SceneWindth * CellSize, SceneHeight * CellSize);

            // Draw black cells
            for (int y = 0; y < SceneHeight; y++)
            {
                for (int x = 0; x < SceneWindth; x++)
                {
                    const int bright = 12;
                    Color color = Color.FromArgb(bright, bright, bright);
                    MainForm.G.FillRectangle(new SolidBrush(color), x * CellSize, y * CellSize, InnerCellSize, InnerCellSize);
                }
            }

            // Draw mouse hover cell
            if (_mouseHoverZone.IsMouseHover())
            {
                var position = _mainForm.PointToClient(Cursor.Position);
                int hoverCellX = position.X / CellSize;
                int hoverCellY = position.Y / CellSize;
                if (Buildings[hoverCellX, hoverCellY] != null)
                {
                    const int bright = 120;
                    Color selectedItemColor = Color.FromArgb(bright, bright, bright);
                    MainForm.G.FillRectangle(new SolidBrush(selectedItemColor), hoverCellX * CellSize, hoverCellY * CellSize, InnerCellSize, InnerCellSize);
                }
            }

            // Draw buildings
            for (int y = 0; y < SceneHeight; y++)
            {
                for (int x = 0; x < SceneWindth; x++)
                {
                    if (Buildings[x, y] != null)
                    {
                        Buildings[x, y].DrawOnGrid(x, y);
                    }
                }
            }

        }



        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mouseHoverZone.IsMouseHover())
            {
                var position = _mainForm.PointToClient(Cursor.Position);
                int hoverCellX = position.X / CellSize;
                int hoverCellY = position.Y / CellSize;
                if (Buildings[hoverCellX, hoverCellY] != null)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        Buildings[hoverCellX, hoverCellY] = null;
                    }
                    else
                    {
                    Buildings[hoverCellX, hoverCellY].Name = "lol";
                    }
                }
                else
                {
                    Buildings[hoverCellX, hoverCellY] = new Building("test");
                }
                DrawScene();
            }
        }



        private void Timer_Tick (object sender, EventArgs e)
        {
            DrawScene();
        }
    }
}
