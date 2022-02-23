
namespace RouteAPI.DataAccess.Entities
{
    public class Route
    {
        public string FromLandMark { get; }

        public string ToLandMark { get; }

        public int Distance { get; }

        public override string ToString()
        {
            return $"{FromLandMark}-{ToLandMark}";
        }

        public Route(string from , string to, int distance)
        {
            FromLandMark = from;
            ToLandMark = to;
            Distance = distance;
        }
    }
}
