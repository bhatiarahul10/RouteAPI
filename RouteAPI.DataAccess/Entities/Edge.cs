namespace RouteAPI.DataAccess.Entities
{
    public class Edge
    {
        public Edge(Node source, Node target, int distance)
        {
            Source = source;
            Target = target;
            Distance = distance;
        }

        public Node Source { get; }
        public Node Target { get; }
        public int Distance { get; }
    }
}
