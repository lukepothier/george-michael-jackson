using GeorgeMichaelJackson.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GeorgeMichaelJackson
{
    internal class Program
    {
        private static void Main()
        {
            var sw = Stopwatch.StartNew();

            var vertices = new List<Vertex<string>>();

            // Assuming all the strings are already valid, unique, trimmed, etc.
            var inputs = File.ReadLines(@"Assets\billboard-unique.txt");

            Console.WriteLine($"Input count was {inputs.Count()} strings");

            foreach (var input in inputs)
                vertices.Add(new Vertex<string>(input));

            var dictionary = new Dictionary<string, Vertex<string>>();

            foreach (var vertex in vertices)
                dictionary.Add(vertex.Value, vertex);

            foreach (var entry in dictionary)
            {
                var vertexPrefix = entry.Key.Split(' ').First();
                var vertexSuffix = entry.Key.Split(' ').Last();

                foreach (var innerEntry in dictionary)
                {
                    if (innerEntry.Key != entry.Key &&
                        (innerEntry.Value.ToString().EndsWith(vertexPrefix, StringComparison.OrdinalIgnoreCase) ||
                            (innerEntry.Value.ToString().StartsWith($"{vertexSuffix} ", StringComparison.OrdinalIgnoreCase) ||
                                innerEntry.Value.ToString().Equals(vertexSuffix, StringComparison.OrdinalIgnoreCase))))
                    {
                        entry.Value.AddEdge(innerEntry.Value);
                    }
                }
            }

            var graph = new Graph<string>(dictionary);

            var graphBuildDuration = sw.Elapsed;

            Debug.WriteLine($"Graph was size {graph.Size}");

            var searchSw = Stopwatch.StartNew();

            foreach (var vertex in graph.Vertices)
            {
                graph.DepthFirstSearch(vertex, v => Debug.WriteLine(v));
                Debug.WriteLine(Environment.NewLine);
            }

            var searchDuration = searchSw.Elapsed;

            Console.WriteLine($"Graph max length: {graph.MaxLength}");

            Console.WriteLine(graph.LongestPaths.Count == 1
                ? "Graph longest path:"
                : "Graph longest paths:");

            foreach (var path in graph.LongestPaths)
                Console.WriteLine(CleanPathString(path));

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Graph was constructed in {graphBuildDuration}");
            Console.WriteLine($"Search took was constructed in {searchDuration}");
            Console.WriteLine($"Total time was {sw.Elapsed}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static string CleanPathString(string path)
        {
            path = Regex.Replace(Regex.Replace(path, @"[\d>:-]", ""), @"\s+", " ");

            var words = path.Split(' ');
            var sb = new StringBuilder();

            for (var i = 0; i < words.Length; i++)
                if (words[i] != (i != words.Length - 1 ? words[i + 1] : default))
                    sb.Append($"{words[i]} ");

            return sb.ToString().Trim();
        }
    }
}
