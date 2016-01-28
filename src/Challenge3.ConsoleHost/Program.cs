
namespace Challenge3.ConsoleHost
{
    using Challenge3.UI;
    using System;

    class Program
    {
        private static IHumanToSoftwareInterface Interpreter;

        static void Main(string[] args)
        {
            bool userNeedsMore;
            do
            {
                userNeedsMore = Program.Interpreter.HandleUserNeeds();

            } while (userNeedsMore);
        }
    }
}
