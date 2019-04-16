using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            IRadio radio = new MotorolaAdapter();
            radio.Call("55347534");

            radio = new HyteraAdapter();
            radio.Call("87666664");

            // BadPractice();
        }

        private static void BadPractice()
        {
            if (false)
            {
                MotorolaRadio radio = new MotorolaRadio();
                radio.Init();
                radio.Call("55347534");
                radio.Release();
            }
            else
            {
                HyteraRadio radio = new HyteraRadio();
                radio.Start();
                radio.Ring("55347534");
            }
        }
    }
}
