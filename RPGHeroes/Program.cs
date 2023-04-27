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

            Mage Mage1 = new Mage("Magix");
        
           
     
            //    Console.WriteLine("Name: " + Mage1.Name + " | " + "level: " + Mage1.Level);
            Mage1.Display();
            Mage1.LevelUp();
            Mage1.Display();
          
        }
    }
}
