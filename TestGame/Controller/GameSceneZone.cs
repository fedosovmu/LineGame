using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineGame
{
    class GameSceneZoneEventArgs : EventArgs
    {
        public MouseButtons Button;
        public int HoverCellX;
        public int HoverCellY;

        public GameSceneZoneEventArgs (MouseButtons button, int hoverCellX, int hoverCellY)
        {
            Button = button;
            HoverCellX = hoverCellX;
            HoverCellY = hoverCellY;
        }
    }

    delegate void GameSceneZoneEventHandler(object s, GameSceneZoneEventArgs e);



    class GameSceneZone : MouseHoverZone
    {
        private MainForm _mainForm;       
        public event GameSceneZoneEventHandler Click;



        public GameSceneZone(MainForm form)
            : base(form, GameScene.X, GameScene.Y, GameScene.Width, GameScene.Height)
        {
            _mainForm = form;
            _mainForm.MouseDown += (s, e) =>
            {
                if (IsMouseHover())
                {
                    var coordinates = GetHoverCellCoordinate();                   
                    Click(this, new GameSceneZoneEventArgs (e.Button, coordinates.Item1, coordinates.Item2));
                }
            };
        }



        public Tuple<int, int> GetHoverCellCoordinate()
        {
            var position = _mainForm.PointToClient(Cursor.Position);
            int hoverCellX = (position.X - GameScene.X) / CellPainter.CellSize;
            int hoverCellY = (position.Y - GameScene.Y) / CellPainter.CellSize;
            return new Tuple<int, int>(hoverCellX, hoverCellY);
        }

    }
}
