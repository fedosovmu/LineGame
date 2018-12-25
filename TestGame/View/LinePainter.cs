using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestGame
{
    static class LinePainter
    {
        static LinePainter()
        {

        }



        public static void DrawOnGrid(int fromX, int fromY, int toX, int toY)
        {
            var fromCenterX = fromX * CellPainter.CellSize + CellPainter.CellSize / 2;
            var fromCenterY = fromY * CellPainter.CellSize + CellPainter.CellSize / 2;
            var toCenterX = toX * CellPainter.CellSize + CellPainter.CellSize / 2;
            var toCenterY = toY * CellPainter.CellSize + CellPainter.CellSize / 2;
            MainForm.G.DrawLine(new Pen(CellPainter.GreenColor, 2), new Point(fromCenterX, fromCenterY), new Point(toCenterX, toCenterY));
        }

    }
}
