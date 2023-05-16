using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes
{
    class Ranger : Hero
    {
        public override void LevelUp()
        {
            Level++;
            HeroAttributes.AttributeGain(1, 5, 1);
        }

        public override int DamagingAttribute { get { return (HeroAttributes.Dexterity + EquipmentAttributes.Dexterity); } }
        public Ranger(string name)
        {
            Name = name;
            MyClass = "Ranger";
            HeroAttributes = new HeroAttribute(1, 7, 1);
            ValidArmorTypes.Add(Enum.ArmorType.Leather);
            ValidArmorTypes.Add(Enum.ArmorType.Mail);
            ValidWeaponTypes.Add(Enum.WeaponType.Bow);
        }
    }
}
