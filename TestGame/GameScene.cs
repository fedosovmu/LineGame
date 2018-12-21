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
        private Game _game;
        private MouseHoverZone _mouseHoverZone;

        public const int CellSize = 70;
        public const int InnerCellSize = CellSize - 4;
        public const int PixelHeight = Game.SceneHeight * CellSize;
        public const int PixelWidth = Game.SceneWindth * CellSize;

        private int _selectedCellX = -1;
        private int _selectedCellY = -1;

        public static Color NormalCellColor;
        public static Color HoverCellColor;

        public delegate void GameSceneClickEventHandler(int x, int y);
        public event GameSceneClickEventHandler Click;



        public GameScene(MainForm form, Game game, Timer timer)
        {
            _mainForm = form;
            _game = game;

            _mainForm.Shown += Form_Shown;
            _mainForm.MouseClick += Form_MouseClick;
            timer.Tick += Timer_Tick;

            _mainForm.ClientSize = new Size(Game.SceneWindth * CellSize, Game.SceneHeight * CellSize + ButtonsPanel.Height);
            _mouseHoverZone = new MouseHoverZone(_mainForm, 0, 0, Game.SceneWindth * CellSize, Game.SceneHeight * CellSize);

            const int bright = 12;
            NormalCellColor = Color.FromArgb(bright, bright, bright);
            const int bright2 = 120;
            HoverCellColor = Color.FromArgb(bright2, bright2, bright2);
        }



        private void Form_Shown(object sender, EventArgs e)
        {
            DrawScene();
        }



        private void DrawScene()
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), 0, 0, Game.SceneWindth * CellSize, Game.SceneHeight * CellSize);

            // Draw black cells
            for (int y = 0; y < Game.SceneHeight; y++)
            {
                for (int x = 0; x < Game.SceneWindth; x++)
                {
                    MainForm.G.FillRectangle(new SolidBrush(NormalCellColor), x * CellSize, y * CellSize, InnerCellSize, InnerCellSize);
                }
            }

            // Draw mouse hover cell
            if (_mouseHoverZone.IsMouseHover())
            {
                var position = _mainForm.PointToClient(Cursor.Position);
                int hoverCellX = position.X / CellSize;
                int hoverCellY = position.Y / CellSize;
                int x = hoverCellX * CellSize;
                int y = hoverCellY * CellSize;

                if (_game.Buildings[hoverCellX, hoverCellY] != null && ButtonsPanel.SelectedBuildingName == null)
                {
                    MainForm.G.FillRectangle(new SolidBrush(HoverCellColor), x, y, InnerCellSize, InnerCellSize);
                }

                if (ButtonsPanel.SelectedBuildingName != null)
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] == null)
                    {
                        MainForm.G.FillRectangle(new SolidBrush(Color.FromArgb(40, 150, 40)), x, y, InnerCellSize, InnerCellSize);
                        BuildingPainter.DrawOnGrid(new Building(ButtonsPanel.SelectedBuildingName), hoverCellX, hoverCellY);
                    }
                    else
                    {
                        MainForm.G.FillRectangle(new SolidBrush(Color.FromArgb(150, 40, 40)), x, y, InnerCellSize, InnerCellSize);
                    }

                }
            }

            // Draw buildings
            for (int y = 0; y < Game.SceneHeight; y++)
            {
                for (int x = 0; x < Game.SceneWindth; x++)
                {
                    if (_game.Buildings[x, y] != null)
                    {
                        _game.Buildings[x, y].DrawOnGrid(x, y);
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
                if (_game.Buildings[hoverCellX, hoverCellY] == null)
                {
                    if (e.Button == MouseButtons.Left && ButtonsPanel.SelectedBuildingName != null)
                    {
                        _game.Buildings[hoverCellX, hoverCellY] = new Building(ButtonsPanel.SelectedBuildingName);
                        ButtonsPanel.DeactivateButtons();
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (ButtonsPanel.SelectedBuildingName == null)
                        {
                            MessageBox.Show(_game.Buildings[hoverCellX, hoverCellY].Name + " click");  
                        }
                        else
                        {
                            MessageBox.Show("Здесь нельзя строить");
                        }
                    }
                    else
                    {
                        if (ButtonsPanel.SelectedBuildingName == null)
                        {
                            _game.Buildings[hoverCellX, hoverCellY] = null;
                        }
                    }
                }
            }
        }



        private void Timer_Tick (object sender, EventArgs e)
        {
            DrawScene();
        }
    }
}
