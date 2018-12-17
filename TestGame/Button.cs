using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    class Button : MouseHoverZone
    {
        public event MouseEventHandler MouseClick;
        public event EventHandler MouseHover;
        public event EventHandler MouseLeave;
        private bool _isMouseWasHover = false;



        public Button (MainForm form, Timer timer, int x, int y, int width, int height)
            : base (form, x, y, width, height)
        {
            timer.Tick += Timer_Tick;
            form.MouseClick += Form_MouseClick;
            form.Shown += (s, e) => MouseLeave(s, e);
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsMouseHover())
            {
                if (_isMouseWasHover == false)
                {
                    _isMouseWasHover = true;
                    if (MouseHover != null)
                    {
                        MouseHover(sender, e);
                    }
                }
            }
            else
            {
                if (_isMouseWasHover == true)
                {
                    _isMouseWasHover = false;
                    if (MouseLeave != null)
                    {
                        MouseLeave(sender, e);
                    }
                }
            }
        }



        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsMouseHover())
            {
                if (MouseClick != null)
                {
                    MouseClick(sender, e);
                }
            }              
        }
    }
}
