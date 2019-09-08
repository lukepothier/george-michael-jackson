using System;
using System.Collections.Generic;
using System.Linq;

namespace GeorgeMichaelJackson.Models
{
    internal class Graph<T>
    {
        public int MaxLength { get; private set; } = 0;
        public IList<string> LongestPaths { get; private set; } = new List<string>();

        public Graph(params Vertex<T>[] initialNodes)
            : this((IEnumerable<Vertex<T>>)initialNodes)
        {
        }

        public Graph(IEnumerable<Vertex<T>> initialNodes = null)
        {
            Vertices = initialNodes?.ToList() ?? new List<Vertex<T>>();
        }

        public Graph(Dictionary<string, Vertex<T>> initialNodes = null)
        {
            Vertices = new List<Vertex<T>>(initialNodes.Values);
        }

        public List<Vertex<T>> Vertices { get; }

        public int Size => Vertices.Count;

        public void AddPair(Vertex<T> first, Vertex<T> second)
        {
            AddToList(first);
            AddToList(second);
            AddNeighbors(first, second);
        }

        public void DepthFirstSearch(Vertex<T> root, Action<string> writer)
        {
            UnvisitAll();
            DepthFirstSearchImpl(root, writer);
        }

        private void DepthFirstSearchImpl(Vertex<T> root, Action<string> writer, string previousValue = null, int count = 0)
        {
            if (!root.IsVisited)
            {
                bool longest = false;

                count++;

                if (count > MaxLength)
                {
                    MaxLength = count;
                    longest = true;
                }

                var value = previousValue + (root.NeighborCount > 0
                    ? $"{count}: {root.Value} -> "
                    : $"{count}: {root.Value.ToString()}");

                if (count == MaxLength)
                {
                    LongestPaths.Add(value);
                }

                if (longest)
                {
                    LongestPaths = new List<string>() { value };
                }

                writer(value);

                root.IsVisited = true;

                foreach (Vertex<T> neighbor in root.Neighbors)
                {
                    DepthFirstSearchImpl(neighbor, writer, value, count);
                }
            }
        }

        private void AddToList(Vertex<T> vertex)
        {
            if (!Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
            }
        }

        private void AddNeighbors(Vertex<T> first, Vertex<T> second)
        {
            AddNeighbor(first, second);
            AddNeighbor(second, first);
        }

        private void AddNeighbor(Vertex<T> first, Vertex<T> second)
        {
            if (!first.Neighbors.Contains(second))
            {
                first.AddEdge(second);
            }
        }

        private void UnvisitAll()
        {
            foreach (var vertex in Vertices)
            {
                vertex.IsVisited = false;
            }
        }
    }
}
