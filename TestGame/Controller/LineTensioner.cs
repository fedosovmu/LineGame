using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineGame
{
    class LineTensioner
    {
        private Game _game;
        private GameSceneZone _gameSceneZone;
        public event Action<int, int> Selected;
        public event Action Deselected;
        public event Action<int, int, int, int> Connected;

        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;



        public LineTensioner(MainForm form, Game game, GameSceneZone gameSceneZone)
        {
            _game = game;
            _gameSceneZone = gameSceneZone;
            form.MouseDown += Form_MouseDown;
            form.MouseUp += Form_MouseUp;

            Connected += (fromX, fromY, toX, toY) =>
            {
                CellSelector.Deselect();
                if (gameSceneZone.IsMouseHover())
                {
                    var position = gameSceneZone.GetHoverCellCoordinate();
                    game.Connect(fromX, fromY, toX, toY);
                }
            };
        }



        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (_gameSceneZone.IsMouseHover() && e.Button == MouseButtons.Left)
            {
                var position =_gameSceneZone.GetHoverCellCoordinate();
                Select(position.Item1, position.Item2);
            }
        }



        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (_gameSceneZone.IsMouseHover() && e.Button == MouseButtons.Left)
            {
                var position = _gameSceneZone.GetHoverCellCoordinate();
                var x = position.Item1;
                var y = position.Item2;

                if (X != x || Y != y)
                {
                    CellSelector.Deselect();
                }

                if (_game.IsCellFree(X, Y) == false && _game.IsCellFree(x, y) == false && (X != x || Y != y))
                {
                    Connect(X, Y, x, y);
                }
                else
                {
                    Deselect();
                }
            }
        }



        private void Select(int x, int y)
        {
            X = x;
            Y = y;
            if (Selected != null)
                Selected(X, Y);
        }



        private void Deselect()
        {
            X = -1;
            Y = -1;
            if (Deselected != null)
                Deselected();
        }



        private void Connect(int fromX, int fromY, int toX, int toY)
        {
            X = -1;
            Y = -1;
            if (Connected != null)
                Connected(fromX, fromY, toX, toY);
        }



        public bool IsSelected()
        {
            return (X != -1 && Y != -1);
        }
    }
}
