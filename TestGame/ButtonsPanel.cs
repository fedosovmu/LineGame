﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestGame
{
    class ButtonsPanel
    {
        private MainForm _mainForm;
        private Game _game;
        public const int X = -1;
        public const int Y = GameScene.Height;
        public const int Height = 200;
        public const int Width = 700;
        public String SelectedButtonName { get; private set; }
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Button _button4;



        public ButtonsPanel(MainForm form, Game game, Timer timer)
        {
            SelectedButtonName = null;
            _mainForm = form;
            _game = game;
            _mainForm.Shown += Form_Shown;
            _button1 = ButtonsInitialization(timer, "Extractor", 30, 50);
            _button2 = ButtonsInitialization(timer, "Converter", 150, 50);
            _button3 = ButtonsInitialization(timer, "Storage", 270, 50);
            _button4 = ButtonsInitialization(timer, "LoL", 390, 50);

            _mainForm.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {                    
                    DeactivateButtons();
                }
            };
        }



        public void DeactivateButtons()
        {
            SelectedButtonName = null;
            _button1.Deactivate();
            _button2.Deactivate();
            _button3.Deactivate();
            _button4.Deactivate();
        }



        private Button ButtonsInitialization(Timer timer, String Capture, int x, int y)
        {
            const int buttonSize = 100;
            Building buttonBuilding = new Building(Capture);
            const int buildingIndent = (buttonSize - GameScene.CellSize) / 2 + 2;

            Button.DrawNormal drawNormal = (buttonX, buttonY, width, height) =>
            {
                var color = GameScene.NormalCellColor;
                MainForm.G.FillRectangle(new SolidBrush(color), buttonX - 1, buttonY - 1, width + 2, height + 2);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };

            Button.DrawHover drawHover = (buttonX, buttonY, width, height) =>
            {
                var color = GameScene.HoverCellColor;
                MainForm.G.FillRectangle(new SolidBrush(color), buttonX, buttonY, width, height);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };

            Button.DrawActive drawActive = (buttonX, buttonY, width, height) =>
            {
                var color = GameScene.GreenColor;
                MainForm.G.FillRectangle(new SolidBrush(color), buttonX, buttonY, width, height);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };

            var button = new Button(_mainForm, timer, X + x, Y + y, buttonSize, buttonSize, drawNormal, drawHover, drawActive);


            button.Click += (s, e) =>
            {
                SelectedButtonName = Capture;
                GameScene.CellSecector.Deselect(); // <- костыль
            };

            return button;
        }



        private void DrawPanel (int mouseX = -1, int mouseY = -1)
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), X, Y, Width, Height);

            Font font = new Font("Arial", 16);
            SolidBrush fontBrush = new SolidBrush(Color.White);
            String header = "Здания и преобразователи:";
            MainForm.G.DrawString(header, font, fontBrush, X + 10, Y + 10);
        }



        private void Form_Shown (object sender, EventArgs e)
        {
            DrawPanel();
        }
    }
}
