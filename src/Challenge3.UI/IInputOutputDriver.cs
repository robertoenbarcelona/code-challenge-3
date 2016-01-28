
namespace Challenge3.UI
{
    /// <summary>
    /// Represents the input oupt driver of the runnng host 
    /// </summary>
    public interface IInputOutputDriver
    {
        /// <summary>
        /// Outputs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Output(string message);

        /// <summary>
        ///Get the input stream.
        /// </summary>
        /// <returns></returns>
        string Input();
    }
}
