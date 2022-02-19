namespace RouteAPI
{
    public interface IRouteManager
    {
        /// <summary>
        /// Registers a Route 
        /// </summary>
        /// <param name="fromLandMark"></param>
        /// <param name="toLandMark"></param>
        /// <param name="weightedDistance"></param>
        /// <returns>Returns true if registered successfully</returns>
        bool RegisterRoute(string fromLandMark, string toLandMark, int weightedDistance);
    }
}