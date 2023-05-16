using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeroes.Exceptions
{
    using System;

    namespace RPGHeroes.Exceptions
    {
        public class InvalidWeaponException : Exception
        {
            public InvalidWeaponException()
            {
            }

            public InvalidWeaponException(string message)
                : base(message)
            {
            }

            public InvalidWeaponException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    }
}
