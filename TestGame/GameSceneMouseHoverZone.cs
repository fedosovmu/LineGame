using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    class GameSceneMouseHoverZone : MouseHoverZone
    {
        private MainForm _mainForm;
        public delegate void GameSceneClickEventHandler(object s, MouseEventArgs e, int hoverCellX, int hoverCellY);
        public event GameSceneClickEventHandler ClickOnCell;



        public GameSceneMouseHoverZone(MainForm form)
            : base(form, GameScene.X, GameScene.Y, GameScene.Width, GameScene.Height)
        {
            _mainForm = form;
            _mainForm.MouseClick += (s, e) =>
            {
                if (IsMouseHover())
                {
                    var coordinates = GetHoverCellCoordinate();                   
                    ClickOnCell(this, e, coordinates.Item1, coordinates.Item2);
                }
            };
        }



        public Tuple<int, int> GetHoverCellCoordinate()
        {
            var position = _mainForm.PointToClient(Cursor.Position);
            int hoverCellX = (position.X - GameScene.X) / GameScene.CellSize;
            int hoverCellY = (position.Y - GameScene.Y) / GameScene.CellSize;
            return new Tuple<int, int>(hoverCellX, hoverCellY);
        }

    }
}
