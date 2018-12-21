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
        private ButtonsPanel _buttonsPanel;
        private GameSceneMouseHoverZone _gameSceneMouseHoverZone;

        public const int X = 0;
        public const int Y = 0;
        public const int CellSize = 70;
        public const int InnerCellSize = CellSize - 4;
        public const int Width = Game.SceneWidth * CellSize;
        public const int Height = Game.SceneHeight * CellSize;

        public static CellSecector CellSecector; // <- костыль

        public static Color NormalCellColor;
        public static Color HoverCellColor;
        public static Color GreenColor;
        public static Color RedColor;
        public static Color SelectedCellColor;

            

        public GameScene(MainForm form, Game game, Timer timer, ButtonsPanel buttonsPanel)
        {
            _mainForm = form;
            _game = game;
            _buttonsPanel = buttonsPanel;
            _gameSceneMouseHoverZone = new GameSceneMouseHoverZone(_mainForm);

            _mainForm.Shown += (s, e) => DrawScene();
            _gameSceneMouseHoverZone.ClickOnCell += ClickOnCell;
            timer.Tick += (s, e) => DrawScene();

            _mainForm.ClientSize = new Size(GameScene.Width, GameScene.Height + ButtonsPanel.Height);

            CellSecector = new CellSecector();

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

                if (_game.Buildings[hoverCellX, hoverCellY] != null && _buttonsPanel.SelectedButtonName == null)
                {
                    MainForm.G.FillRectangle(new SolidBrush(HoverCellColor), posX, posY, InnerCellSize, InnerCellSize);
                }

                if (_buttonsPanel.SelectedButtonName != null)
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] == null)
                    {
                        MainForm.G.FillRectangle(new SolidBrush(GreenColor), posX, posY, InnerCellSize, InnerCellSize);
                        BuildingPainter.DrawOnGrid(new Building(_buttonsPanel.SelectedButtonName), hoverCellX, hoverCellY);
                    }
                    else
                    {
                        MainForm.G.FillRectangle(new SolidBrush(RedColor), posX, posY, InnerCellSize, InnerCellSize);
                    }

                }
            }

            // Draw selected cell
            if (CellSecector.IsCellSelected())
            {
                int posX = CellSecector.X * CellSize;
                int posY = CellSecector.Y * CellSize;

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



        private void ClickOnCell(object s, MouseEventArgs e, int hoverCellX, int hoverCellY)
        {
            if (e.Button == MouseButtons.Left) // Левый клик
            {
                if (_buttonsPanel.SelectedButtonName != null) // Выбрано здание
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] == null) // есть здание
                    {
                        _game.Buildings[hoverCellX, hoverCellY] = new Building(_buttonsPanel.SelectedButtonName);
                        _buttonsPanel.DeactivateButtons();
                    }
                    else
                    {
                        MessageBox.Show("Здесь нельзя строить");
                    }
                }
                else // Наводим пустой курсор
                {
                    if (_game.Buildings[hoverCellX, hoverCellY] != null) // есть здание
                    {
                        CellSecector.Select(hoverCellX, hoverCellY);
                    }
                    else
                    {
                        CellSecector.Deselect();
                    }
                }
            }
            
            if (e.Button == MouseButtons.Right) // Правый клик
            {
                _buttonsPanel.DeactivateButtons();
                CellSecector.Deselect();
            }
        }
    }
}
