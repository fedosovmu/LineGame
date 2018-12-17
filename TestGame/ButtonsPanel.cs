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
        private const int _x = 0;
        private const int _y = GameScene.PixelHeight;
        public const int Height = 200;
        public const int Width = 500;



        public ButtonsPanel (MainForm form, Game game)
        {
            _mainForm = form;
            _game = game;
            _mainForm.Shown += Form_Shown;
            _mouseHoverZone = new MouseHoverZone(_mainForm, _x, _y, Width, Height);
            ButtonsInitialization();
        }



        private void ButtonsInitialization()
        {
            int buttonX = _x + 30;
            int buttonY = _y + 50;
            const int buttonSize = 100;
            Building buttonBuilding = new Building("extractor");
            const int buildingIndent = (buttonSize - GameScene.CellSize) / 2 + 2;

            Button button = new Button(_mainForm, buttonX, buttonY, buttonSize, buttonSize);
            button.MouseLeave += (s, e) =>
            {
                MainForm.G.FillRectangle(new SolidBrush(GameScene.BlackCellColor), buttonX - 1, buttonY - 1, buttonSize + 2, buttonSize + 2);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };
            button.MouseHover += (s, e) =>
            {
                MainForm.G.FillRectangle(new SolidBrush(GameScene.SelectedCellColor), buttonX, buttonY, buttonSize, buttonSize);
                BuildingPainter.Draw(buttonBuilding, buttonX + buildingIndent, buttonY + buildingIndent);
            };
            button.MouseClick += (s, e) =>
            {
                MessageBox.Show("Button1 Click");
            };
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
