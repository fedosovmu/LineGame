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
        private GameSceneZone _gameSceneZone;

        public const int X = 0;
        public const int Y = 0;
        public const int Width = Game.SceneWidth * CellPainter.CellSize;
        public const int Height = Game.SceneHeight * CellPainter.CellSize;

            

        public GameScene(MainForm form, Game game, Timer timer)
        {
            _mainForm = form;
            _game = game;
            _gameSceneZone = new GameSceneZone(_mainForm);

            _mainForm.Shown += (s, e) => DrawScene();
            _gameSceneZone.Click += GameSceneClick;
            timer.Tick += (s, e) => DrawScene();

            _mainForm.ClientSize = new Size(GameScene.Width, GameScene.Height + ButtonsPanel.Height);
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
                    CellPainter.DrawOnGrid(CellPainter.NormalCellColor, x, y);
                }
            }

            // Draw hover cell
            if (_gameSceneZone.IsMouseHover())
            {
                var coordinates = _gameSceneZone.GetHoverCellCoordinate();
                int hoverCellX = coordinates.Item1;
                int hoverCellY = coordinates.Item2;

                if (_game.Buildings[hoverCellX, hoverCellY] != null && ButtonsSelector.ButtonName == null)
                {
                    CellPainter.DrawOnGrid(CellPainter.HoverCellColor, hoverCellX, hoverCellY);
                }

                if (ButtonsSelector.ButtonName != null)
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] == null)
                    {
                        CellPainter.DrawOnGrid(CellPainter.GreenColor, hoverCellX, hoverCellY);
                        BuildingPainter.DrawOnGrid(new Building(ButtonsSelector.ButtonName), hoverCellX, hoverCellY);
                    }
                    else
                    {
                        CellPainter.DrawOnGrid(CellPainter.RedColor, hoverCellX, hoverCellY);
                    }

                }
            }

            // Draw selected cell
            if (CellSelector.IsCellSelected())
            {
                CellPainter.DrawOnGrid(CellPainter.SelectedCellColor, CellSelector.X, CellSelector.Y);
            }

            // Draw buildings
            for (int y = 0; y < Game.SceneHeight; y++)
            {
                for (int x = 0; x < Game.SceneWidth; x++)
                {
                    if (_game.Buildings[x, y] != null)
                    {
                        BuildingPainter.DrawOnGrid(_game.Buildings[x, y], x, y);
                    }
                }
            }

        }



        private void GameSceneClick(object s, GameSceneZoneEventArgs e)
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
