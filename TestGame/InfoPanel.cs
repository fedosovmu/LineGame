﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestGame
{
    class InfoPanel
    {
        private MainForm _mainForm;
        private Game _game;
        private MouseHoverZone _mouseHoverZone;
        private const int _x = ButtonsPanel.Width - 2;
        private const int _y = GameScene.PixelHeight;
        public const int Height = ButtonsPanel.Height;
        public const int Width = GameScene.PixelWidth - ButtonsPanel.Width + 2;



        public InfoPanel(MainForm form, Game game)
        {
            _mainForm = form;
            _game = game;
            _mouseHoverZone = new MouseHoverZone(_mainForm, _x, _y, Width, Height);
            _mainForm.Shown += Form_Shown;
            _mainForm.MainTimer.Tick += (s, e) => DrawPanel();

        }



        private void DrawPanel()
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), _x, _y, Width, Height);

            Font font = new Font("Arial", 16);
            SolidBrush fontBrush = new SolidBrush(Color.White);
            String header = "Информация:";
            MainForm.G.DrawString(header, font, fontBrush, _x + 10, _y + 10);


            // Draw building
            const int wiondowSize = 130;
            int point = GameScene.CellSize * 2 - 31;
            MainForm.G.DrawImage(MainForm.Btm, _x + 16, _y + 40, new Rectangle(point, point, wiondowSize, wiondowSize), GraphicsUnit.Pixel);

            // Draw building window      
            var pen = new Pen(new SolidBrush(Color.White), 2);
            MainForm.G.DrawRectangle(pen, new Rectangle(_x + 16, _y + 40, wiondowSize, wiondowSize));
        }



        private void Form_Shown(object sender, EventArgs e)
        {
            DrawPanel();
        }
    }
}
