using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace LineGame
{
    class GameScene
    {
        private Game _game;
        private GameSceneZone _gameSceneZone;
        private LineTensioner _lineTensioner;

        public const int X = 0;
        public const int Y = 0;
        public const int Width = Game.SceneWidth * CellPainter.CellSize;
        public const int Height = Game.SceneHeight * CellPainter.CellSize;

            

        public GameScene(MainForm form, Game game, Timer timer)
        {
            _game = game;
            _gameSceneZone = new GameSceneZone(form);
            _lineTensioner = new LineTensioner(form, game, _gameSceneZone);

            form.Shown += (s, e) => DrawScene();
            _gameSceneZone.Click += GameSceneClick;
            timer.Tick += (s, e) => DrawScene();

            form.ClientSize = new Size(GameScene.Width, GameScene.Height + ButtonsPanel.Height);
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

                if (_game.IsCellFree(hoverCellX, hoverCellY) == false && ButtonsSelector.IsSelected() == false)
                {
                    CellPainter.DrawOnGrid(CellPainter.HoverCellColor, hoverCellX, hoverCellY);
                }

                if (ButtonsSelector.IsSelected())
                {
                    if (_game.IsCellFree(hoverCellX, hoverCellY))
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

            // Draw tensioning line
            if (_gameSceneZone.IsMouseHover() && _lineTensioner.IsSelected())
            {
                var position = _gameSceneZone.GetHoverCellCoordinate();
                var x = position.Item1;
                var y = position.Item2;
                
                if (_game.IsCellFree(_lineTensioner.X, _lineTensioner.Y) == false && (x != _lineTensioner.X || y != _lineTensioner.Y))
                {
                    CellPainter.DrawOnGrid(CellPainter.GreenColor, _lineTensioner.X, _lineTensioner.Y);
                    CellPainter.DrawOnGrid(CellPainter.GreenColor, x, y);
                    LinePainter.DrawOnGrid(_lineTensioner.X, _lineTensioner.Y, x, y);
                }
            }

            // Draw Lines
            foreach (var line in _game.Lines)
            {
                LinePainter.DrawOnGrid(line.Begin.X, line.Begin.Y, line.End.X, line.End.Y);
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
                if (ButtonsSelector.IsSelected()) // Выбрано здание
                {
                    if (_game.IsCellFree(e.HoverCellX, e.HoverCellY)) // есть здание
                    {
                        _game.Build(e.HoverCellX, e.HoverCellY, ButtonsSelector.ButtonName);
                        ButtonsSelector.Deselect();
                    }
                    else
                    {
                        MessageBox.Show("Здесь нельзя строить");
                    }
                }
                else // Наводим пустой курсор
                {
                    if (_game.IsCellFree(e.HoverCellX, e.HoverCellY) == false) // есть здание
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
