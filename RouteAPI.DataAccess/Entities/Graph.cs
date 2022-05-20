using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteAPI.DataAccess.Entities
{
    public class Graph
    {
        public ConcurrentBag<Node> Nodes { get; } = new();

        public ConcurrentBag<Edge> Edges { get; } = new();

        private object _lock = new object();

        public Edge this[string source, string destination]
        {
            get
            {
                return Edges.FirstOrDefault(edge =>
                    string.Equals(source, edge.Source.Key) && string.Equals(destination, edge.Target.Key));
            }
        }

        public Node this[string source]
        {
            get
            {
                return Nodes.FirstOrDefault(node =>
                    string.Equals(source, node.Key));
            }
        }

        public virtual Node AddNode(string node)
        {
            if (this[node] != null)
                return this[node];

            var newNode = new Node(node);
            lock (_lock)
            {
                if (this[node] != null)
                    return this[node];

                Nodes.Add(newNode);
            }

            return newNode;
        }

        public virtual async Task<Edge> AddEdge(string origin, string dest, int distance)
        {
            var originNode = await Task.Run(() => AddNode(origin));
            var destNode = await Task.Run(() => AddNode(dest));

            if (originNode == null || destNode == null)
                throw new Exception("Landmarks for route does not exist");

            originNode.AddNeighbour(destNode);

            var newEdge = new Edge(originNode, destNode, distance);
            this.Edges.Add(newEdge);
            return newEdge;
        }
    }
}
