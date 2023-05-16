using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes
{
    class HeroAttribute
    {
        private int strength;
        private int dexterity;
        private int intelligence;

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public HeroAttribute LevelAttributes()
        {
            return new HeroAttribute(Strength, Dexterity, Intelligence);
        }
        public HeroAttribute Total(HeroAttribute equipment)
        {
            //  return Strength + Dexterity + Intelligence + Sum(equipment);
            return new HeroAttribute(Strength + equipment.Strength, Dexterity + equipment.Dexterity, Intelligence + equipment.Intelligence);
        }

        public int Sum(HeroAttribute x, HeroAttribute y)
        {
            return x.Strength + x.Dexterity + x.Intelligence + y.Strength + y.Dexterity + y.Intelligence;
        }
        public int Sum(HeroAttribute x)
        {
            return x.Strength + x.Dexterity + x.Intelligence;
        }

        public void AttributeGain(HeroAttribute gain)
        {
            Strength += gain.Strength;
            Dexterity += gain.Dexterity;
            Intelligence += gain.Intelligence;
        }
        public void AttributeGain(int strength, int dexterity, int intelligence)
        {
            Strength += strength;
            Dexterity += dexterity;
            Intelligence += intelligence;
        }
        public void AttributeLoss(HeroAttribute loss)
        {
            Strength -= loss.Strength;
            Dexterity -= loss.Dexterity;
            Intelligence -= loss.Intelligence;
        }
        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
        }
    }
}
