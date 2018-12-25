using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    class LineTensioner
    {
        private Game _game;
        private GameSceneZone _gameSceneZone;
        public event Action<int, int> Selected;
        public event Action Deselected;

        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;



        public LineTensioner(MainForm form, Game game, GameSceneZone gameSceneZone)
        {
            _game = game;
            _gameSceneZone = gameSceneZone;
            form.MouseDown += Form_MouseDown;
            form.MouseUp += Form_MouseUp;

            Deselect();
        }



        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (_gameSceneZone.IsMouseHover())
            {
                var position =_gameSceneZone.GetHoverCellCoordinate();
                Select(position.Item1, position.Item2);
            }
        }



        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (_gameSceneZone.IsMouseHover())
            {
                var position = _gameSceneZone.GetHoverCellCoordinate();
                Deselect();
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



        public bool IsSelected()
        {
            return (X != -1 && Y != -1);
        }
    }
}
