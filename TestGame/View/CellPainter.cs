using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LineGame
{
    static class CellPainter
    {
        public const int CellSize = 70;
        public const int InnerCellSize = CellSize - 4;

        public static Color NormalCellColor;
        public static Color HoverCellColor;
        public static Color GreenColor;
        public static Color RedColor;
        public static Color SelectedCellColor;


        static CellPainter ()
        {
            const int bright = 12;
            NormalCellColor = Color.FromArgb(bright, bright, bright);
            const int bright2 = 120;
            HoverCellColor = Color.FromArgb(bright2, bright2, bright2);
            GreenColor = Color.FromArgb(40, 150, 40);
            RedColor = Color.FromArgb(150, 40, 40);
            SelectedCellColor = Color.FromArgb(150, 150, 40);
        }



        public static void DrawOnGrid (Color color ,int x, int y)
        {
            MainForm.G.FillRectangle(new SolidBrush(color), GameScene.X + x * CellSize, GameScene.Y + y * CellSize, InnerCellSize, InnerCellSize);
        }
    }
}
