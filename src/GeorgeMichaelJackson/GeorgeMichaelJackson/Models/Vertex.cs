using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeorgeMichaelJackson.Models
{
    internal class Vertex<T>
    {
        public Vertex(T value, params Vertex<T>[] parameters)
            : this(value, (IEnumerable<Vertex<T>>)parameters)
        {
        }

        public Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)
        {
            Value = value;
            Neighbors = neighbors?.ToList() ?? new List<Vertex<T>>();
        }

        public T Value { get; set; }

        public List<Vertex<T>> Neighbors { get; }

        public bool IsVisited { get; set; }

        public int NeighborCount => Neighbors.Count;

        public void AddEdge(Vertex<T> vertex)
            => Neighbors.Add(vertex);

        public void AddEdges(params Vertex<T>[] newNeighbors)
            => Neighbors.AddRange(newNeighbors);

        public void AddEdges(IEnumerable<Vertex<T>> newNeighbors)
            => Neighbors.AddRange(newNeighbors);

        public void RemoveEdge(Vertex<T> vertex)
            => Neighbors.Remove(vertex);

        public override string ToString()
            => Neighbors.Aggregate(new StringBuilder($"{Value}: "), (sb, n) => sb.Append($"{n.Value} -> ")).ToString();
    }
}
