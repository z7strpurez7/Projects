using RPGHeroes.Exceptions;
using RPGHeroes.Exceptions.RPGHeroes.Exceptions;
using RPGHeroes.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPGHeroes.Enum;

namespace RPGHeroes
{
    abstract class Hero //Hero class defines hero
    {
        private int level;
        private string name;
        private string Myclass;
        private HeroAttribute heroAttributes;
        private HeroAttribute equipmentAttributes;
        private List<WeaponType> validWeaponTypes;
        private List<ArmorType> validArmorTypes;
        private Dictionary<Slot, Item.Item> equipment;

        public string MyClass { get; set; }
        public Dictionary<Slot, Item.Item> Equipment { get { return equipment; } set { equipment = value; } }
        public List<WeaponType> ValidWeaponTypes { get; set; }
        public List<ArmorType> ValidArmorTypes { get; set; }
        public HeroAttribute HeroAttributes { get => heroAttributes; set => heroAttributes = value; }
        public HeroAttribute EquipmentAttributes { get => equipmentAttributes; set => equipmentAttributes = value; }
        public string Name { get { return name; } set { name = value; } }
        public int Level { get => level; set => level = value; }

        public int LevelAttributes => HeroAttributes.Sum(heroAttributes);
        public int TotalAttributes => HeroAttributes.Sum(equipmentAttributes, heroAttributes); 

        public virtual void LevelUp() { Level++; }
        public virtual int DamagingAttribute { get; }
        public string Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " +Name);
            sb.AppendLine("Class: " + MyClass);
            sb.AppendLine("Level " + Level);
            sb.AppendLine("Attributes:"+"Strength: " + HeroAttributes.Strength + " | " + "Dexterity: " + HeroAttributes.Dexterity + " | " + "Intelligence: " + HeroAttributes.Intelligence);
            sb.AppendLine("Level Attributes: " + LevelAttributes);
            sb.AppendLine("Total Attributes: " + TotalAttributes);
            sb.AppendLine("CurrentDamage: " + Damage());
            sb.AppendLine("");
        

            return sb.ToString();
        }


        public void EquipArmor(Item.Item item)
        {

            try { 
            
            if (item.GetType() == typeof(Armor) && ValidArmorTypes.Contains(item.GetArmorType) && item.RequiredLevel <= Level)
            {
                if (Equipment[item.Slot] != null)
                {
                    EquipmentAttributes.AttributeLoss(Equipment[item.Slot].ArmorAttribute);
                }
                Equipment[item.Slot] = item;
                EquipmentAttributes.AttributeGain(Equipment[item.Slot].ArmorAttribute);
                Console.WriteLine("equipable");
            }
                else
                {
                    throw new InvalidArmorException("Invalid armor");
                }

            }
            catch (InvalidArmorException ex)
            {
                Console.WriteLine("Invalid Armor: " + ex.Message);
            }



        }
        public void EquipWeapon(Item.Item item)
        {
            try
            {
                if (item.GetType() == typeof(Weapon) && ValidWeaponTypes.Contains(item.GetWeaponType))
                {
                    if (ValidWeaponTypes.Contains(item.GetWeaponType) && item.RequiredLevel <= Level && item.GetType() == typeof(Weapon))
                    {
                        Equipment[item.Slot] = item;

                    }
                }
                else if (item.GetType() == typeof(Armor) || !ValidWeaponTypes.Contains(item.GetWeaponType))
                {
                    throw new InvalidWeaponException("Invalid weapon");
                }
            }
            catch (InvalidWeaponException ex)
            {
                Console.WriteLine("Invalid Weapon: " + ex.Message);
            }
        }


        public double Damage()
        {
            if (equipment[Slot.Weapon] == null)
            {
        
                return (1.0 * (1.0 + DamagingAttribute / 100.0));
            }
            else
        
            return Equipment[Slot.Weapon].WeaponDamage * (1.0 + DamagingAttribute / 100.0);

        }
        public Hero() //Default constructor
        {
            Level = 1;
            equipment = new Dictionary<Slot, Item.Item>();
            ValidWeaponTypes = new List<WeaponType>();
            ValidArmorTypes = new List<ArmorType>();
            equipmentAttributes = new HeroAttribute(0, 0, 0);
            Equipment[Enum.Slot.Head] = null;
            Equipment[Enum.Slot.Body] = null;
            Equipment[Enum.Slot.Legs] = null;
            Equipment[Enum.Slot.Weapon] = null;
        }
    }
}
