using Stateless;
using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    class Lamp
    {
        // Install-Package Stateless

        private StateMachine<LampState, LampTrigger> machine;

        public Lamp()
        {
            machine = new StateMachine<LampState, LampTrigger>(LampState.Off);

            machine.Configure(LampState.Off)
                .Permit(LampTrigger.Down, LampState.On)
                .Ignore(LampTrigger.Up)
                ;

            machine.Configure(LampState.On)
                .OnEntry(() => Console.WriteLine("Pamietaj o wyl. swiatla"), "Powitanie")
                .Permit(LampTrigger.Up, LampState.Off)
                .Permit(LampTrigger.Down, LampState.Blinking)
               // .Ignore(LampTrigger.Down)
                ;

            machine.Configure(LampState.Blinking)
                .Permit(LampTrigger.Up, LampState.Off)
                .Permit(LampTrigger.Down, LampState.On);
        }

        public void PushDown() => machine.Fire(LampTrigger.Down);

        public void PushUp() => machine.Fire(LampTrigger.Up);

        public bool CanPushDown => machine.CanFire(LampTrigger.Down);

        public bool CanPushUp => machine.CanFire(LampTrigger.Up);

        public LampState State => machine.State;

        public string Graph => UmlDotGraph.Format(machine.GetInfo());

        public string SerialNumber { get; set; }

        public Lamp(string serialNumber)
            : this()
        {
            SerialNumber = serialNumber;
        }
    }

    enum LampTrigger
    {
        Up,
        Down
    }

    enum LampState
    {
        On,
        Off,
        Blinking
    }
}
