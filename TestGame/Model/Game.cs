using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // <-- MessageBox

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
            var b2 = new Building("Extractor");
            var b3 = new Building("Converter");
            var b4 = new Building("Storage");
            var b5 = new Building("Turret");
            Build(3, 4, b1);
            Build(2, 2, b2);
            Build(6, 4, b3);
            Build(7, 1, b4);
            Build(8, 6, b5);

            Lines.Add(new Line(b1, b2));
        }



        public void Connect(int beginX, int beginY, int endX, int endY)
        {
            if (IsCellFree(beginX, beginY) == false && IsCellFree(endX, endY) == false)
            {
                var begin = Buildings[beginX, beginY];
                var end = Buildings[endX, endY];
                if (IsBuildingsConnected(begin, end) == false)
                {
                    Lines.Add(new Line(begin, end));
                }
                else
                {
                    MessageBox.Show("Здания " + begin.Name + "(" + begin.X + "," + beginY + ") - " + end.Name + "(" + end.X + "," + end.Y + ") уже соеденены.");
                }
            }
        }



        public bool IsBuildingsConnected (Building begin, Building end)
        {
            foreach (var line in Lines)
            {
                if (line.IsIncident(begin) && line.IsIncident(end))
                    return true;
            }
            return false;
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
