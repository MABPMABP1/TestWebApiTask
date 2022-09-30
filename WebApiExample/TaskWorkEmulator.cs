namespace WebApiExample
{
    /// <summary>
    /// Emulates async work
    /// </summary>
    public static class TaskWorkEmulator
    {
        private const int _minimumMillisecondsDelay = 1000;
        private const int _maximumMillisecondsDelay = 3000;
        
        /// <summary>
        /// Emulates work using delay
        /// </summary>
        /// <param name="milliseconds">Delay duration</param>
        public async static Task DoWork(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        /// <summary>
        /// Emulates work using delay. Delay duration is random on some predefined interval
        /// </summary>
        /// <returns></returns>
        public async static Task DoWork()
        {
            int delay = new Random().Next(_minimumMillisecondsDelay, _maximumMillisecondsDelay);
            await DoWork(delay);
        }
    }
}
