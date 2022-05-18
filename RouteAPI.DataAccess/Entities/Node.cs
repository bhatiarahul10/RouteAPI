using System.Collections.Generic;

namespace RouteAPI.DataAccess.Entities
{
    public class Node
    {
        public Node(string key)
        {
            Key = key;
            AdjacentNodes = new List<Node>();
        }

        public List<Node> AdjacentNodes { get; }

        public string Key { get; }
    }
}
