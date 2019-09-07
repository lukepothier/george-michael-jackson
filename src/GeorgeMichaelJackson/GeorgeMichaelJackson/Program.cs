using GeorgeMichaelJackson.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeorgeMichaelJackson
{
    class Program
    {
        static void Main()
        {
            var vertices = new List<Vertex<string>>();

            var inputs = File.ReadLines(@"Assets\billboard-unique.txt");

            foreach (var input in inputs)
            {
                vertices.Add(new Vertex<string>(input));
            }

            var dictionary = new Dictionary<string, Vertex<string>>();

            foreach (var vertex in vertices)
            {
                dictionary.Add(vertex.Value, vertex);
            }

            foreach (var entry in dictionary)
            {
                var vertexPrefix = entry.Key.Split(' ').First();
                var vertexSuffix = entry.Key.Split(' ').Last();

                foreach (var innerEntry in dictionary)
                {
                    if (innerEntry.Key != entry.Key &&
                        (innerEntry.Value.ToString().EndsWith(vertexPrefix, StringComparison.OrdinalIgnoreCase) ||
                            innerEntry.Value.ToString().StartsWith(vertexSuffix, StringComparison.OrdinalIgnoreCase)))
                    {
                        entry.Value.AddEdge(innerEntry.Value);
                    }
                }
            }

            var graph = new Graph<string>(dictionary);

            foreach (var vertex in graph.Vertices)
            {
                graph.DepthFirstSearch(vertex, v => Console.WriteLine(v));
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
