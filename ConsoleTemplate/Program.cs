// (c) 2020 Moonwave Interactive. All rights reserved under MIT License.

using System;

namespace ConsoleTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Template";
            Console.WriteLine("Moonwave Interactive Console Template");
            Console.WriteLine("(c) 2020 Moonwave Interactive. All rights reserved under MIT License.\n");

            CmdSystem.Initialize();

            while (true)
            {
                Console.Write("util>");

                CmdSystem.Execute(Console.ReadLine());
            }
        }
    }
}
