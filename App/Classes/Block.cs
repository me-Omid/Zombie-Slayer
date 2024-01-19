using KWEngine3.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GruppeC.App.Classes
{
    internal class Block : GameObject
    {
        public Block()
        {
            SetScale(10f, 0.1f, 10f);
            AddRotationY(90);
        }
        public override void Act()
        {
            
        }
    }
}
