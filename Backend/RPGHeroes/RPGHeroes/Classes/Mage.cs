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

        public override void LevelUp()
        {
            Level++;
            HeroAttributes.AttributeGain(1, 1, 5);
        }

        public override int DamagingAttribute { get { return (HeroAttributes.Intelligence + EquipmentAttributes.Intelligence); } }


        public Mage(string name)
        {
            
            Name = name;
            MyClass = "Mage";
            HeroAttributes = new HeroAttribute(1, 1, 8);
            ValidArmorTypes.Add(Enum.ArmorType.Cloth);
            ValidWeaponTypes.Add(Enum.WeaponType.Staff);
            ValidWeaponTypes.Add(Enum.WeaponType.Wand);
        }
    }
}
