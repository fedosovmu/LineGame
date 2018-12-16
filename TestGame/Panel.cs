using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    class Panel
    {
        MainForm _mainForm;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private bool _isHover = false;



        public Panel (MainForm form, int x, int y, int width, int height)
        {
            _mainForm = form;
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            _mainForm.Shown += Shown;
            _mainForm.MouseClick += Form_MouseClick;
            _mainForm.MouseMove += Form_MouseMove;
        }



        protected void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.X > _x && e.Y > _y) && (e.X < _x + _width && e.Y < _y + _height))
            {
                this.MouseClick(sender, e);
            }
        }

        protected void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.X > _x && e.Y > _y) && (e.X < _x + _width && e.Y < _y + _height))
            {
                this.MouseMove(sender, e);
                if (_isHover == false)
                {
                    _isHover = true;
                    this.MouseHover(sender, e);
                }
            }
            else
            {
                if (_isHover)
                {
                    _isHover = false;
                    this.MouseLeave(sender, e);
                }
            }
        }



        private void Shown(object sender, EventArgs e)
        {

        }


        private void MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void MouseMove(object sender, MouseEventArgs e)
        {

        }


        private void MouseHover(object sender, MouseEventArgs e)
        {

        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
