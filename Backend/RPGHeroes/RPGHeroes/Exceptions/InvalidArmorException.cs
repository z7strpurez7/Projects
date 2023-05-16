using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes.Exceptions
{
    public class InvalidArmorException : Exception
    {
        public InvalidArmorException()
        {
        }

        public InvalidArmorException(string message)
            : base(message)
        {
        }

        public InvalidArmorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
