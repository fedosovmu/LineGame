using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestGame
{
    static class BuildingPainter
    {
        private static Pen _pen;
        private static Font _font;
        private static SolidBrush _fontBrush;
        private static StringFormat _stringFormat;

        static BuildingPainter ()
        {
            _pen = new Pen(new SolidBrush(Color.White), 2);

            _font = new Font("Arial", 8);
            _fontBrush = new SolidBrush(Color.Yellow);
            _stringFormat = new StringFormat();
            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;           
        }



        public static void DrawOnGrid(Building building, int x, int y)
        {
            Draw(building, x * GameScene.CellSize, y * GameScene.CellSize);
        }



        public static void Draw(Building building, int x, int y)
        {
            const int indent = 4;
            const int buildSize = GameScene.InnerCellSize - 2 * indent;
            MainForm.G.DrawRectangle(_pen, new Rectangle(x + indent, y + indent, buildSize, buildSize));


            const int textIndent = GameScene.CellSize * 47 / 100;
            int centerX = x + textIndent; // 70->32, 50->23
            int centerY = y + textIndent;
            String name = building.Name;
            const int strMaxLenght = GameScene.CellSize / 7;
            if (name.Length > strMaxLenght) //50 -> 7, 70 -> 11
            {
                name = name.Insert(strMaxLenght, "\n");
            }
            MainForm.G.DrawString(name, _font, _fontBrush, centerX, centerY, _stringFormat);
        }
    }
}
