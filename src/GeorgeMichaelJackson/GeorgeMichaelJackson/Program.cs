using GeorgeMichaelJackson.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeorgeMichaelJackson
{
    class Program
    {
        // Temporary inputs for testing
        static readonly string[] inputs = { "Jackson Five", "George Michael", "Michael Jackson", "Fall Out Boy",
            "Elvis Presley", "Five Finger Death Punch", "Boy George", "Janet Jackson" };

        static void Main()
        {
            var vertices = new List<Vertex<string>>();

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

                Debug.WriteLine($"Entry \"{entry.Key}\" has prefix \"{vertexPrefix}\" and suffix \"{vertexSuffix}\"");

                foreach (var innerEntry in dictionary)
                {
                    if (innerEntry.Key != entry.Key &&
                        (innerEntry.Value.ToString().EndsWith(vertexPrefix) || innerEntry.Value.ToString().StartsWith(vertexSuffix)))
                    {
                        entry.Value.AddEdge(innerEntry.Value);
                    }
                }
            }

            var graph = new Graph<string>(dictionary);

            Debug.WriteLine(Environment.NewLine);
            Console.WriteLine("Vertices in the graph:");

            foreach (var vertex in graph.Vertices)
            {
                Console.WriteLine(vertex.ToString());
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
