using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes
{
    class Warrior : Hero
    {
        public override void LevelUp()
        {
            Level++;
            HeroAttributes.AttributeGain(3, 2, 1);
        }
        public override int DamagingAttribute { get { return (HeroAttributes.Strength + EquipmentAttributes.Strength); } }
        public Warrior(string name)
        {
            Name = name;
            MyClass = "Warrior";
            HeroAttributes = new HeroAttribute(5, 2, 1);
            ValidArmorTypes.Add(Enum.ArmorType.Plate);
            ValidArmorTypes.Add(Enum.ArmorType.Mail);
            ValidWeaponTypes.Add(Enum.WeaponType.Hammer);
            ValidWeaponTypes.Add(Enum.WeaponType.Sword);
            ValidWeaponTypes.Add(Enum.WeaponType.Axe);
        }
    }
}