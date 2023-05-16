
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPGHeroes.Enum;

namespace RPGHeroes.Item
{
    class Armor : Item
    {
        private HeroAttribute armorAttribute;
        private ArmorType type;

        public override HeroAttribute ArmorAttribute { get; set; }
        public ArmorType Type { get; set; }

        public override ArmorType GetArmorType { get => Type; }


        public Armor(String name, int requiredLevel, HeroAttribute attributes, ArmorType type, Slot bodySlot)
        {
            if (bodySlot == Slot.Weapon)
            {
                throw new ArgumentException("Invalid slot for armor.");
            }
            else
            {
                this.Slot = bodySlot;
            }
            this.Name = name;
            this.RequiredLevel = requiredLevel;
            this.ArmorAttribute = attributes;
            this.Type = type;


        }
    }
}
