
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPGHeroes.Enum;

namespace RPGHeroes.Item
{
    abstract class Item
    {
        private string name;
        private int requiredLevel;
        private Slot slot;
        public string Name { get; set; }
        public int RequiredLevel { get; set; }

        public virtual HeroAttribute ArmorAttribute { get; set; }
        public virtual int WeaponDamage { get; set; }
        public virtual ArmorType GetArmorType { get; }
        public virtual WeaponType GetWeaponType { get; }


        public Slot Slot
        {
            get; set;
        }




    }
}
