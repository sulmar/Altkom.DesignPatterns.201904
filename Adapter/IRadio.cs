using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    interface IRadio
    {
        void Call(string recipient);
    }

    public class MotorolaAdapter : IRadio
    {
        // Adaptee
        private MotorolaRadio radio;

        public MotorolaAdapter()
        {
            radio = new MotorolaRadio();
        }

        public void Call(string recipient)
        {
            radio.Init();
            radio.Call(recipient);
            radio.Release();
        }
    }

    public class HyteraAdapter : IRadio
    {
        private HyteraRadio radio;

        public HyteraAdapter()
        {
            radio = new HyteraRadio();
        }

        public void Call(string recipient)
        {
            radio.Start();
            radio.Ring(recipient);
        }
    }
}
