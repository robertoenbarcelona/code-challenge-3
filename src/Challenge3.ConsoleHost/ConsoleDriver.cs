
namespace Challenge3.ConsoleHost
{
    using Challenge3.UI;
    using System;

    /// <summary>
    /// Use <see cref="System.Console"/> as input output driver
    /// </summary>
    internal class ConsoleDriver : IInputOutputDriver
    {
        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Output(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Get the input stream.
        /// </summary>
        /// <returns>The user input</returns>
        public string Input()
        {
            return Console.ReadLine();
        }
    }
}
