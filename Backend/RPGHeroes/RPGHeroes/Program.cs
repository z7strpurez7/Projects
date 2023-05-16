using RPGHeroes.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPGHeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero Mage1 = new Mage("Magix");
            //    Console.WriteLine("Name: " + Mage1.Name + " | " + "level: " + Mage1.Level);
            Item.Item clothArmor = new Armor("Body", 5, new HeroAttribute(1, 1, 50), Enum.ArmorType.Cloth, Enum.Slot.Body);
            Item.Item clothArmor2 = new Armor("Body", 1, new HeroAttribute(1, 1, 100), Enum.ArmorType.Cloth, Enum.Slot.Body);
            Item.Item weapon = new Weapon("Common wand", 1, 3, Enum.WeaponType.Wand);
            Console.WriteLine(Mage1.Display());

            Mage1.EquipArmor(clothArmor);
            Console.WriteLine(Mage1.Display());
            Mage1.EquipArmor(clothArmor2);
            Mage1.EquipWeapon(weapon);
            Console.WriteLine(Mage1.Display());

        }
    }
}
