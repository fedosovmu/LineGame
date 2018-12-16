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
        private static GameScene instance;
        private Form _mainForm;

        public const int SceneWindth = 15;
        public const int SceneHeight = 9;
        public const int CellSize = 50;
        public const int InnerCellSize = CellSize - 4;
        public const int PixelHeight = SceneHeight * CellSize;
        public const int PixelWidth = SceneWindth * CellSize;

        private int _selectedCellX = -1;
        private int _selectedCellY = -1;

        public static Building[,] Buildings;



        public static GameScene getInstance(MainForm form)
        {
            if (instance == null)
                instance = new GameScene(form);
            return instance;
        }



        public GameScene(MainForm form)
        {
            _mainForm = form;

            _mainForm.Shown += Form_Shown;
            _mainForm.MouseClick += Form_MouseClick;
            _mainForm.MouseMove += Form_MouseMove;

            Buildings = new Building[SceneWindth, SceneHeight];     
            Buildings[3, 4] = new Building(); //test
            Buildings[2, 2] = new Building("extractor"); //test


            const int buttonsPanelHeight = 150;
            _mainForm.ClientSize = new Size(SceneWindth * CellSize, SceneHeight * CellSize + buttonsPanelHeight);
        }



        private void Form_Shown(object sender, EventArgs e)
        {
            DrawScene();
            _mainForm.Refresh();
        }



        private void Clear()
        {
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), 0, 0, SceneWindth * CellSize, SceneHeight * CellSize);
        }



        private void DrawScene(int mouseX = -1, int mouseY = -1)
        {
            this.Clear();

            for (int y = 0; y < SceneHeight; y++)
            {
                for (int x = 0; x < SceneWindth; x++)
                {
                    const int bright = 12;
                    Color color = Color.FromArgb(bright, bright, bright);
                    MainForm.G.FillRectangle(new SolidBrush(color), x * CellSize, y * CellSize, InnerCellSize, InnerCellSize);
                }
            }
            
            if ((mouseY < PixelHeight && mouseX < PixelWidth) && (mouseX >= 0 && mouseY >= 0))
            {
                int hoverCellX = mouseX / CellSize;
                int hoverCellY = mouseY / CellSize;
                if (Buildings[hoverCellX, hoverCellY] != null)
                {
                    const int bright = 120;
                    Color selectedItemColor = Color.FromArgb(bright, bright, bright);
                    MainForm.G.FillRectangle(new SolidBrush(selectedItemColor), hoverCellX * CellSize, hoverCellY * CellSize, InnerCellSize, InnerCellSize);
                }
            }

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
            DrawScene(e.X, e.Y);

            int x = e.X;
            int y = e.Y;

            if (e.Button.ToString() == "Left")
            {
                MainForm.G.FillRectangle(new SolidBrush(Color.Red), x - 10, y - 10, 20, 20);
            }
            else
            {
                MainForm.G.FillRectangle(new SolidBrush(Color.Green), x - 10, y - 10, 20, 20);
            }

            _mainForm.Refresh();
        }



        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            DrawScene(e.X, e.Y);
            _mainForm.Refresh();
        }
    }
}
