using GrapgDS;

namespace RouteAPI.Entities
{
    public class Route
    {
        public string FromLandMark { get; }

        public string ToLandMark { get; }

        public int Distance { get; }

        public Route(string from , string to, int distance)
        {
            FromLandMark = from;
            ToLandMark = to;
            Distance = distance;
        }
    }
}
