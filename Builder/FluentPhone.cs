using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    // Phone
    // .On
    // .From("555-665-333")
    // .To("555-888-999")
    // .To("555-685-888")
    // .WithSubject(".NET Design Patterns")
    // .Call()
    
    interface IFrom
    {
        ITo From(string number);
    }

    interface ITo
    {
        ISubject To(string number);
    }

    interface ISubject : ICall, ITo
    {
        ICall WithSubject(string subject);
    }

    interface ICall
    {
        void Call();
    }

    class FluentPhone : IFrom, ITo, ISubject, ICall
    {
        private string from;
        // private string to;
        private IList<string> tos = new List<string>();

        private string subject;

        protected FluentPhone()
        {
        }

        public static IFrom On
        {
            get
            {
                return new FluentPhone();
            }
        }

        public ITo From(string number)
        {
            this.from = number;
            return this;
        }

        public ISubject To(string number)
        {
            // this.to = number;
            this.tos.Add(number);

            return this;
        }

        public ICall WithSubject(string subject)
        {
            this.subject = subject;
            return this;
        }


        // Build
        public void Call()
        {
            // Console.WriteLine($"Calling from {from} to {to} subject {subject}");

            foreach (var to in tos)
            {
                Console.WriteLine($"Calling from {from} to {to} subject {subject}");
            }
            
        }
    }
}
