namespace WebApiExample.Services.DataServices
{
    /// <summary>
    /// Base interface for data services
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Receives data with delay
        /// </summary>
        /// <param name="delayInSeconds">Delay in seconds</param>
        /// <returns>Task for async work</returns>
        public Task GetData(int delayInSeconds);
    }
}
