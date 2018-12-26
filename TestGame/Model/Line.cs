using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestGame
{
    class Line
    {
        public Building Begin { get; private set; }
        public Building End { get; private set; }



        public Line (Building begin, Building end)
        {
            if (begin == end) throw new ArgumentException("The building cannot connect with itself");
            Begin = begin;
            End = end;
        }

        

        public bool IsIncident(Building building)
        {
            return Begin == building || End == building;
        }



        public Building OtherBuilding(Building building)
        {
            if (Begin == building) return End;
            else if (End == building) return Begin;
            else throw new ArgumentException ("Line not connected to this building");
        }
    }
}
