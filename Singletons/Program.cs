using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singletons.Before
{
    class Program
    {
        static Logger logger;

        static void Main2(string[] args)
        {
            if (logger == null)
            {
                logger = new Logger();
            }
            DoWork();

            if (logger == null)
            {
                logger = new Logger();
            }
            Send();
        }

        private static void DoWork()
        {
            Logger logger = new Logger();
            logger.Log("working...");
        }

        private static void Send()
        {
            Logger logger = new Logger();
            logger.Log("sending...");
        }
    }

    class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging {message}");
        }
    }
}

namespace Singletons.After
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DoWork();
            Send();

            //Logger logger1 = Logger.Instance;
            //Logger logger2 = Logger.Instance;

            Logger logger1 = Singleton<Logger>.Instance;
            Logger logger2 = Singleton<Logger>.Instance;


            // if (logger1==logger2)
            if (ReferenceEquals(logger1, logger2))
            {
                Console.WriteLine("Identyczne instancje");
            }
            else
            {
                Console.WriteLine("Różne instance?");
            }

        }

        private static void DoWork()
        {
            Logger logger = Singleton<Logger>.Instance;
            logger.Log("working...");
        }

        private static void Send()
        {
            Logger logger = Singleton<Logger>.Instance;
            logger.Log("sending...");
        }

        public static void Print()
        {
            Logger logger = Singleton<Logger>.Instance;
            logger.Log("Printing...");
        }
    }

    class DbLogger : Logger
    {
        public DbLogger()
        {

        }
    }

    class Singleton<T>
        where T : new()
    {
        protected Singleton()
        {
        }

        private static object syncLock = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                lock(syncLock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }

                return instance;
                
            }
        }

    }

    class Logger
    {
        //protected Logger()
        //{

        //}

        //private static Logger instance;

        //private static object syncLock = new object();

        //public static Logger Instance
        //{
        //    get
        //    {
        //        lock (syncLock)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new Logger();
        //            }
        //        }

        //        return instance;
        //    }
        //}

        public void Log(string message)
        {
            Console.WriteLine($"Logging {message}");
        }
    }
}
