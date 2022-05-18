using RouteAPI.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

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
                throw new Exception("Landmark exists");

            var newNode = new Node(node);
            this.Nodes.Add(newNode);
            return newNode;
        }

        public virtual Edge AddEdge(string origin, string dest, int distance)
        {
            if (this[origin,dest] != null)
                throw new Exception("Route exists");

            if (string.Equals(origin, dest, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Invalid Route");

            var originNode = AddNode(origin);
            var destNode = AddNode(dest);

            if (originNode == null || destNode == null)
                throw new Exception("Landmarks for route does not exist");
            
            originNode.AdjacentNodes.Add(destNode);

            var newEdge = new Edge(originNode,destNode,distance);
            this.Edges.Add(newEdge);
            return newEdge;
        }
    }
}
