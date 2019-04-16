using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class HyteraRadio
    {
        public byte Volume { get; set; }

        public void Start()
        {
            Volume = 50;
        }

        public void Ring(string number)
        {
            Console.WriteLine($"Calling to {number} volume {Volume}");
        }

    }
}
