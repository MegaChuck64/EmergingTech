using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public class Collision
    {
        public GameScript colA;
        public GameScript colB;

        public GameScript GetOther(GameScript asker)
        {
            if (colA == asker) return colB;
            else if (colB == asker) return colA;
            else
            {
                Console.WriteLine("Can't find collision that matches this asker.");
                return null;
            }
        }
    }
}
