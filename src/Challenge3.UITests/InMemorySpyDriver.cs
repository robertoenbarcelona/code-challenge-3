
namespace Challenge3.UITests
{
    using Challenge3.UI;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Spy stub for test
    /// </summary>
    internal class InMemorySpyDriver : IInputOutputDriver
    {
        private readonly Queue<string> inputStream = new Queue<string>();
        private readonly Queue<string> outputStream = new Queue<string>();

        /// <summary>
        /// Gets the input stream.
        /// </summary>
        internal Queue<string> InputStream
        {
            get { return inputStream; }
        }

        /// <summary>
        /// Gets the output stream.
        /// </summary>
        internal Queue<string> OutputStream
        {
            get { return outputStream; }
        } 

        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Output(string message)
        {
            this.outputStream.Enqueue(message);
        }

        /// <summary>
        /// Get the input stream.
        /// </summary>
        /// <returns></returns>
        public string Input()
        {
            return this.inputStream.Dequeue();
        }
    }
}
