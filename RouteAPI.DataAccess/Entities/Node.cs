using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RouteAPI.DataAccess.Entities
{
    public class Node
    {
        private object _lock = new object();

        public Node(string key)
        {
            Key = key;
            AdjacentNodes = new ConcurrentBag<Node>();
        }

        [JsonIgnore]
        public ConcurrentBag<Node> AdjacentNodes { get; }

        public string Key { get; }

        public void AddNeighbour(Node node)
        {
            lock (_lock)
            {
                if (AdjacentNodes.Contains(node))
                    return;

                AdjacentNodes.Add(node);
            }
        }
    }
}
