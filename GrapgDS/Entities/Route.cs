using GrapgDS;

namespace RouteAPI.Entities
{
    public class Route
    {
        public Landmark FromLandMark { get; }

        public Landmark ToLandMark { get; }

        public int Distance { get; }

        public Route(Landmark from , Landmark to, int distance)
        {
            FromLandMark = from;
            ToLandMark = to;
            Distance = distance;
        }

    }
}
