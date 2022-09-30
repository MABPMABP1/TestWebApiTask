namespace WebApiExample.Services.DataServices
{
    /// <summary>
    /// Basic inplementation of <see cref="IDataService"/>
    /// No external calls. Delays emulated by <see cref="TaskWorkEmulator"/>
    /// </summary>
    public class BasicDataService : IDataService
    {
        private const int _defaultDelayInSeconds = 1;
        private const int _millisecondsInSecond = 1000;

        private int GetMilliseconds(int seconds) => seconds * _millisecondsInSecond;
        
        /// <summary>
        /// Emulates delay durin data receiving process.
        /// </summary>
        /// <param name="delayInSeconds">Delay in seconds</param>
        /// <returns></returns>
        public async Task GetData(int delayInSeconds)
        {
            if(delayInSeconds < 0)
                delayInSeconds = _defaultDelayInSeconds;
            await TaskWorkEmulator.DoWork(GetMilliseconds(delayInSeconds));
        }
    }
}
