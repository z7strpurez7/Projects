using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPGHeroes.Enum;

namespace RPGHeroes.Item
{
    class Weapon : Item
    {
        private int weaponDamage;
        private WeaponType type;
        public override int WeaponDamage { get; set; }

        public WeaponType Type { get; set; }
        public override WeaponType GetWeaponType { get => Type; }


        public Weapon(String name, int requiredLevel, int weaponDamage, WeaponType weaponType, Slot weaponSlot = Slot.Weapon)
        {
            this.Name = name;
            this.RequiredLevel = requiredLevel;
            this.WeaponDamage = weaponDamage;
            this.Type = weaponType;
            this.Slot = weaponSlot;
        }
    }
}
