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
    }
}
