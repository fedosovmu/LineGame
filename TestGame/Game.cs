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
        public Building[,] Buildings;



        public Game ()
        {
            Buildings = new Building[SceneWidth, SceneHeight];
            Buildings[3, 4] = new Building(); //test
            Buildings[2, 2] = new Building("extractor"); //test
        }
    }
}
