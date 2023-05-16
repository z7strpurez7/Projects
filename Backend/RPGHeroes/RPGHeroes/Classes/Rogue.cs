using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes
{
    class Rogue : Hero
    {
        public override void LevelUp()
        {
            Level++;
            HeroAttributes.AttributeGain(1, 4, 1);
        }
        public override int DamagingAttribute { get { return (HeroAttributes.Dexterity + EquipmentAttributes.Dexterity); } }
        public Rogue(string name)
        {
            Name = name;
            MyClass = "Rogue";
            HeroAttributes = new HeroAttribute(2, 6, 1);
            ValidArmorTypes.Add(Enum.ArmorType.Leather);
            ValidArmorTypes.Add(Enum.ArmorType.Mail);
            ValidWeaponTypes.Add(Enum.WeaponType.Dagger);
            ValidWeaponTypes.Add(Enum.WeaponType.Sword);
        }
    }
}
