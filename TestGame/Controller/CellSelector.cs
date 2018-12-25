using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    public static class CellSelector
    {
        public static event Action<int, int> Selected;
        public static event Action Deselected;

        public static int X { get; private set; } = -1;
        public static int Y { get; private set; } = -1;



        static CellSelector ()
        {
            Deselect();
        }



        public static void Select(int x, int y)
        {
            X = x;
            Y = y;
            if (Selected != null)
                Selected(X, Y);
        }



        public static void Deselect()
        {
            X = -1;
            Y = -1;
            if (Deselected != null)
                Deselected();
        }



        public static bool IsCellSelected ()
        {
            return (X != -1 && Y != -1);
        }
    }
}
