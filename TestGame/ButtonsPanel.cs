using System;
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
        private MouseHoverZone _mouseHoverZone;
        private const int _x = -1;
        private const int _y = GameScene.PixelHeight;
        public const int Height = 200;
        public const int Width = 700;
        public static String SelectedBuildingName = null;



        public ButtonsPanel (MainForm form, Game game, Timer timer)
        {
            _mainForm = form;
            _game = game;
            _mainForm.Shown += Form_Shown;
            _mouseHoverZone = new MouseHoverZone(_mainForm, _x, _y, Width, Height);
            var button1 = ButtonsInitialization(timer, "Extractor", 30, 50);
            var button2 = ButtonsInitialization(timer, "Converter", 150, 50);
            var button3 = ButtonsInitialization(timer, "Storage", 270, 50);

            _mainForm.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    SelectedBuildingName = null;
                }
            };
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
                var color = Color.FromArgb(40, 150, 40);
                MainForm.G.FillRectangle(new SolidBrush(color), buttonX, buttonY, width, height);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };

            var button = new Button(_mainForm, timer, _x + x, _y + y, buttonSize, buttonSize, drawNormal, drawHover, drawActive);


            button.Click += (s, e) =>
            {
                SelectedBuildingName = Capture;
            };

            return button;
        }



        private void DrawPanel (int mouseX = -1, int mouseY = -1)
        {
            // Draw background
            MainForm.G.FillRectangle(new SolidBrush(MainForm.BackgroundColor), _x, _y, Width, Height);

            Font font = new Font("Arial", 16);
            SolidBrush fontBrush = new SolidBrush(Color.White);
            String header = "Здания и преобразователи:";
            MainForm.G.DrawString(header, font, fontBrush, _x + 10, _y + 10);
        }



        private void Form_Shown (object sender, EventArgs e)
        {
            DrawPanel();
        }
    }
}
