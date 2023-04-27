using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes
{
    //Internal?
    class Mage : Hero   //Inherit
    {
     
        int strength;

        public void LevelUp()
        {
            Level++;
            Strength += 1;
            Detexterity += 1;
            Intelligence += 5;
        }
        public Mage(string name)
        {
            Name = name;
            Strength = 1;
            Detexterity = 1;
            Intelligence = 1;

        }
    }
}
