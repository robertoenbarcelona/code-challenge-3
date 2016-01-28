
namespace Challenge3.ConsoleHost
{
    using Challenge3.UI;
    using System;

    class Program
    {
        private static IHumanToSoftwareInterface Interpreter;

        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                keepRunning = Program.Interpreter.HandleUserNeeds();
            }
        }
    }
}
