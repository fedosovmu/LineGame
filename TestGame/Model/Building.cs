using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class Building
    {
        public readonly String Name;
        public int X { get; private set; }
        public int Y { get; private set; }



        public Building(String name = "build")
        {
            Name = name;
            X = -1;
            Y = -1;
        }



        public bool IsBuilded()
        {
            return X != -1 && Y != -1;
        }



        public void Build(int x, int y, Game game)
        {
            if (IsBuilded() == false)
            {
                X = x;
                Y = y;
                if (game.Buildings[X, Y] != this) throw new ArgumentException("Build error");
            }
            else
            {
                throw new ArgumentException("The building is already built");
            }
        }
    }
}
