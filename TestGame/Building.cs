using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestGame
{
    class Building
    {
        public String Name;
        private Pen _pen;

        public Building(String name = "build")
        {
            Name = name;
            _pen = new Pen(new SolidBrush(Color.White), 2);
        }



        public void DrawOnGrid(int x, int y)
        {
            Draw(x * GameScene.CellSize, y * GameScene.CellSize);
        }
        


        public void Draw (int x, int y)
        {
            int indent = 4;
            int buildSize = GameScene.InnerCellSize - 2 * indent;
            MainForm.G.DrawRectangle(_pen, new Rectangle(x + indent, y + indent, buildSize, buildSize));



            Font font = new Font("Arial", 8);
            SolidBrush fontBrush = new SolidBrush(Color.Yellow);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            int centerX = x + 23;
            int centerY = y + 23;
            String name = Name;
            if (Name.Length > 7)
            {
                name = Name.Insert(7,"\n");
            }
            MainForm.G.DrawString(name, font, fontBrush, centerX, centerY, stringFormat);
        }
    }
}
