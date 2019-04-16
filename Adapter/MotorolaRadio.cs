using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class MotorolaRadio
    {
        private bool powerOn = false;

        public void Init()
        {
            powerOn = true;
        }

        public void Call(string to)
        {
            if (powerOn)
            {
                Console.WriteLine($"Calling to {to}");
            }
        }

        public void Release()
        {
            powerOn = false;
        }

    }
}
