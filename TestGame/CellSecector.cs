using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class CellSecector
    {
        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;



        public CellSecector ()
        {
            Deselect();
        }



        public void Select(int x, int y)
        {
            X = x;
            Y = y;
        }



        public void Deselect()
        {
            X = -1;
            Y = -1;
        }



        public bool IsCellSelected ()
        {
            return (X != -1 && Y != -1);
        }
    }
}
