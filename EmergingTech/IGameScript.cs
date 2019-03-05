using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public interface IGameScript
    {
        void Start();
        void Update(float dt);
    }
}
