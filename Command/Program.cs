using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandPrintTest();

            CommandScanTest();

            CommandQueueTest();

           // StandardPrintAndScan();
        }

        private static void CommandQueueTest()
        {
            //ICommand command = new ScanCommand(true);
            //command.Execute();

            Queue<ICommand> commands = new Queue<ICommand>();
            commands.Enqueue(new ScanCommand(true));
            commands.Enqueue(new PrintCommand(true, 5, "Hello World"));
            commands.Enqueue(new PrintCommand(true, 1, "Hello .NET"));

            foreach (var command in commands)
            {
                if (command.CanExecute())
                    command.Execute();
            }

        }

        private static void CommandScanTest()
        {
            ScanCommand scanCommand = new ScanCommand(true);
            scanCommand.Execute();
        }

        private static void CommandPrintTest()
        {
            PrintCommand printCommand = new PrintCommand(true, 3, "Hello World");

            if (printCommand.CanExecute())
            {
                printCommand.Execute();
            }
        }

        private static void StandardPrintAndScan()
        {
            var printer = new Printer();
            printer.Print("Hello World", 5);

            if (printer.CanPrint(string.Empty, 3))
            {
                printer.Print(string.Empty, 3);
            }

            if (printer.CanScan())
            {
                printer.Print(string.Empty, 1);
            }
        }
    }

    /*
     *   class Command 
     *        void Execute()
     */

    interface ICommand
    {
        void Execute();
        bool CanExecute();
    }

    class ScanCommand : ICommand
    {
        private readonly bool isPowerOn;

        public ScanCommand(bool isPowerOn)
        {
            this.isPowerOn = isPowerOn;
        }

        public void Execute()
        {
            if (isPowerOn)
            {
                Console.WriteLine("Scanning...");
            }
        }

        public bool CanExecute()
        {
            return isPowerOn;
        }
    }

    class PrintCommand : ICommand
    {
        private readonly bool isPowerOn;
        private readonly byte pages;
        private readonly string content;

        public PrintCommand(bool isPowerOn, byte pages, string content)
        {
            this.isPowerOn = isPowerOn;
            this.pages = pages;
            this.content = content;
        }

        public void Execute()
        {
            if (isPowerOn)
            {
                for (int page = 0; page < pages; page++)
                {
                    Console.WriteLine($"{page} {content}");
                }
            }
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(content)
               && pages > 0
               && isPowerOn;
        }
    }

    class Printer
    {
        private bool isPowerOn;

        public void Print(string content, byte pages)
        {
            if (isPowerOn)
            {
                for (int page = 0; page < pages; page++)
                {
                    Console.WriteLine($"{page} {content}");
                }
            }
        }

        public void Scan()
        {
            if (isPowerOn)
            {
                Console.WriteLine("Scanning...");
            }
        }

        public bool CanScan()
        {
            return isPowerOn;
        }

        public bool CanPrint(string content, byte pages)
        {
            return !string.IsNullOrEmpty(content)
                && pages > 0
                && isPowerOn;
        }
    }
}
