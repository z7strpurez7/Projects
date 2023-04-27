using System;

namespace RPGHeroes
{
    class Hero //Hero class defines hero
    {
        private int level;
        private string name;
        private int strength;
        private int detexterity;
        private int intelligence;

        // Getter and setter for the values - detailed
        public string Name
        {
            get{return name;}
            set{name = value;}
      }

        //Getter setter / information hiding / syntatic sugar
        public int Level { get => level; set => level = value; }
        public int Strength { get; set; }
        public int Detexterity { get; set; }
        public int Intelligence { get; set; }



        public void Equip() { }
        public void Damage() { }
        public void TotalAttributes() { }
        public void Display()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(Name + "(" + Level + ")");
            Console.WriteLine("Attributes:");
            Console.WriteLine("Strength: " + Strength + " | " + "Detexterity: " + Detexterity + " | " + "Intelligence: " + Intelligence);
            Console.WriteLine("------------------------------------------");
        }
        public Hero() //Default constructor - explicit
        {
            Level = 1;
        }
    }
}
