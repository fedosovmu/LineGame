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



        public static void DrawOnGrid(int beginX, int beginY, int endX, int endY)
        {
            var beginCenterX = beginX * CellPainter.CellSize + CellPainter.CellSize / 2;
            var beginCenterY = beginY * CellPainter.CellSize + CellPainter.CellSize / 2;
            var endCenterX = endX * CellPainter.CellSize + CellPainter.CellSize / 2;
            var endCenterY = endY * CellPainter.CellSize + CellPainter.CellSize / 2;
            MainForm.G.DrawLine(new Pen(CellPainter.GreenColor, 2), new Point(beginCenterX, beginCenterY), new Point(endCenterX, endCenterY));

            //Draw line from edge to edge
            //var beginEdgeX = (endCenterX * 5 + beginCenterX) / 6;
            //var beginEdgeY = (endCenterY * 5 + beginCenterY) / 6;
            //var endEdgeX = (endCenterX + beginCenterX * 5) / 6;
            //var endEdgeY = (endCenterY + beginCenterY * 5) / 6;

            //MainForm.G.DrawLine(new Pen(Color.FromArgb(0,0,200), 2), new Point(beginEdgeX, beginEdgeY), new Point(endEdgeX, endEdgeY));

            var relativeEndX = endCenterX - beginCenterX;
            var relativeEndY = endCenterY - beginCenterY;

            bool UpRight = relativeEndX > relativeEndY;
            bool UpLeft = -1 * relativeEndY > relativeEndX;

            int indentX = 0;
            int indentY = 0;
            int indent = 30;

            if (UpRight && UpLeft) indentY = -1 * indent; // UP
            else if (UpRight && !UpLeft) indentX = indent; // Right
            else if (!UpRight && UpLeft) indentX = -1 * indent; // Left
            else if (!UpRight && !UpLeft) indentY = indent; // Down


            var beginEdgeX = beginCenterX + indentX;
            var beginEdgeY = beginCenterY + indentY;
            var endEdgeX = endCenterX - indentX - 3;
            var endEdgeY = endCenterY - indentY - 3;


            var size = 3;
            MainForm.G.FillEllipse(new SolidBrush(Color.FromArgb(200, 0, 0)), new Rectangle(beginEdgeX - size, beginEdgeY - size, size * 2, size * 2));
            MainForm.G.FillEllipse(new SolidBrush(Color.FromArgb(200, 0, 0)), new Rectangle(endEdgeX - size, endEdgeY - size, size * 2, size * 2));

            

            // Draw Ending
            //var endMarkerX = (endCenterX * 5 + beginCenterX) / 6;
            //var endMarkerY = (endCenterY * 5 + beginCenterY) / 6;
            //var size = 3;
            //MainForm.G.FillEllipse(new SolidBrush(Color.FromArgb(200, 0, 0)), new Rectangle(endCenterX - size, endCenterY - size, size * 2, size * 2));

        }

    }
}
