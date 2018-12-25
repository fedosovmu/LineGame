using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace TestGame
{
    class GameScene
    {
        private MainForm _mainForm;
        private Game _game;
        private GameSceneZone _gameSceneMouseHoverZone;

        public const int X = 0;
        public const int Y = 0;
        public const int CellSize = 70;
        public const int InnerCellSize = CellSize - 4;
        public const int Width = Game.SceneWidth * CellSize;
        public const int Height = Game.SceneHeight * CellSize;

        public static Color NormalCellColor;
        public static Color HoverCellColor;
        public static Color GreenColor;
        public static Color RedColor;
        public static Color SelectedCellColor;

            

        public GameScene(MainForm form, Game game, Timer timer)
        {
            _mainForm = form;
            _game = game;
            _gameSceneMouseHoverZone = new GameSceneZone(_mainForm);

            _mainForm.Shown += (s, e) => DrawScene();
            _gameSceneMouseHoverZone.Click += Click;
            timer.Tick += (s, e) => DrawScene();

            _mainForm.ClientSize = new Size(GameScene.Width, GameScene.Height + ButtonsPanel.Height);

            const int bright = 12;
            NormalCellColor = Color.FromArgb(bright, bright, bright);
            const int bright2 = 120;
            HoverCellColor = Color.FromArgb(bright2, bright2, bright2);
            GreenColor = Color.FromArgb(40, 150, 40);
            RedColor = Color.FromArgb(150, 40, 40);
            SelectedCellColor = Color.FromArgb(150, 150, 40);
        }  



        private void DrawScene()
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), X, Y, GameScene.Width, GameScene.Height);

            // Draw normal cells
            for (int y = 0; y < Game.SceneHeight; y++)
            {
                for (int x = 0; x < Game.SceneWidth; x++)
                {
                    MainForm.G.FillRectangle(new SolidBrush(NormalCellColor), x * CellSize, y * CellSize, InnerCellSize, InnerCellSize);
                }
            }

            // Draw hover cell
            if (_gameSceneMouseHoverZone.IsMouseHover())
            {
                var coordinates = _gameSceneMouseHoverZone.GetHoverCellCoordinate();
                int hoverCellX = coordinates.Item1;
                int hoverCellY = coordinates.Item2;

                int posX = hoverCellX * CellSize;
                int posY = hoverCellY * CellSize;

                if (_game.Buildings[hoverCellX, hoverCellY] != null && ButtonsSelector.ButtonName == null)
                {
                    MainForm.G.FillRectangle(new SolidBrush(HoverCellColor), posX, posY, InnerCellSize, InnerCellSize);
                }

                if (ButtonsSelector.ButtonName != null)
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] == null)
                    {
                        MainForm.G.FillRectangle(new SolidBrush(GreenColor), posX, posY, InnerCellSize, InnerCellSize);
                        BuildingPainter.DrawOnGrid(new Building(ButtonsSelector.ButtonName), hoverCellX, hoverCellY);
                    }
                    else
                    {
                        MainForm.G.FillRectangle(new SolidBrush(RedColor), posX, posY, InnerCellSize, InnerCellSize);
                    }

                }
            }

            // Draw selected cell
            if (CellSelector.IsCellSelected())
            {
                int posX = CellSelector.X * CellSize;
                int posY = CellSelector.Y * CellSize;

                MainForm.G.FillRectangle(new SolidBrush(SelectedCellColor), posX, posY, InnerCellSize, InnerCellSize);
            }

            // Draw buildings
            for (int y = 0; y < Game.SceneHeight; y++)
            {
                for (int x = 0; x < Game.SceneWidth; x++)
                {
                    if (_game.Buildings[x, y] != null)
                    {
                        _game.Buildings[x, y].DrawOnGrid(x, y);
                    }
                }
            }

        }



        private void Click(object s, GameSceneZoneEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Левый клик
            {
                if (ButtonsSelector.ButtonName != null) // Выбрано здание
                {
                    if (_game.Buildings[e.HoverCellX, e.HoverCellY] == null) // есть здание
                    {
                        _game.Buildings[e.HoverCellX, e.HoverCellY] = new Building(ButtonsSelector.ButtonName);
                        ButtonsSelector.Deselect();
                    }
                    else
                    {
                        MessageBox.Show("Здесь нельзя строить");
                    }
                }
                else // Наводим пустой курсор
                {
                    if (_game.Buildings[e.HoverCellX, e.HoverCellY] != null) // есть здание
                    {
                        CellSelector.Select(e.HoverCellX, e.HoverCellY);
                    }
                    else
                    {
                        CellSelector.Deselect();
                    }
                }
            }
            
            if (e.Button == MouseButtons.Right) // Правый клик
            {
                CellSelector.Deselect();
            }
        }
    }
}
