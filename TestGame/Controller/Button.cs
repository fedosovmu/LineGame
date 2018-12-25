using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame
{
    class Button
    {
        public event EventHandler Click;
        public delegate void Draw(int x, int y, int width, int height);
        //public delegate void DrawNormal(int x, int y, int width, int height);
        //public delegate void DrawHover(int x, int y, int width, int height);
        //public delegate void DrawActive(int x, int y, int width, int height);
        private Draw _drawNormal;
        private Draw _drawHover;
        private Draw _drawActive;
        private MouseHoverZone _mouseHoverZone;
        private bool _isMouseHover = false;
        private bool _isButtonActive = false;
        private int _x;
        private int _y;
        private int _windth;
        private int _height;



        public Button (MainForm form, Timer timer, int x, int y, int width, int height, Draw drawNormal, Draw drawHover, Draw drawActive)
        {
            _mouseHoverZone = new MouseHoverZone(form, x, y, width, height);
            _x = x;
            _y = y;
            _windth = width;
            _height = height;
            _drawNormal = drawNormal;
            _drawHover = drawHover;
            _drawActive = drawActive;
            timer.Tick += Timer_Tick;
            form.MouseClick += Form_MouseClick;
            form.Shown += (s, e) => drawNormal(x, y, width, height);
        }



        public void Deactivate()
        {
            _isButtonActive = false;
            if (_mouseHoverZone.IsMouseHover())
            {
                _drawHover(_x, _y, _windth, _height);
            }
            else
            {
                _drawNormal(_x, _y, _windth, _height);
            }
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_mouseHoverZone.IsMouseHover())
            {
                if (_isMouseHover == false)
                {
                    _isMouseHover = true;
                    if (_isButtonActive == false)
                    {
                        _drawHover(_x, _y, _windth, _height);   
                    }                 
                }
            }
            else
            {
                if (_isMouseHover == true)
                {
                    _isMouseHover = false;
                    if (_isButtonActive == false)
                    {
                        _drawNormal(_x, _y, _windth, _height);
                    }
                }
            }
        }



        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mouseHoverZone.IsMouseHover())
            {
                _isButtonActive = true;
                _drawActive(_x, _y, _windth, _height);             
                Click(sender, e);           
            }              
        }
    }
}
