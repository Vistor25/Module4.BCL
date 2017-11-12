using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileWatcher fw = new FileWatcher();
            fw.Watch();
            Console.CancelKeyPress += ClickHandler;
            Console.WriteLine("Enter 'q' or click CTRL+C or CTRL+BREAK for finish to execute application");
            while (Console.Read() != 'q') ;
        }
        protected static void ClickHandler(object sender, ConsoleCancelEventArgs args)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
