using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineGame
{
    public class MouseHoverZone
    {
        MainForm _mainForm;
        private int _x;
        private int _y;
        private int _width;
        private int _height;



        public MouseHoverZone(MainForm form, int x, int y, int width, int height)
        {
            _mainForm = form;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }



        public bool IsMouseHover ()
        {
            var mousePosition = _mainForm.PointToClient(Cursor.Position);
            return ((mousePosition.X > _x && mousePosition.Y > _y) && (mousePosition.X < _x + _width && mousePosition.Y < _y + _height));
        }
    }
}
