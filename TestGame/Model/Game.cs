using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class Game
    {
        public const int SceneWidth = 17;
        public const int SceneHeight = 9;
        public readonly Building[,] Buildings;
        public readonly List<Line> Lines = new List<Line>();



        public Game ()
        {
            Buildings = new Building[SceneWidth, SceneHeight];
            // test
            var b1 = new Building(); 
            var b2 = new Building("extractor");
            Build(3, 4, b1);
            Build(2, 2, b2);
            Lines.Add(new Line(b1, b2));
        }



        public bool IsCellFree(int x, int y)
        {
            return Buildings[x, y] == null;
        }



        public void Build(int x, int y, String name)
        {
            var building = new Building(name);
            Build(x, y, building);
        }



        public void Build(int x, int y, Building building)
        {
            if (IsCellFree(x, y))
            {               
                Buildings[x, y] = building;
                building.Build(x, y, this);
            }
            else
            {
                throw new ArgumentException("There is already a building in this place");
            }
        }
    }
}
